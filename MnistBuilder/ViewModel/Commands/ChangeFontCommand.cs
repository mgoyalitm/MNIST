namespace MNIST.ViewModel.Commands;
public class ChangeFontCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object parameter) => parameter is int;

    public async void Execute(object parameter)
    {
        if (parameter is int index)
        {
            App.ViewModel.SelectedFontIndex += index;
        }
    }
}
