using ChildGTS_Group02.BLL.Services;
using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreditCardWindow.xaml
    /// </summary>
    public partial class CreditCardWindow : Window
    {
        public int UserID { get; set; }
        public int PackageID { get; set; }
        public decimal Price { get; set; }
        private PaymentService _paymentService = new();
        public CreditCardWindow()
        {
            InitializeComponent();
        }
        private void SerialNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex(@"^\d{4}(-\d{4}){3}$");
            if (!regex.IsMatch(SerialNumberTextBox.Text))
            {
                SerialNumberTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                SerialNumberTextBox.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }
        private void CardholderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CardholderTextBox.Text))
            {
                CardholderTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                CardholderTextBox.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }
        private void CardholderTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z]+$");
        }

        private void ExpirationDateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9/]");
        }

        private void ExpirationDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var regex = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$");
            if (!regex.IsMatch(ExpirationDateTextBox.Text))
            {
                ExpirationDateTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                ExpirationDateTextBox.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }

        private void CVVTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }

        private void CVVTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CVVTextBox.Text.Length > 3)
            {
                CVVTextBox.Text = CVVTextBox.Text.Substring(0, 3);
                CVVTextBox.CaretIndex = CVVTextBox.Text.Length;
            }

            if (CVVTextBox.Text.Length == 3)
            {
                CVVTextBox.BorderBrush = System.Windows.Media.Brushes.Green;
            }
            else
            {
                CVVTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }
        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (SerialNumberTextBox.BorderBrush == System.Windows.Media.Brushes.Red ||
        CardholderTextBox.BorderBrush == System.Windows.Media.Brushes.Red ||
        ExpirationDateTextBox.BorderBrush == System.Windows.Media.Brushes.Red ||
        CVVTextBox.BorderBrush == System.Windows.Media.Brushes.Red)
            {
                MessageBox.Show("Please correct the highlighted fields before proceeding.", "Information Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var serialNumber = SerialNumberTextBox.Text;
            var cardholder = CardholderTextBox.Text;
            var expirationDate = ExpirationDateTextBox.Text;
            var cvv = CVVTextBox.Text;

            if (string.IsNullOrWhiteSpace(serialNumber) || string.IsNullOrWhiteSpace(cardholder) ||
                string.IsNullOrWhiteSpace(expirationDate) || string.IsNullOrWhiteSpace(cvv))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var paymentID = _paymentService.GetNextPaymentID();
            var transactionID = $"TX-{paymentID:D3}";
            var invoiceNumber = $"INV-{paymentID:D3}";

            var payment = new Payment
            {
                UserId = this.UserID,
                PackageId = this.PackageID,
                Amount = this.Price,
                PaymentMethod = "Credit Card",
                Status = "Completed",
                TransactionId = transactionID,
                InvoiceNumber = invoiceNumber,
                TransactionDate = DateTime.Now
            };

            var success = _paymentService.AddPayment(payment);

            if (success)
            {
                var userService = new UserService();
                var user = userService.GetUserById(this.UserID);

                var packageService = new MembershipPackageService();
                var package = packageService.GetPackageById(this.PackageID);
                var invoiceWindow = new InvoiceWindow(payment, user.FullName, package.PackageName, package.Description);
                invoiceWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Purchase failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

