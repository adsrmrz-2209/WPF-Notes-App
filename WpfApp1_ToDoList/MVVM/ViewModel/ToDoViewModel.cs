using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1_ToDoList.MVVM.Model;
using WpfApp1_ToDoList.MVVM.View;

namespace WpfApp1_ToDoList.MVVM.ViewModel
{
    internal class ToDoViewModel : ViewModelBase
    {
        public ObservableCollection<object> Notes { get; set; }
        public RelayCommand AddNoteCommand => new RelayCommand(execute => AddNoteBtn());
		public RelayCommand DeleteNoteCommand => new RelayCommand(execute => DeleteNoteBtn());
		public RelayCommand ClearNoteCommand => new RelayCommand(execute => ClearNoteBtn());
		public RelayCommand OpenEditWindowCommand => new RelayCommand(execute => OpenEditWindow());
		public RelayCommand SaveChanges => new RelayCommand(execute => ApplyEditChanges());
		public RelayCommand MinimizeCommand => new RelayCommand(execute => Minimize());
		public RelayCommand MaximizeCommand => new RelayCommand(execute => Maximize());
		public RelayCommand CloseCommand => new RelayCommand(execute => Close());

        EditNoteWindow editNoteWindow = null;

		//constructor
        public ToDoViewModel() 
		{
			Notes = new ObservableCollection<object>();
        }

		//Binded to listview SelectedItem (object)
        public object selectedNote { get; set; }
        public object SelectedNote 
		{
			get { return selectedNote; }
			set 
			{ 
				selectedNote = value;
				OnPropertyChanged();
			} 
		}

        //Binded to listview SelectedIndex
        //This is for the edit window, when editing and setting the changes, get the Notes[SelectedIndex] then assign the EditNoteText
        //Notes[SelectedIndex] = editNoteText;
        private int selectedIndex;
		public int SelectedIndex
        {
			get { return selectedIndex; }
			set 
			{
                selectedIndex = value;
                //OnPropertyChanged();
            }
		}

		private string toDoTxt;

		public string ToDoTxt
		{
			get { return toDoTxt; }
			set 
			{ 
				toDoTxt = value;
				OnPropertyChanged();
			}
		}


		//The textbox element in the EditNoteWindow
		private string editNoteText;
		public string EditNoteText
        {
			get { return editNoteText; }
			set 
			{
                editNoteText = value; 
				OnPropertyChanged();
			}
		}


        void AddNoteBtn()
		{	
			if(!string.IsNullOrEmpty(ToDoTxt))
			{
				Notes.Add(ToDoTxt);
                ToDoTxt = string.Empty;
            }
		}

		void OpenEditWindow()
		{
			if (SelectedNote != null)
			{
                editNoteWindow = new EditNoteWindow();

                //to make sure the current note is displayed in the edit text box
                EditNoteText = SelectedNote.ToString(); 

                editNoteWindow.DataContext = this;

				editNoteWindow.Owner = Application.Current.MainWindow;
                editNoteWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;


				Application.Current.MainWindow.Opacity = 0.3;
                editNoteWindow.ShowDialog();

                Application.Current.MainWindow.Opacity = 1;


            }
        }

		void DeleteNoteBtn()
		{
			Notes.Remove(SelectedNote); //object
		}

		void ClearNoteBtn()
		{
			Notes.Clear();
		}

		void ApplyEditChanges()
		{
			//get the Notes element SelectedIndex then assign the changes typed in the EditNoteWindow Textbox
			Notes[SelectedIndex] = EditNoteText;
			//SelectedNote = EditNoteText;
            editNoteWindow.Close();
        }

		void Minimize()
		{
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		void Maximize()
		{
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState == WindowState.Maximized ?
            WindowState.Normal : WindowState.Maximized;
        }

		void Close()
		{
			Application.Current.Shutdown();
		}

	}
}
