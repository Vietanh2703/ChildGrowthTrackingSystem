using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        public InvoiceWindow(Payment payment, string userFullName, string packageName, string packageDescription)
        {
            InitializeComponent();

            // Bind payment details to the UI
            DataContext = new
            {
                InvoiceNumber = payment.InvoiceNumber,
                TransactionId = payment.TransactionId,
                TransactionDate = payment.TransactionDate?.ToString("g", CultureInfo.CurrentCulture), 
                UserFullName = userFullName,
                PackageName = packageName,
                PackageDescription = packageDescription,
                Amount = $"{payment.Amount:N0} VNĐ",
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
