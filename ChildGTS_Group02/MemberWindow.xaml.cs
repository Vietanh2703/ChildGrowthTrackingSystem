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

        private readonly ChildService _childService;
        public User? User { get; set; }
        public MemberWindow()
        {
            InitializeComponent();
            _childService = new ChildService();
            LoadChildren();
        }
      
        private async Task LoadChildren()
        {
            try
            {
                var children = await _childService.GetAllChildrenAndParent();
                ChildrenDataGrid.ItemsSource = children;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load children data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            var childForm = new ChildFormWindow();  // Mở form thêm mới

            bool? result = childForm.ShowDialog();  // Hiển thị form dưới dạng modal (đợi người dùng đóng)

            if (result == true)
            {
                 
                Child newChild = childForm.ResultChild;

            await  _childService.CreateChildren(newChild);

               
                LoadChildren();
            }
        }

        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
 
            var selectedChild = ChildrenDataGrid.SelectedItem as Child; ;  

            if (selectedChild != null)
            {
                var childForm = new ChildFormWindow(selectedChild);  

                bool? result = childForm.ShowDialog();  

                if (result == true)
                {
              
                    Child updatedChild = childForm.ResultChild;

                  
                    await _childService.UpdateChildren(updatedChild);

                   
                    LoadChildren();
                }

                else 
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("Choose children, you want to update.");
            }
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedChild = ChildrenDataGrid.SelectedItem as Child;

            if (selectedChild != null)
            {
                var result = MessageBox.Show("You wanna delete this child?", "Confirm,plz", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _childService.DeleteChildren(selectedChild.ChildId); // Sửa lại tên hàm

                        // Làm mới DataGrid sau khi xóa
                        LoadChildren();
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

    }
}
