namespace MNIST.ViewModel.Commands;

public class BulkRemoveCommand : ICommand
{
    private readonly SemaphoreSlim semaphore = new(1);
    public event EventHandler CanExecuteChanged { add { } remove { } }

    public bool CanExecute(object _) => true;

    public async void Execute(object parameter)
    {
        await semaphore.WaitAsync();
        using SemaphoreRelease release = new(semaphore);
        
        if (App.GetConfirmation("Confirm", "You are about to remove all fonts in the font bucket that match the current filter.", "Do you want to continue?") is false)
        {
            return;
        }

        FontStyle[] styles = [.. App.ViewModel.FilterViewModel?.SelectedFontStyles?.Cast<FontStyle>() ?? []];
        FontWeight[] weights = [.. App.ViewModel.FilterViewModel?.SelectedFontWeights?.Cast<FontWeight>() ?? []];
        FontCategory[] categories = [.. App.ViewModel.FilterViewModel?.SelectedFontCategories?.Cast<FontCategory>() ?? []];

        FontModel[] bucket = [.. App.ViewModel.FontBucket];

        foreach (FontModel font in await Task.Run(() => bucket.Where(Condition)))
        {
            App.ViewModel.FontBucket.Remove(font);
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

            return true;
        }

    }
}
