using System.Globalization;
using System.Windows.Data;

namespace MNIST.Converters;

public class FontStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Model.FontStyle style)
        {
            return style switch
            {
                FontStyle.Italic => System.Windows.FontStyles.Italic,
                _ => System.Windows.FontStyles.Normal
            };
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
