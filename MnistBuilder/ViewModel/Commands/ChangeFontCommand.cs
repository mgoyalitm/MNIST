namespace MNIST.ViewModel.Commands;
public class ChangeFontCommand(FontController controller) : ICommand
{
    private readonly FontController _controller = controller;

    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object parameter) => parameter is int;

    public async void Execute(object parameter)
    {
        if (parameter is int index)
        {
            _controller.SelectedFontIndex += index;
        }
    }
}
