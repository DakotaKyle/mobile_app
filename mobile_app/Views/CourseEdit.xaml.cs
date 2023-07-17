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
    public partial class CourseEdit : ContentPage
    {

        private readonly int _selectedCourseId;

        public CourseEdit()
        {
            InitializeComponent();
        }
        public CourseEdit(Course course)
        {
            InitializeComponent();

            _selectedCourseId = course.Id;
            courseName.Text = course.Name;
            courseStatus.SelectedItem = course.Status;
            instructorName.Text = course.InstructorName;
            phone.Text = course.PhoneNumber;
            email.Text = course.Email;
            StartDatePicker.Date = course.StartDate;
            EndDatePicker.Date = course.EndDate;
        }

        async void SaveCourse_Clicked(object sender, EventArgs e)
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

            await DatabaseService.UpdateCourse(_selectedCourseId, courseName.Text, courseStatus.SelectedItem.ToString(), instructorName.Text, phone.Text, email.Text, StartDatePicker.Date, EndDatePicker.Date, Notification.IsToggled, NotesEditor.Text);
            await Navigation.PopAsync();
        }

        async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Course and associated Assessments", "Delete this course and all of its assessments?", "Yes", "No");

            if (answer == true)
            {
                var id = _selectedCourseId;
                
                await DatabaseService.RemoveCourse(id);

                await DisplayAlert("Course deleted.", "Course  and assessments deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = _selectedCourseId;

            await Navigation.PushAsync(new AssessmentAdd(courseId));
        }

        private async void ShareURL_Clicked(object sender, EventArgs e)
        {
            var text = NotesEditor.Text;

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Notes."
            });
        }

        private async void ViewAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = _selectedCourseId;

            await Navigation.PushAsync(new AssessmentsDashboard(courseId));
        }
    }
}