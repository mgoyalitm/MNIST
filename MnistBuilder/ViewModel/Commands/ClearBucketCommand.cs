using System.Windows;

namespace MNIST.ViewModel.Commands;

public class ClearBucketCommand(FontController controller) : ICommand
{
    private FontController _controller = controller;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public bool CanExecute(object _) => true;

    public void Execute(object _)
    {
        MessageBoxResult result = MessageBox.Show("This action will empty the font bucket.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _controller.FontBucket.Clear();
        }
    }
}
