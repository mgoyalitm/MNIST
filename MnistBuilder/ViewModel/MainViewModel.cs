namespace MNIST.ViewModel;
public class MainViewModel : INotifyPropertyChanged
{
    private string font_directory = @"C:\Repositories\google-fonts";
    private string statusMessage;

    public event PropertyChangedEventHandler PropertyChanged;

    public MainViewModel()
    {
        FontController = new(this);

        action();
        async void action() 
        { 
            await FontController.InitializeFontsAsync(font_directory); 
        }
    }

    public FontController FontController { get; }


    public string StatusMessage
    {
        get => statusMessage;
        set
        {
            if (value != statusMessage)
            {
                statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
    }


    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public void ShowNotification(string message)
    {
    }
}
