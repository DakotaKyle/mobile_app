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

            await DatabaseService.AddAssessment(_selectedCourseId, WidgetName.Text, WidgetColorPicker.SelectedItem.ToString(), CreationDatePicker.Date, Notification.IsToggled);
            await Navigation.PopAsync();
        }
    }
}