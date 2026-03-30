namespace MNIST.ViewModel.Commands;

public class GenerateMNISTCommand(FontController controller) : IProgress<int>, ICommand, INotifyPropertyChanged
{
    private readonly FontController _controller = controller;
    
    private bool isRunning;
    private int total;
    private int current;
    private string directory = @"C:\Repositories\mnist-data";
    
    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler CanExecuteChanged;

    public bool IsExecuting
    {
        get => isRunning;
        set
        {
            if (value != isRunning)
            {
                isRunning = value;
                OnPropertyChanged(nameof(IsExecuting));
            }
        }
    }

    public int Current
    {
        get => current;
        set
        {
            if (value != current)
            {
                current = value;
                OnPropertyChanged(nameof(Current));
            }
        }
    }

    public int Total
    {
        get => total;
        set
        {
            if (value != total)
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
    }

    public bool CanExecute(object _) => IsExecuting == false;
    public async void Execute(object _)
    {
        try
        {
            FontModel[] fonts = [.. _controller.FontBucket];
            Current = 0;
            Total = fonts.Length * FontManager.CharacterCount * FontManager.RotationSteps;
            IsExecuting = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            await FontManager.WriteMNISTAsync(directory,fonts, this);
        }
        finally
        {
            await Task.Delay(1000);
            IsExecuting = false;
            Current = 0;
            Total = 0;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new(propertyName));

    public void Report(int value) => Current = value;
}
