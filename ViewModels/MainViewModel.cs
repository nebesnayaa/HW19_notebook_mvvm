using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

using HW19_mvvm_notebook.Commands;
using HW19_mvvm_notebook.Views;

namespace HW19_mvvm_notebook.ViewModels
{
    internal class MainViewModel : DependencyObject
    {

        int _fileCounter = 1;

        private Note _selectedPerson;

        public Note SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
            }
        }
        public ObservableCollection<Note> People { get; set; }


        DelegateCommand closeCommand;
        public ICommand CloseCommand { get { return closeCommand; } }

        DelegateCommand addCommand;
        public ICommand AddCommand { get { return addCommand; } }

        DelegateCommand editCommand;
        public ICommand EditCommand { get { return editCommand; } }

        DelegateCommand saveSelectedCommand;
        public ICommand SaveSelectedCommand { get { return saveSelectedCommand; } }

        DelegateCommand deleteCommand;
        public ICommand DeleteCommand { get { return deleteCommand; } }

        public MainViewModel()
        {
            People = new ObservableCollection<Note>();
            closeCommand = new DelegateCommand(CloseWindow, CanAlways);
            addCommand = new DelegateCommand(AddPerson, CanAlways);
            editCommand = new DelegateCommand(Edit, CanModifyItem);
            deleteCommand = new DelegateCommand(Delete, CanModifyItem);
            saveSelectedCommand = new DelegateCommand(SaveSelected, CanSaveSelected);
        }

        private void SaveSelected(object obj)
        {
            if (SelectedPerson == null)
            {
                MessageBox.Show("Please select a person to save.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string json = JsonSerializer.Serialize(SelectedPerson);
                string fileName = $"Note{_fileCounter++.ToString("D3")}.json";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving selected person: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSaveSelected(object obj)
        {
            return SelectedPerson != null;
        }

        private bool CanModifyItem(object obj)
        {
            return obj != null;
        }

        private void Edit(object obj)
        {
            AddPersonWindow view = new AddPersonWindow();
            NoteViewModel viewModel = new NoteViewModel();
            view.DataContext = viewModel;
            view.ShowDialog();

            if (!viewModel.Result) return;

            if (viewModel.FullName == "" || viewModel.Address == "" || viewModel.Phone == "")
            {
                MessageBox.Show("Please, fill all the fields.");
                return;
            }

            People.Remove(obj as Note);
            People.Add(new Note(viewModel.FullName, viewModel.Address, viewModel.Phone));
        }

        private void Delete(object obj)
        {
            People.Remove(obj as Note);
        }

        private bool CanAlways(object obj)
        {
            return true;
        }

        private void CloseWindow(object obj)
        {
            (obj as Window).Close();
        }

        private void AddPerson(object obj)
        {
            AddPersonWindow view = new AddPersonWindow();
            NoteViewModel viewModel = new NoteViewModel();
            view.DataContext = viewModel;
            view.ShowDialog();

            if (!viewModel.Result) return;

            if (viewModel.FullName == "" || viewModel.Address == "" || viewModel.Phone == "")
            {
                MessageBox.Show("Please, fill all the fields.");
                return;
            }

            People.Add(new Note(viewModel.FullName, viewModel.Address, viewModel.Phone));
        }
    }
}