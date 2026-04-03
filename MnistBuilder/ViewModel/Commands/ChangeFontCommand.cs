namespace MNIST.ViewModel.Commands;
public class ChangeFontCommand : ICommand
{
    public event EventHandler CanExecuteChanged { add { } remove { } }
    public bool CanExecute(object parameter) => parameter is int;

    public async void Execute(object parameter)
    {
        if (parameter is int increment)
        {
            if (App.ViewModel.FilterViewModel.ShowFilteredResults == false)
            {
                App.ViewModel.SelectedFontIndex += increment;
                return;
            }

            int step = Math.Sign(increment);

            if (step == 0) return;

            int current_index = App.ViewModel.SelectedFontIndex;
            int threshold_index = current_index + App.ViewModel.FontsCountTotal * step;
            int index = current_index + step;

            int min = Math.Min(current_index, threshold_index);
            int max = Math.Max(current_index, threshold_index);

            while (index > min && index < max)
            {
                int actual_index = (index + 2 * App.ViewModel.FontsCountTotal) % App.ViewModel.FontsCountTotal;
                FontModel font = App.ViewModel.AvailableFonts[actual_index];

                if (FilterCheck(font))
                {
                    App.ViewModel.SelectedFontIndex = actual_index;
                    return;
                }

                index += step;
            }
            
        }
    }

    private static bool FilterCheck(FontModel font)
    {
        FontStyle[] styles = [.. App.ViewModel.FilterViewModel?.SelectedFontStyles?.Cast<FontStyle>() ?? []];
        FontWeight[] weights = [.. App.ViewModel.FilterViewModel?.SelectedFontWeights?.Cast<FontWeight>() ?? []];
        FontCategory[] categories = [.. App.ViewModel.FilterViewModel?.SelectedFontCategories?.Cast<FontCategory>() ?? []];

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
