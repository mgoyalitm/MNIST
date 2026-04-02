using System.Windows;
namespace MNIST.ViewModel.Commands;
public class ClearBucketCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object _) => true;
    public void Execute(object _)
    {
        if (App.ViewModel.FontBucket.Count < 1)
        {
            return;
        }

        if (App.GetConfirmation("Confirm", "This will permanently delete all fonts in the font bucket.", "Do you want to continue?") is true)
        {
            App.ViewModel.FontBucket.Clear();
        }
    }
}
