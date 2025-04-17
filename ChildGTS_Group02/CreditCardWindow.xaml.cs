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

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            // Collect credit card information
            var serialNumber = SerialNumberTextBox.Text;
            var cardholder = CardholderTextBox.Text;
            var expirationDate = ExpirationDateTextBox.Text;
            var cvv = CVVTextBox.Text;

            // Validate input (basic validation)
            if (string.IsNullOrWhiteSpace(serialNumber) || string.IsNullOrWhiteSpace(cardholder) ||
                string.IsNullOrWhiteSpace(expirationDate) || string.IsNullOrWhiteSpace(cvv))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Generate TransactionID and InvoiceNumber
            var paymentID = _paymentService.GetNextPaymentID();
            var transactionID = $"TX-{paymentID:D3}";
            var invoiceNumber = $"INV-{paymentID:D3}";

            // Save payment information
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

                // Open Invoice Window
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

