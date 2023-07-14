using mobile_app.Models;
using mobile_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentEdit : ContentPage
    {
        private readonly int _selectedAssessmentId;

        public AssessmentEdit()
        {
            InitializeComponent();
        }

        public AssessmentEdit(Assessment selectedAssessment)
        {
            InitializeComponent();

            _selectedAssessmentId = selectedAssessment.Id;
            assessmentName.Text = selectedAssessment.Name.ToString();
            type.SelectedItem = selectedAssessment.AssessmentType;
            DueDatePicker.Date = selectedAssessment.DueDate;
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
                await DisplayAlert("Missing an assessment type", "Please select an assessment", "OK");
                return;
            }

            await DatabaseService.UpdateAssessment(_selectedAssessmentId, assessmentName.Text, type.SelectedItem.ToString(), DueDatePicker.Date, Notification.IsToggled);
            await Navigation.PopAsync();
        }

        private async void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Assessment?", "Delete this Assessment?", "Yes", "No");

            if (answer == true)
            {
                var id = _selectedAssessmentId;

                await DatabaseService.RemoveAssessment(id);

                await DisplayAlert("Assessment deleted.", "Assessment deleted", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}