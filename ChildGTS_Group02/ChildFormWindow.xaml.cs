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
    /// Interaction logic for ChildFormWindow.xaml
    /// </summary>
    public partial class ChildFormWindow : Window
    {
        private readonly UserService _userService;

        private readonly ChildService _childService;
        public Child ResultChild { get; private set; }  // Đảm bảo ResultChild chứa đầy đủ thông tin của Child
        public bool IsEditMode { get; set; }
 

        public List<User> Parents { get; set; }

        public ChildFormWindow(Child existingChild = null)
        {
            InitializeComponent();
            _userService = new UserService();
            _childService = new ChildService();
            if (existingChild != null)
            {
                // Chế độ sửa: nếu có ChildId thì sẽ giữ lại và cập nhật
                IsEditMode = true;
                ResultChild = existingChild;  // Lưu lại Child đầy đủ, bao gồm ChildId

                // Gán các giá trị vào form để người dùng có thể chỉnh sửa
                FullNameTextBox.Text = existingChild.FullName;
                DateOfBirthPicker.SelectedDate = existingChild.DateOfBirth.ToDateTime(TimeOnly.MinValue);
                if (existingChild.Gender != null)
                {
                    var selectedGender = existingChild.Gender;
                    foreach (ComboBoxItem item in GenderComboBox.Items)
                    {
                        if (item.Content.ToString() == selectedGender)
                        {
                            GenderComboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
                BirthWeightTextBox.Text = existingChild.BirthWeight.ToString();
                BirthHeightTextBox.Text = existingChild.BirthHeight.ToString();
                ProfileImageTextBox.Text = existingChild.ProfileImage;
                MedicalNotesTextBox.Text = existingChild.MedicalNotes;
                ParentComboBox.SelectedValue = existingChild.ParentId;
            }
            else
            {
                // Nếu không có existingChild, khởi tạo một đối tượng Child mới
                ResultChild = new Child();
            }

            // Chỉ hiển thị Growth Record và nút Add khi ở chế độ Edit
            if (IsEditMode)
            {
                GrowthRecordWrapper.Visibility = Visibility.Visible;
            }
            else
            {
                GrowthRecordWrapper.Visibility = Visibility.Collapsed;
            }

            // Load danh sách phụ huynh
            Parents = GetParentsFromDatabase();
            ParentComboBox.ItemsSource = Parents;
            ParentComboBox.SelectedValuePath = "UserId";
            ParentComboBox.DisplayMemberPath = "FullName";

            LoadGrowthRecordsWithDoctors(existingChild?.ChildId ?? 0);
        }


        private async void LoadGrowthRecordsWithDoctors(int childId)
        {
            // Lấy tất cả GrowthRecords liên quan đến ChildId
            var growthRecords = await _childService.GetChildRelatedData(childId);

            // Duyệt qua tất cả các GrowthRecord và thêm tên bác sĩ từ DataShare
            var growthRecordsWithDoctor = new List<object>();  // Tạo danh sách tạm để chứa kết quả

            foreach (var gr in growthRecords)
            {
                var recordedById = gr.RecordedBy ?? 2;  // Gán mặc định là 2 nếu null
                var doctor = await _userService.GetUserByIdAsync(recordedById);  // Sử dụng phương thức bất đồng bộ

                var doctorName = doctor?.FullName ?? "Unknown Doctor";  // Kiểm tra null nếu doctor không tồn tại

                growthRecordsWithDoctor.Add(new
                {
                    gr.RecordDate,
                    gr.Height,
                    gr.Weight,
                    gr.HeadCircumference,
                    gr.MeasurementType,
                    gr.Notes,
                    DoctorName = doctorName
                });
            }

            // Gán nguồn dữ liệu cho DataGrid
            GrowthRecordDataGrid.ItemsSource = growthRecordsWithDoctor;
        }


        private List<User> GetParentsFromDatabase()
        {
            return _userService.GetAllUsers();
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResultChild.FullName = FullNameTextBox.Text;
                ResultChild.DateOfBirth = DateOnly.FromDateTime(DateOfBirthPicker.SelectedDate ?? DateTime.Today);
                ResultChild.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                ResultChild.BirthWeight = decimal.TryParse(BirthWeightTextBox.Text, out var weight) ? weight : (decimal?)null;
                ResultChild.BirthHeight = decimal.TryParse(BirthHeightTextBox.Text, out var height) ? height : (decimal?)null;
                ResultChild.ProfileImage = ProfileImageTextBox.Text;
                ResultChild.MedicalNotes = MedicalNotesTextBox.Text;
                ResultChild.ParentId = (int)(ParentComboBox.SelectedValue ?? 0);
                ResultChild.LastCheckupDate = DateTime.Now;

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}");
            }
        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void AddGrowthbtn_Click(object sender, RoutedEventArgs e)
        {

            GrowthDataShareWindow growthDataShareWindow = new GrowthDataShareWindow(ResultChild.ChildId);

            // Mở dưới dạng hộp thoại (blocking, chờ đến khi đóng)
            bool? result = growthDataShareWindow.ShowDialog();

            // Nếu người dùng đã thêm xong và đóng cửa sổ
            if (result == true)
            {
                LoadGrowthRecordsWithDoctors(ResultChild.ChildId);
            }
        }

        
    }
}
