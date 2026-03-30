using System.Windows;

namespace MNIST.Model;
public record FontModel(string Name, string Path, FontStyle Style, FontCategory Category, int Weight)
{
    public override string ToString()
    {
        string category = Category switch
        {
            FontCategory.Sans_Serif => "Sans-Serif",
            FontCategory.Serif => "Serif",
            FontCategory.Handwriting => "Handwriting",
            FontCategory.Display => "Display",
            FontCategory.Monospace => "Monospace",
            FontCategory.Undefined or _ => "Unknow N/A",
        };

        string weight = Weight switch
            {
                <= 150 => "Thin",
                <= 250 => "ExtraLight",
                <= 350 => "Light",
                <= 450 => "Normal",
                <= 550 => "Medium",
                <= 650 => "SemiBold",
                <= 750 => "Bold",
                <= 850 => "ExtraBold",
                _ => "Black"
            };

        return $"{Name}, {Style}, {category}, {weight}";
    }
}

