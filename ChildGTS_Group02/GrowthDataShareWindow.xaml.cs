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
        private GrowRecordService _growRecordService = new();
        private ChildService _childService = new();
        public int _childid { get; set; }
        public GrowthDataShareWindow(int childId)
        {
            InitializeComponent();
            _childid = childId;
        }

        private void GrowthDataWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var child = _childService.GetChildById(_childid);
            if (child != null)
            {
                ChildNameBox.Text = child.FullName;
            }
            else
            {
                MessageBox.Show("Child not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
                this.Close();
            }
            }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var child = _childService.GetChildById(_childid);
            if (child == null)
            {
                MessageBox.Show("Child not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var heightCm = decimal.Parse(HeightBox.Text);
            var weightKg = decimal.Parse(WeightBox.Text);
            var headCircumference = decimal.Parse(HeadBox.Text);
            var heightM = heightCm / 100;

            var bmi = Math.Round(weightKg / (heightM * heightM), 2);
            GrowthRecord growthRecord = new GrowthRecord()
            {
                ChildId = _childid,
                RecordDate = DateTime.Now,
                Height = heightCm,
                Weight = weightKg,
                Bmi = bmi,
                HeadCircumference = headCircumference,
                MeasurementType = MeasurementTypeBox.Text,
                Notes = NoteBox.Text,
                RecordedBy = RecordByBox.Text,
                Status = "Active"
            };

            _growRecordService.AddRecord(growthRecord);
            MessageBox.Show("Data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }
        

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

       
    }
}
