using System.Windows;
namespace MNIST.View;
public partial class FilterWindow : Window
{
    public FilterWindow()
    {
        InitializeComponent();
        TitleBar.MouseDown += (s, e) =>
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        };
    }
}
