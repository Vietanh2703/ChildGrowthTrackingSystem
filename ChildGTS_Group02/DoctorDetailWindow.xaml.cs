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
    /// Interaction logic for DoctorDetailWindow.xaml
    /// </summary>
    public partial class DoctorDetailWindow : Window
    {
        private UserService _userService = new();
        private DoctorPositionService _doctorPositionService = new();
        public User? User { get; set; }
        public User? EditedDoctor { get; set; } = null;
        private bool _isFormValid;
        public bool IsFormValid
        {
            get => _isFormValid;
            set
            {
                _isFormValid = value;
                SaveButton.IsEnabled = _isFormValid;
            }
        }
        public DoctorDetailWindow(User user)
        {
            InitializeComponent();
            User = user;
        }

     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PositionIdComboBox.ItemsSource = _doctorPositionService.GetAllDoctorPositions();
            PositionIdComboBox.DisplayMemberPath = "PositionName";
            PositionIdComboBox.SelectedValuePath = "PositionId";
            PositionIdComboBox.SelectedValue = 1;

            DoctorModeLabel.Content = "Create new doctor";
            PasswordLabel.Content = "Password";

            if (EditedDoctor != null)
            {
                DoctorModeLabel.Content = "Edit doctor";
                PasswordLabel.Content = "New password";
                DoctorCodeTextBox.Text = EditedDoctor.DoctorCode.ToString();
                DoctorCodeTextBox.IsEnabled = false;
                DoctorNameTextBox.Text = EditedDoctor.FullName;
                EmailTextBox.Text = EditedDoctor.Email;
                PasswordBox.Password = EditedDoctor.Password;
                PasswordTextBox.Text = EditedDoctor.Password;
                PhoneTextBox.Text = EditedDoctor.Phone.ToString();
                AddressTextBox.Text = EditedDoctor.Address;
                PositionIdComboBox.SelectedValue = EditedDoctor.PositionId;
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            // Show the TextBox and hide the PasswordBox for Password
            PasswordTextBox.Text = PasswordBox.Password;
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;

            // Show the TextBox and hide the PasswordBox for Confirm Password
            ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
            ConfirmPasswordTextBox.Visibility = Visibility.Visible;
            ConfirmPasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Hide the TextBox and show the PasswordBox for Password
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;

            // Hide the TextBox and show the PasswordBox for Confirm Password
            ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
            ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
            ConfirmPasswordBox.Visibility = Visibility.Visible;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var doctorCode = DoctorCodeTextBox.Text;
            var email = EmailTextBox.Text;
            var password = PasswordBox.Password;
            var fullName = DoctorNameTextBox.Text;
            var phone = PhoneTextBox.Text;
            var address = AddressTextBox.Text;
            var positionId = int.Parse(PositionIdComboBox.SelectedValue.ToString());
            if (EditedDoctor == null)
                _userService.CreateDoctor(doctorCode, email, password, fullName, phone, address, positionId);
            else
                _userService.UpdateDoctor(EditedDoctor.UserId, doctorCode, email, password, fullName, phone, address, positionId);

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
