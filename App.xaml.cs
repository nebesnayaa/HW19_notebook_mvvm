using System.Windows;
using HW19_mvvm_notebook.ViewModels;

namespace HW19_mvvm_notebook
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow View = new MainWindow();
            MainViewModel ViewModel = new MainViewModel();
            View.DataContext = ViewModel;
            View.Show();
        }
    }
}
