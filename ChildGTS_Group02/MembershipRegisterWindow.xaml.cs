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
    /// Interaction logic for MembershipRegisterWindow.xaml
    /// </summary>
    public partial class MembershipRegisterWindow : Window
    {
        private MembershipPackageService _membershipPackageService = new();
        private bool _isFormValid;
        public bool IsFormValid
        {
            get => _isFormValid;
            set
            {
                _isFormValid = value;
                RegisterButton.IsEnabled = _isFormValid;
            }
        }
        public MembershipRegisterWindow()
        {
            InitializeComponent();
            DataContext = this;
            ValidateForm();
            LoadMembershipPackages();
        }

        private void ValidateForm()
        {
            IsFormValid = !string.IsNullOrWhiteSpace(EmailTextBox.Text) &&
                          !string.IsNullOrWhiteSpace(PasswordBox.Password) &&
                          !string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password) &&
                          !string.IsNullOrWhiteSpace(FullNameTextBox.Text) &&
                          !string.IsNullOrWhiteSpace(PhoneTextBox.Text) &&
                          !string.IsNullOrWhiteSpace(AddressTextBox.Text);
        }

        private void LoadMembershipPackages()
        {
            var packages = _membershipPackageService.GetAllMembershipPackages();

            if (packages.Count > 0)
            {
                var price1 = packages[0].Price > 0 ? $"{packages[0].Price:N0} VNĐ" : "Free";
                Package1TextBlock.Text = $"{packages[0].PackageName}\n{packages[0].Description}\nPrice: {price1}";
            }

            if (packages.Count > 1)
            {
                var price2 = packages[1].Price > 0 ? $"{packages[1].Price:N0} VNĐ" : "Free";
                Package2TextBlock.Text = $"{packages[1].PackageName}\n{packages[1].Description}\nPrice: {price2}";
            }
        }

        private Border _selectedPackageBorder;

        private void Package1Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectPackage(Package1Border);
        }

        private void Package2Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectPackage(Package2Border);
        }

        private void SelectPackage(Border selectedBorder)
        {
            // Reset the background color of previously selected border
            if (_selectedPackageBorder != null)
            {
                _selectedPackageBorder.Background = Brushes.White;
            }

            // Set the background color of the newly selected border
            selectedBorder.Background = Brushes.LightBlue;
            _selectedPackageBorder = selectedBorder;
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailErrorTextBlock.Text = "Email is required.";
                EmailErrorTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                EmailErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            ValidateForm();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordErrorTextBlock.Text = "Password is required.";
                PasswordErrorTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            ValidateForm();
        }

        private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                ConfirmPasswordErrorTextBlock.Text = "Confirm Password is required.";
                ConfirmPasswordErrorTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ConfirmPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            ValidateForm();
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


        private void FullNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                FullNameErrorTextBlock.Text = "Full Name is required.";
                FullNameErrorTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                FullNameErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            ValidateForm();
        }

        private void PhoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                PhoneErrorTextBlock.Text = "Phone is required.";
                PhoneErrorTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                PhoneErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            ValidateForm();
        }

        private void AddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressTextBox.Text))
            {
                AddressErrorTextBlock.Text = "Address is required.";
                AddressErrorTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                AddressErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            ValidateForm();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var password = PasswordTextBox.Visibility == Visibility.Visible ? PasswordTextBox.Text : PasswordBox.Password;
            var confirmPassword = ConfirmPasswordTextBox.Visibility == Visibility.Visible ? ConfirmPasswordTextBox.Text : ConfirmPasswordBox.Password;
            var fullName = FullNameTextBox.Text;
            var phone = PhoneTextBox.Text;
            var address = AddressTextBox.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            bool isTrial = _selectedPackageBorder == Package1Border ? true : false;
            int packageID = _selectedPackageBorder == Package1Border ? 1 : 2;
            decimal price = _selectedPackageBorder == Package1Border ? 0 : 49000;


            var userService = new UserService();
            if (!userService.RegisterUser(email, password, fullName, phone, address, isTrial, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = userService.GetAccount(email, password);
            if (user == null)
            {
                MessageBox.Show("Failed to retrieve user information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var creditCardWindow = new CreditCardWindow
            {
                UserID = user.UserId,
                PackageID = packageID,
                Price = price
            };
            creditCardWindow.ShowDialog();

            this.Close();
        }
    }
}
