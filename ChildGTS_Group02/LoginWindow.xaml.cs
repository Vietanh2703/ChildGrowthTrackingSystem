using ChildGTS_Group02.BLL.Services;
using ChildGTS_Group02.DAL.Entities;
using System.Windows;

namespace ChildGTS_Group02
{
    public partial class LoginWindow : Window
    {
        private UserService _userService = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void CheckBoxHide_Checked(object sender, RoutedEventArgs e)
        {
            PasswordShowBox.Text = PasswordHiddenBox.Password;
            PasswordShowBox.Visibility = Visibility.Visible;
            PasswordHiddenBox.Visibility = Visibility.Collapsed;
        }

        private void CheckBoxHide_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordHiddenBox.Password = PasswordShowBox.Text;
            PasswordHiddenBox.Visibility = Visibility.Visible;
            PasswordShowBox.Visibility = Visibility.Collapsed;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordHiddenBox.Visibility == Visibility.Visible ? PasswordHiddenBox.Password : PasswordShowBox.Text;

            User? user = _userService.GetAccount(email, password);
            if (user == null)
            {
                MessageBox.Show("Invalid email or password", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(user.RoleId == 1)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            }
            else if (user.RoleId == 2)
            {
                DoctorWindow doctorWindow = new DoctorWindow();
                doctorWindow.Show();
                this.Close();
            }
            else if (user.RoleId == 3)
            {
                MemberWindow memberWindow = new MemberWindow();
                memberWindow.Show();
                this.Close();
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
