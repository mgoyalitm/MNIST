using System.Windows;
namespace MNIST.ViewModel.Commands;
public class DialogButton : ICommand
{
    public bool? DialogResult { get; set; }
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object parameter) => parameter is Window;
    public void Execute(object parameter)
    {
        if (parameter is Window window)
        {
            window.DialogResult = DialogResult;
            window.Close();
        }
    }
}
