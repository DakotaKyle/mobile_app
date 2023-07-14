using mobile_app.Models;
using mobile_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentAdd : ContentPage
    {
        private readonly int _selectedCourseId;

        public AssessmentAdd()
        {
            InitializeComponent();
        }

        public AssessmentAdd(int courseId)
        {
            InitializeComponent();
            _selectedCourseId = courseId;
        }

        private async void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(assessmentName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(type.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing Assessment type", "Please select a type", "OK");
                return;
            }

            await DatabaseService.AddAssessment(_selectedCourseId, assessmentName.Text, type.SelectedItem.ToString(), DueDatePicker.Date, Notification.IsToggled);
            await Navigation.PopAsync();
        }
    }
}