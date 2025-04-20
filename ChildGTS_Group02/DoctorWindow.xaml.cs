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
using ChildGTS_Group02.BLL.Services;
using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using ChildGTS_Group02.Services;

namespace ChildGTS_Group02
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        private GrowthRecordService _growthRecordService = new();
        private DoctorFeedbackService _doctorFeedbackService = new();
        private int? _selectedRecordId = null;
        private int? _selectedChildId = null;

        public DoctorWindow()
        {
            InitializeComponent();
            LoadGrowthRecords();
        }

        private void LoadGrowthRecords()
        {
            List<GrowthRecord> records = _growthRecordService.GetAllGrowthRecords();
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
                tbStatus.Text = string.Empty;
                tbAttachments.Text = string.Empty;
                tbPriority.Text = string.Empty;
                cbParentAcknowledged.IsChecked = false;
            }
        }

        private void BtnSubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRecordId == null || _selectedChildId == null)
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để feedback.");
                return;
            }

            // TODO: Lấy ShareId phù hợp với nghiệp vụ (nếu có DataShare)
            // Ở đây tạm thởi truyền RecordId vào ShareId (cần điều chỉnh nếu logic khác)
            var feedback = new DoctorFeedback
            {
                ShareId = _selectedRecordId.Value,
                Comments = tbComments.Text,
                Recommendations = tbRecommendations.Text,
                FollowUpDate = dpFollowUpDate.SelectedDate,
                Status = tbStatus.Text,
                Attachments = tbAttachments.Text,
                Priority = tbPriority.Text,
                ParentAcknowledged = cbParentAcknowledged.IsChecked ?? false
            };

            _doctorFeedbackService.AddFeedback(feedback);
            MessageBox.Show("Feedback đã được gửi thành công!");
        }
    }
}
