namespace MNIST.ViewModel.Commands;
public class SelectFontCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object parameter) => parameter is string;
    public void Execute(object parameter)
    {
        if (parameter is string font_path)
        {
            FontModel target = App.ViewModel.AvailableFonts.FirstOrDefault(x => x.Path == font_path);
            
            if (App.ViewModel.AvailableFonts?.IndexOf(target) is int index && index != -1)
            {
                App.ViewModel.SelectedFontIndex = index;
            }
        }
    }
}
