using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
namespace MNIST.Converters;
public class FontFamilyConverter : IValueConverter
{
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is FontModel model)
        {
            try
            {
                Uri uri = new(model.Path, UriKind.Absolute);
                GlyphTypeface glyph = new(uri);
                string name = glyph.FamilyNames.Values.FirstOrDefault();

                if (string.IsNullOrWhiteSpace(name) is false)
                {
                    string folder = Path.GetDirectoryName(model.Path);
                    uri = new Uri($"file:///{folder.Replace("\\", "/")}/");
                    FontFamily family = new(uri, $"./#{name}");
                    return family;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        return null;
    }
}
