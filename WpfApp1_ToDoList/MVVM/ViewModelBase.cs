using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1_ToDoList.MVVM
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
