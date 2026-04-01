namespace MNIST.ViewModel.Commands;
public class AddFontToBucketCommand: ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object parameter) => parameter is FontModel;
    public void Execute(object parameter)
    {
        if (parameter is FontModel font)
        {
            if (App.ViewModel.FontBucket.FirstOrDefault(x => x.Path == font.Path) is null)
            {
                App.ViewModel.FontBucket.Add(font);
            }
        }
    }
}
