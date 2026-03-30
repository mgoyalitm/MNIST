using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MNIST.Converters;

public class FontWeightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int weight)
        {
            return weight switch
            {
                <= 150 => FontWeights.Thin,
                <= 250 => FontWeights.ExtraLight,
                <= 350 => FontWeights.Light,
                <= 450 => FontWeights.Normal,
                <= 550 => FontWeights.Medium,
                <= 650 => FontWeights.SemiBold,
                <= 750 => FontWeights.Bold,
                <= 850 => FontWeights.ExtraBold,
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
