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
    public partial class CourseAdd : ContentPage
    {
        private readonly int _selectedTermId;

        public CourseAdd()
        {
            InitializeComponent();
        }

        public CourseAdd(int termId)
        {
            InitializeComponent();
            _selectedTermId = termId;
        }

        private async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(courseName.Text))
            {
                await DisplayAlert("Missing course name", "Please enter a name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(instructorName.Text))
            {
                await DisplayAlert("Missing instructor name", "Please enter a name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(phone.Text))
            {
                await DisplayAlert("Missing a phone number", "Please enter a number", "OK");
                return;
            }

            if (string.IsNullOrEmpty(email.Text))
            {
                await DisplayAlert("Missing a email", "Please enter an email address", "OK");
                return;
            }

            await DatabaseService.AddCourse(_selectedTermId, courseName.Text, courseStatus.SelectedItem.ToString(), instructorName.Text, phone.Text, email.Text, StartDatePicker.Date, EndDatePicker.Date);
            await Navigation.PopAsync();
        }
    }
}