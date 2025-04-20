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
    /// Interaction logic for DoctorFeedbackRatingWindow.xaml
    /// </summary>
    public partial class DoctorFeedbackRatingWindow : Window
    {
        private DoctorFeedbackService _doctorFeedbackService = new();
        private FeedbackRatingService _feedbackRatingService = new();
        private ChildService _childService = new();
        private int? _selectedFeedbackId = null;
        private int _currentUserId = 1;
        public User? User { get; set; }
        public DoctorFeedbackRatingWindow()
        {
            InitializeComponent();
        }

        private void DoctorFeedbackRatingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int userId = User.UserId;
            LoadDoctorFeedbacks();
        }

        private void LoadDoctorFeedbacks()
        {
            int userId = User.UserId;
            List<Child> childIds = _childService.GetAllChildrenByUserId(userId);
            List<DoctorFeedback> feedbacks = _doctorFeedbackService.GetAllFeedbacksByChildId(childIds[0].ChildId);
            dgDoctorFeedbacks.ItemsSource = null;
            dgDoctorFeedbacks.ItemsSource = feedbacks;
        }

        private void dgDoctorFeedbacks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = dgDoctorFeedbacks.SelectedItem as DoctorFeedback;
            if (selected != null)
            {
                _selectedFeedbackId = selected.FeedbackId;
                var rating = _feedbackRatingService.GetUserRatingForFeedback(_selectedFeedbackId.Value, _currentUserId);
                if (rating != null)
                {
                    tbRating.Text = rating.Rating.ToString();
                    tbRatingComment.Text = rating.Comment;
                }
                else
                {
                    tbRating.Text = string.Empty;
                    tbRatingComment.Text = string.Empty;
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSubmitRating_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedFeedbackId == null)
            {
                MessageBox.Show("Vui lòng chọn một feedback để đánh giá.");
                return;
            }
            if (!int.TryParse(tbRating.Text, out int ratingValue) || ratingValue < 1 || ratingValue > 5)
            {
                MessageBox.Show("Điểm đánh giá phải là số nguyên từ 1 đến 5.");
                return;
            }
            var rating = new FeedbackRating
            {
                FeedbackId = _selectedFeedbackId.Value,
                UserId = _currentUserId,
                Rating = ratingValue,
                Comment = tbRatingComment.Text,
                RatingDate = System.DateTime.Now,
                Status = "Active"
            };
            _feedbackRatingService.AddRating(rating);
            MessageBox.Show("Đánh giá đã được gửi thành công!");
        }
    }
}
