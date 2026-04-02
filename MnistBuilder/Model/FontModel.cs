using System.Windows;

namespace MNIST.Model;
public record FontModel(string Name, string Path, FontStyle Style, FontCategory Category, FontWeight Weight)
{
    public override string ToString() => $"{Name}, {Style}, {Category.ToString().Replace("_", "-")}, {Weight}";
}

