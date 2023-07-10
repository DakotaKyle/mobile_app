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
            WidgetId.Text = selectedAssessment.Id.ToString();
            WidgetName.Text = selectedAssessment.Name.ToString();
            WidgetColorPicker.SelectedItem = selectedAssessment.Color;
            NotesEditor.Text = selectedAssessment.Notes;
            CreationDatePicker.Date = selectedAssessment.CreationDate;
            Notification.IsToggled = selectedAssessment.StartNotification;
        }

        private async void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WidgetName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(WidgetColorPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing color", "Please enter a color", "OK");
                return;
            }

            await DatabaseService.UpdateAssessment(_selectedAssessmentId, WidgetName.Text, WidgetColorPicker.SelectedItem.ToString(), CreationDatePicker.Date, Notification.IsToggled, NotesEditor.Text);
            await Navigation.PopAsync();
        }

        private async void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Widget?", "Delete this Widget?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(WidgetId.Text);

                await DatabaseService.RemoveAssessment(id);

                await DisplayAlert("Widget deleted.", "Widget deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        private async void ShareURL_Clicked(object sender, EventArgs e)
        {
            var text = WidgetName.Text;

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text."
            });
        }
    }
}