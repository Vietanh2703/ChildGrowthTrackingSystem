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
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public User? User { get; set; }
        private GrowRecordService _growRecordService = new();
        private DoctorFeedbackService _doctorFeedbackService = new();
        private int? _selectedRecordId = null;
        private int? _selectedChildId = null;
        public DoctorWindow()
        {
            InitializeComponent();
        }
        private void DoctorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int userId = User.UserId;
            LoadGrowthRecords();
            HelloMessageLabel.Content = "Hello " + User.FullName + " !";
        }
        private void LoadGrowthRecords()
        {
            List<GrowthRecord> records = _growRecordService.GetAllGrowthRecords();
            dgGrowthRecords.ItemsSource = null;
            dgGrowthRecords.ItemsSource = records;
        }

        private void dgGrowthRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = dgGrowthRecords.SelectedItem as GrowthRecord;
            if (selected != null)
            {
                _selectedRecordId = selected.RecordId;
                _selectedChildId = selected.ChildId;
                // Optionally clear form fields or load previous feedback
                tbComments.Text = string.Empty;
                tbRecommendations.Text = string.Empty;
                dpFollowUpDate.SelectedDate = null;
                tbAttachments.Text = string.Empty;
                tbPriority.Text = string.Empty;
                cbParentAcknowledged.IsChecked = false;
            }
        }

        private void BtnSubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            var growRecord = _growRecordService.GetGrowthRecordById(_selectedRecordId);
                                               
            if (_selectedRecordId == null || _selectedChildId == null)
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để feedback.");
                return;
            }

            if(growRecord.Status == "Đã phản hồi")
            {
                MessageBox.Show("Bản ghi này đã được phản hồi trước đó.");
                return;
            }

            var feedback = new DoctorFeedback
            {
                GrowthRecordId = _selectedRecordId.Value,
                Comments = tbComments.Text,
                Recommendations = tbRecommendations.Text,
                FollowUpDate = dpFollowUpDate.SelectedDate,
                Status = "Reviewed",
                Attachments = tbAttachments.Text,
                Priority = tbPriority.Text,
                ParentAcknowledged = cbParentAcknowledged.IsChecked ?? false
            };

            _doctorFeedbackService.AddFeedback(feedback);
            if(growRecord != null)
            {
                growRecord.Status = "Đã phản hồi";
                _growRecordService.UpdateRecord(growRecord);
            }
            MessageBox.Show("Feedback đã được gửi thành công!");
            LoadGrowthRecords();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow x = new();
            x.Show();
            this.Close();
        }
    }
}
