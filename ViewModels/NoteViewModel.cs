using System.Windows;
using System.Windows.Input;

using HW19_mvvm_notebook.Commands;

namespace HW19_mvvm_notebook.ViewModels
{
    internal class NoteViewModel : DependencyObject
    {
        private static readonly DependencyProperty _result;
        private static readonly DependencyProperty _fullName;
        private static readonly DependencyProperty _address;
        private static readonly DependencyProperty _phone;

        public bool Result
        {
            get { return (bool)GetValue(_result); }
            set { SetValue(_result, value); }
        }

        public string FullName
        {
            get { return (string)GetValue(_fullName); }
            set { SetValue(_fullName, value); }
        }

        public string Address
        {
            get { return (string)GetValue(_address); }
            set { SetValue(_address, value); }
        }

        public string Phone
        {
            get { return (string)GetValue(_phone); }
            set { SetValue(_phone, value); }
        }

        static NoteViewModel()
        {
            _result = DependencyProperty.Register("Result", typeof(bool), typeof(MainViewModel));

            _fullName = DependencyProperty.Register("FullName", typeof(string), typeof(MainViewModel));

            _address = DependencyProperty.Register("Address", typeof(string), typeof(MainViewModel));

            _phone = DependencyProperty.Register("Phone", typeof(string), typeof(MainViewModel));
        }

        DelegateCommand saveCommand;
        DelegateCommand closeCommand;

        public ICommand SaveCommand { get { return saveCommand; } }
        public ICommand CloseCommand { get { return closeCommand; } }

        public NoteViewModel()
        {
            closeCommand = new DelegateCommand(CloseWindow, CanAlways);
            saveCommand = new DelegateCommand(SaveNote, CanAlways);

            Result = false;
            FullName = "";
            Address = "";
            Phone = "";
        }
        private bool CanAlways(object obj)
        {
            return true;
        }

        private void CloseWindow(object obj)
        {
            Result = false;
            (obj as Window).Close();
        }

        private void SaveNote(object obj)
        {
            Result = true;
            (obj as Window).Close();
        }
    }
}
