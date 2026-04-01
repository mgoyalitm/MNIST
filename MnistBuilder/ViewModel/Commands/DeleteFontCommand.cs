namespace MNIST.ViewModel.Commands;

public class DeleteFontCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }

    public bool CanExecute(object parameter) => parameter is FontModel;

    public void Execute(object parameter)
    {
        if (parameter is FontModel font && App.ViewModel.FontBucket.FirstOrDefault(x => x.Path == font.Path) is FontModel target)
        {
            App.ViewModel.FontBucket.Remove(target);
        }
    }
}
