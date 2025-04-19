using ChildGTS_Group02.BLL.Services;
using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChildGTS_Group02
{
    public partial class GrowthDataShareWindow : Window
    {
        private readonly GrowRecordService _growRecordService;
        private readonly DataShareService _dataShareService;
        private readonly UserService _userService;

        private readonly DataShareReposirory _dataShareReposirory;
        private int _chidid;
        public GrowthDataShareWindow(int id)
        {
            InitializeComponent();
            _chidid = id;
            _growRecordService = new GrowRecordService();
            _dataShareService = new DataShareService();
            _userService = new UserService();
            _dataShareReposirory = new DataShareReposirory();
            // Get doctors and populate ComboBox
            var doctors = _userService.GetDoctors(); // Ensure this is a method that gets doctors
            DoctorNameComboBox.ItemsSource = doctors;
            DoctorNameComboBox.DisplayMemberPath = "FullName";  // Hiển thị tên bác sĩ
            DoctorNameComboBox.SelectedValuePath = "UserId";  // Lưu giá trị UserId làm giá trị của ComboBox
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            
            try
            {
                // Lấy dữ liệu từ các trường nhập liệu
                var height = decimal.Parse(HeightBox.Text);
                var weight = decimal.Parse(WeightBox.Text);
                var headCircumference = decimal.TryParse(HeadBox.Text, out var hc) ? hc : (decimal?)null;
                var bmi = decimal.TryParse(BmiBox.Text, out var bmiValue) ? bmiValue : (decimal?)null;
                var measurementType = MeasurementTypeBox.Text;
                var notes = NoteBox.Text;
                var recordedBy = (int)DoctorNameComboBox.SelectedValue;

                // Lấy dữ liệu từ ComboBox (Bác sĩ)
                var doctorId =  (int)DoctorNameComboBox.SelectedValue  ;  // Lấy giá trị UserId từ SelectedValue

                if (doctorId == null)
                {
                    MessageBox.Show("Please select a doctor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                

                // Lấy ChildId (có thể lấy từ đâu đó, ví dụ thông qua constructor)
                var childId = _chidid;  // Ví dụ: lấy từ thông tin đang chọn, có thể là một giá trị động

                // Tạo đối tượng GrowthRecord
                var growthRecord = new GrowthRecord
                {
                    ChildId = childId,
                    RecordDate = DateTime.Now,
                    Height = height,
                    Weight = weight,
                    HeadCircumference = headCircumference,
                    Bmi = bmi,
                    MeasurementType = measurementType,
                    Notes = notes,
                    RecordedBy = recordedBy,
                    Status = "Active"  // Thêm trạng thái theo yêu cầu
                };

                // Tạo đối tượng DataShare
               
              
                // Sử dụng các service để lưu GrowthRecord và DataShare
                _growRecordService.AddRecord(growthRecord);  // Giả sử phương thức AddGrowthRecord tồn tại trong GrowRecordService
                 // Thông báo thành công và đóng cửa sổ
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            // Đóng cửa sổ mà không làm gì
            this.DialogResult = true;
            this.Close();
        }

       
    }
}
