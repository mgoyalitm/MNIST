using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MNIST.Converters;

public class FontWeightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Model.FontWeight weight)
        {
            return weight switch
            {
                Model.FontWeight.Thin => FontWeights.Thin,
                Model.FontWeight.ExtraLight => FontWeights.ExtraLight,
                Model.FontWeight.Light => FontWeights.Light,
                Model.FontWeight.Normal => FontWeights.Normal,
                Model.FontWeight.Medium => FontWeights.Medium,
                Model.FontWeight.SemiBold => FontWeights.SemiBold,
                Model.FontWeight.Bold => FontWeights.Bold,
                Model.FontWeight.ExtraBold=> FontWeights.ExtraBold,
                _ => FontWeights.Black
            };
        }

        return FontWeights.Normal;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
