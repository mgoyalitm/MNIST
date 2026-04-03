namespace MNIST.ViewModel.Commands;

public class BulkImportCommand : ICommand, INotifyPropertyChanged
{
    private readonly SemaphoreSlim semaphore = new(1);
    private bool importing;

    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler CanExecuteChanged;

    public bool Importing
    {
        get => importing;
        set
        {
            if (value != importing)
            {
                importing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Importing)));
            }
        }
    }

    public bool CanExecute(object _) => Importing == false;
    public async void Execute(object _)
    {
        await semaphore.WaitAsync();
        Importing = true;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        await Task.Delay(10);

        FontStyle[] styles = [.. App.ViewModel.FilterViewModel?.SelectedFontStyles?.Cast<FontStyle>() ?? []];
        FontWeight[] weights = [.. App.ViewModel.FilterViewModel?.SelectedFontWeights?.Cast<FontWeight>() ?? []];
        FontCategory[] categories = [.. App.ViewModel.FilterViewModel?.SelectedFontCategories?.Cast<FontCategory>() ?? []];

        try
        {
            foreach (FontModel font in await Task.Run(() => App.ViewModel.AvailableFonts.Where(Condition)))
            {
                App.ViewModel.FontBucket.Add(font);
            }
        }
        finally
        {
            semaphore.Release();
            await Task.Delay(10);
            Importing = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool Condition(FontModel font)
        {
            if (styles.Length > 0 && styles.Contains(font.Style) is false)
            {
                return false;
            }

            if (weights.Length > 0 && weights.Contains(font.Weight) is false)
            {
                return false;
            }

            if (categories.Length > 0 && categories.Contains(font.Category) is false)
            {
                return false;
            }

            return App.ViewModel.FontBucket.FirstOrDefault(x => x.Path == font.Path) is null;
        }
    }
}
