using ChildGTS_Group02.BLL.Services;
using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChildGTS_Group02
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public User? User { get; set; }
        private UserService _userService = new();
        private ChildService _childService = new();
        private PaymentService _paymentService = new();

        public AdminWindow()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            int totalUsers = _userService.GetAllUsers().Count;
            TotalUsersTextBlock.Text = totalUsers.ToString();

            int totalChildren = _childService.GetAllChildren().Count;
            TotalChildrenTextBlock.Text = totalChildren.ToString();

            decimal totalRevenue = _paymentService.GetAllPayments().Sum(p => p.Amount);
            TotalRevenueTextBlock.Text = $"{totalRevenue:N0} VNĐ";
        }

        private void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            LoadDataToGrid();
            HelloMessageLabel.Content = "Hello " + User.FullName + " !";
        }

        private void LoadDataToGrid()
        {
            UsersDataGrid.ItemsSource = null;
            UsersDataGrid.ItemsSource = _userService.GetAllUsersByRoleId(2);
        }

        private void CreateDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Create Doctor button clicked.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a doctor to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Update Doctor button clicked.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a doctor to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Delete Doctor button clicked.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadDataToGrid();
                return;
            }
            var filteredUsers = _userService.SearchUsersByRoleId(2, searchText);
            UsersDataGrid.ItemsSource = filteredUsers;
        }
    }
}
