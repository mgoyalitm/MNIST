using System.Windows;
namespace MNIST.ViewModel.Commands;
public class ClearBucketCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object _) => true;
    public void Execute(object _)
    {
        MessageBoxResult result = MessageBox.Show("This action will empty the font bucket.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            App.ViewModel.FontBucket.Clear();
        }
    }
}
