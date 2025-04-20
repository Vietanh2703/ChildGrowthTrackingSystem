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
        private readonly UserService _userService = new();

        private readonly ChildService _childService = new();
        public User? User { get; set; }
        public Child? Child { get; set; }
        public Child IsEditMode { get; set; } = null;
 

        public List<User> Parents { get; set; }

        public ChildFormWindow(int parentId)
        {
            InitializeComponent();
            User = _userService.GetUserById(parentId);
        }

        private void ChildFormWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(IsEditMode != null)
            {
                int childId = IsEditMode.ChildId;
                FullNameTextBox.Text = IsEditMode.FullName;
                DateOfBirthPicker.SelectedDate = IsEditMode.DateOfBirth.ToDateTime(new TimeOnly(0, 0));
                GenderComboBox.Text = IsEditMode.Gender.ToString();
                BirthWeightTextBox.Text = IsEditMode.BirthWeight.ToString();
                BirthHeightTextBox.Text = IsEditMode.BirthHeight.ToString();
                ProfileImageTextBox.Text = IsEditMode.ProfileImage;
                MedicalNotesTextBox.Text = IsEditMode.MedicalNotes;
                GrowthRecordWrapper.Visibility = Visibility.Visible;
                LoadGrowthRecordsWithDoctors(childId);
            }
            else
            {
                GrowthRecordWrapper.Visibility = Visibility.Collapsed;
            }

        }

        private void LoadGrowthRecordsWithDoctors(int childId)
        {
            // Clear the current DataGrid items
            GrowthRecordDataGrid.ItemsSource = null;

            // Fetch growth records for the child
            var growthRecords = _childService.GetChildRelatedData(childId);

            // Format the data to include additional details (e.g., doctor information)
            var formattedRecords = growthRecords.Select(record => new
            {
                RecordId = record.RecordId,
                RecordDate = record.RecordDate?.ToString("MM/dd/yyyy"), // Format date
                Height = record.Height,
                Weight = record.Weight,
                Bmi = record.Bmi,
                HeadCircumference = record.HeadCircumference,
                MeasurementType = record.MeasurementType,
                Notes = record.Notes,
                Status = record.Status,
                RecordBy = record.RecordedBy,
            }).ToList();

            // Bind the formatted data to the DataGrid
            GrowthRecordDataGrid.ItemsSource = formattedRecords;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditMode == null)
            {
                Child newChild = new Child
                {
                    FullName = FullNameTextBox.Text,
                    DateOfBirth = DateOnly.FromDateTime(DateOfBirthPicker.SelectedDate ?? DateTime.Today),
                    Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    BirthWeight = decimal.TryParse(BirthWeightTextBox.Text, out var weight) ? weight : (decimal?)null,
                    BirthHeight = decimal.TryParse(BirthHeightTextBox.Text, out var height) ? height : (decimal?)null,
                    ProfileImage = ProfileImageTextBox.Text,
                    MedicalNotes = MedicalNotesTextBox.Text,
                    LastCheckupDate = DateTime.Now,
                    ParentId = User?.UserId ?? 0
                };
                _childService.Create(newChild);
            }
            else
            {
                IsEditMode.FullName = FullNameTextBox.Text;
                IsEditMode.DateOfBirth = DateOnly.FromDateTime(DateOfBirthPicker.SelectedDate ?? DateTime.Today);
                IsEditMode.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                IsEditMode.BirthWeight = decimal.TryParse(BirthWeightTextBox.Text, out var weight) ? weight : (decimal?)null;
                IsEditMode.BirthHeight = decimal.TryParse(BirthHeightTextBox.Text, out var height) ? height : (decimal?)null;
                IsEditMode.ProfileImage = ProfileImageTextBox.Text;
                IsEditMode.MedicalNotes = MedicalNotesTextBox.Text;
                IsEditMode.LastCheckupDate = DateTime.Now;

                _childService.Update(IsEditMode);
            }
            this.Close();
        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddGrowthbtn_Click(object sender, RoutedEventArgs e)
        {
            int childId = IsEditMode.ChildId;
            GrowthDataShareWindow growthDataShareWindow = new GrowthDataShareWindow(childId);
            growthDataShareWindow.ShowDialog();
            if (growthDataShareWindow.DialogResult == true)
            {
                LoadGrowthRecordsWithDoctors(childId);
            }
        }

        
    }
}
