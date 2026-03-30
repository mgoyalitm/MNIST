namespace MNIST.ViewModel.Commands;

public class DeleteFontCommand(FontController controller) : ICommand
{
    private readonly FontController _controller = controller;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public bool CanExecute(object parameter) => parameter is FontModel;

    public void Execute(object parameter)
    {
        if (parameter is FontModel font)
        {
            if (_controller.FontBucket.FirstOrDefault(x => x.Path == font.Path) is FontModel target)
            {
                _controller.FontBucket.Remove(target);
                _controller.MainViewModel.ShowNotification($"Font '{font}' added to font bucket.");
            }
            else
            {
                _controller.MainViewModel.ShowNotification($"Can't delete '{font}' not available in bucket.");
            }
        }
        else
        {
            _controller.MainViewModel.ShowNotification($"No font is selected to add to bucket");
        }
    }
}
