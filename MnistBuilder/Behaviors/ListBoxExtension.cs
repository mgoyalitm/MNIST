using System.Windows;
using System.Windows.Controls;

namespace MNIST.Behaviors;
public class ListBoxExtension : DependencyObject
{
    public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.RegisterAttached("IsMultiSelect", typeof(bool), typeof(ListBoxExtension), new PropertyMetadata(false, OnMultiSelectChanged));
    public static readonly DependencyProperty SelectionsProperty = DependencyProperty.RegisterAttached("Selections", typeof(IList), typeof(ListBoxExtension), new PropertyMetadata(null));
    public static IList GetSelections(ListBox obj) => (IList)obj.GetValue(SelectionsProperty);
    public static void SetSelections(ListBox obj, IList value) => obj.SetValue(SelectionsProperty, value);
    public static bool GetIsMultiSelect(ListBox obj) => (bool)obj.GetValue(IsMultiSelectProperty);
    public static void SetIsMultiSelect(ListBox obj, bool value) => obj.SetValue(IsMultiSelectProperty, value);

    private static async void OnMultiSelectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListBox listBox)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            if (e.NewValue is bool boolean)
            {
                if (boolean is true)
                {
                    await Task.Delay(50);
                    if (GetSelections(listBox) is IList selection)
                    {
                        foreach (object item in selection)
                        {
                            listBox.SelectedItems.Add(item);
                        }
                    }

                    listBox.SelectionChanged += SelectionChanged;
                }
                else
                {
                    SetSelections(listBox, null);
                    listBox.SelectionChanged -= SelectionChanged;
                }
            }

            static void SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (sender is ListBox listBox)
                {
                    IList selection = listBox.SelectedItems;
                    SetSelections(listBox, selection);
                }
            }
        }
    }
}
