using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;

namespace MNIST.Behaviors;
public class AutoScrollToEndBehavior : Behavior<ScrollViewer>
{
    protected override void OnAttached()
    {
        AssociatedObject.ScrollChanged += OnScrollChanged;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.ScrollChanged -= OnScrollChanged;
    }

    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (e.ExtentHeightChange != 0)
        {
            AssociatedObject.ScrollToEnd();
        }
    }
}