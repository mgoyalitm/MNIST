using System.Globalization;
using System.Windows.Data;

namespace MNIST.Converters;

public class ContentToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value.ToString();
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.ToString();
}
