namespace MNIST.ViewModel;
public class FontController : INotifyPropertyChanged
{
    private FontModel[] availableFonts = [];
    private int selectedFontIndex;
    private readonly SemaphoreSlim semaphoreInitialize = new(1);
    private readonly SemaphoreSlim semaphoreGenerate = new(1);

    public event PropertyChangedEventHandler PropertyChanged;

    public FontController(MainViewModel viewModel)
    {
        MainViewModel = viewModel;
        FontBucket = [];
        ChangeFontCommand = new ChangeFontCommand(this);
        AddFontToBucketCommand = new AddFontToBucket(this);
        DeleteFontCommand = new DeleteFontCommand(this);
        ClearBucketCommand = new ClearBucketCommand(this);
        GenerateMNISTCommand = new GenerateMNISTCommand(this);
    }

    public MainViewModel MainViewModel { get; }
    public ICommand ChangeFontCommand { get; }
    public ICommand AddFontToBucketCommand { get; }
    public ICommand DeleteFontCommand { get; }
    public ICommand ClearBucketCommand { get; }
    public ICommand GenerateMNISTCommand { get; }

    public int SelectedFontIndex
    {
        get => selectedFontIndex;
        set
        {
            value = availableFonts.Length == 0 ? 0 : value % availableFonts.Length;
            
            if (value < 0)
            {
                value = availableFonts.Length < 1 ? 0 : value + availableFonts.Length;
            }

            if (value != selectedFontIndex)
            {
                selectedFontIndex = value;
                OnPropertyChanged(nameof(SelectedFontIndex));
                OnPropertyChanged(nameof(SerialNumber));
                OnPropertyChanged(nameof(SelectedFont));
            }
        }
    }

    public int SerialNumber => SelectedFontIndex + 1;
    public int FontsCountTotal => availableFonts.Length;

    public FontModel[] AvailableFonts
    {
        get => availableFonts;
        set
        {
            if (value != availableFonts)
            {
                availableFonts = value;
                OnPropertyChanged(nameof(AvailableFonts));
                OnPropertyChanged(nameof(FontsCountTotal));
            }
        }
    }

    public FontModel SelectedFont => availableFonts.Length == 0 ? null : availableFonts[selectedFontIndex];

    public ObservableCollection<FontModel> FontBucket { get; }

    public async Task InitializeFontsAsync(string directory, CancellationToken cancellationToken = default)
    {
        try
        {
            await semaphoreInitialize.WaitAsync(cancellationToken);
            
            List<FontModel> fonts = [];
            
            await foreach (FontModel font in FontManager.DiscoverFontsAsync(directory, cancellationToken))
            {
                fonts.Add(font);
            }

            AvailableFonts = [.. fonts.OrderBy(x => x.Name)];
            SelectedFontIndex = 0;
            await Task.Delay(10, cancellationToken);
            OnPropertyChanged(nameof(SelectedFont));
        }
        catch (Exception)
        {
            MainViewModel.StatusMessage = $"Failed to load font repository: {directory}";
        }
        finally
        {
            semaphoreInitialize.Release();
        }
    }


    private void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
