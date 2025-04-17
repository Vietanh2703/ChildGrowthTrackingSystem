using ChildGTS_Group02.BLL.Services;
using ChildGTS_Group02.DAL.Entities;
using System.Collections.ObjectModel;
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
        private BlogService _blogService = new();
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

        private void BlogMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToGrid();
        }

        private void BlogDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedBlog = BlogDataGrid.SelectedItem as Blog;
            if (selectedBlog != null)
            {
                BlogDetailWindow blogDetailWindow = new BlogDetailWindow(selectedBlog);
                blogDetailWindow.ShowDialog();
            }
        }

        private void LoadDataToGrid()
        {
            BlogDataGrid.ItemsSource = null;
            BlogDataGrid.ItemsSource = _blogService.GetAllBlogs();
        }

        private void BecomeMemberButton_Click(object sender, RoutedEventArgs e)
        {
            MembershipRegisterWindow membershipRegisterWindow = new MembershipRegisterWindow();
            membershipRegisterWindow.ShowDialog();
        }
    }
}