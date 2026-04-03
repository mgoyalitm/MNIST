namespace MNIST.ViewModel.Commands;
public class FilterCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }

    public bool CanExecute(object _) => true;
    public void Execute(object _) => App.ShowFilters();
}
