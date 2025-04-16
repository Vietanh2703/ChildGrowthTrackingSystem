using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChildGTS_Group02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void FAQButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("FAQ: Frequently Asked Questions\n\n1. How to use the system?\n2. How to add a child?\n3. How to contact support?",
                                        "FAQ",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
        }
    }
}