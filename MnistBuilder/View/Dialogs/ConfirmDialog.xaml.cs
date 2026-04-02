using System.Windows;

namespace MNIST.View.Dialogs
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public ConfirmDialog(string title, string body, string warning)
        {
            InitializeComponent();
            TitleBar.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            };
            Title = title;
            ContentText.Text = body;
            Warning.Text = warning;
        }
    }
}
