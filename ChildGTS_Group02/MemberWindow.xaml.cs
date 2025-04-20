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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {

        private ChildService _childService = new();
        private HealthAlertService _healthAlertService = new();
        private GrowRecordService _growRecordService = new();
        public User? User { get; set; }

        public MemberWindow()
        {
            InitializeComponent();
        }

        private void MemberWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int userId = User.UserId;
            LoadChildren(userId);
            HelloMessageLabel.Content = "Hello " + User.FullName + " !";
        }

        private void LoadChildren(int userId)
        {
            ChildrenDataGrid.ItemsSource = null;
            ChildrenDataGrid.ItemsSource = _childService.GetAllChildrenByUserId(userId);
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            int userId = User.UserId;
            var childForm = new ChildFormWindow(userId);
            childForm.ShowDialog();
            LoadChildren(userId);
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

            int userId = User.UserId;
            var selectedChild = ChildrenDataGrid.SelectedItem as Child; ;  

            if(selectedChild == null)
            {
                MessageBox.Show("Choose children,plz.");
                return;
            }
            ChildFormWindow childForm = new(userId);
            childForm.IsEditMode = selectedChild;
            childForm.ShowDialog();
            LoadChildren(userId);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedChild = ChildrenDataGrid.SelectedItem as Child;
            int userId = User.UserId;

            if (selectedChild != null)
            {
                var result = MessageBox.Show("You wanna delete this child?", "Confirm,plz", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var healthAlerts = _healthAlertService.GetAllHealthAlertsByChildId(selectedChild.ChildId);
                        foreach (var alert in healthAlerts)
                        {
                            _healthAlertService.Delete(alert);
                        }
                        var growthRecords = _growRecordService.GetGrowthRecordsByChildId(selectedChild.ChildId);
                        foreach (var record in growthRecords)
                        {
                            _growRecordService.Delete(record);
                        }
                        _childService.Delete(selectedChild);

                        MessageBox.Show("Child deleted successfully.");
                        LoadChildren(userId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose children,plz.");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadChildren(User.UserId);
                return;
            }
            var searchResults = _childService.Search(searchText,User.UserId);
            ChildrenDataGrid.ItemsSource = searchResults;

        }

        private void BtnViewDoctorFeedback_Click(object sender, RoutedEventArgs e)
        {
            DoctorFeedbackRatingWindow x = new();
            x.User = User;
            x.ShowDialog();

        }
    }
}
