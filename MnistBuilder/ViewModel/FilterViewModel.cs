namespace MNIST.ViewModel;

public class FilterViewModel : INotifyPropertyChanged
{
    private IList selectedFontStyles;
    private IList selectedFontWeights;
    private IList selectedFontCategories;
    private bool showFilterResults;

    public event PropertyChangedEventHandler PropertyChanged;
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public IEnumerable<FontStyle> FontStyles => [FontStyle.Regular, FontStyle.Italic];

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public IEnumerable<FontWeight> FontWeights => [FontWeight.Thin, FontWeight.ExtraLight, FontWeight.Light, FontWeight.Normal, FontWeight.Medium, FontWeight.SemiBold, FontWeight.Bold, FontWeight.ExtraBold, FontWeight.Black];

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public IEnumerable<FontCategory> FontCategories => [FontCategory.Sans_Serif, FontCategory.Serif, FontCategory.Monospace, FontCategory.Display, FontCategory.Handwriting];

    public IList SelectedFontStyles
    {
        get => selectedFontStyles;
        set
        {
            if (value != selectedFontStyles)
            {
                selectedFontStyles = value;
                OnPropertyChange(nameof(SelectedFontStyles));
            }
        }
    }

    public IList SelectedFontWeights
    {
        get => selectedFontWeights;
        set
        {
            if (value != selectedFontWeights)
            {
                selectedFontWeights = value;
                OnPropertyChange(nameof(SelectedFontWeights));
            }
        }
    }

    public IList SelectedFontCategories
    {
        get => selectedFontCategories;
        set
        {
            if (value != selectedFontCategories)
            {
                selectedFontCategories = value;
                OnPropertyChange(nameof(SelectedFontCategories));
            }
        }
    }

    public bool ShowFilteredResults
    {
        get => showFilterResults;
        set
        {
            if (value != showFilterResults)
            {
                showFilterResults = value;
                OnPropertyChange(nameof(ShowFilteredResults));
            }
        }
    }

    private void OnPropertyChange(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
