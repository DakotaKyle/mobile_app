using mobile_app.Models;
using mobile_app.Services;
using Plugin.LocalNotifications;
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
    public partial class CourseDashboard : ContentPage
    {

        private readonly int _selectedTermId;

        public CourseDashboard()
        {
            InitializeComponent();
        }

        public CourseDashboard(int termId)
        {
            InitializeComponent();

            _selectedTermId = termId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var AssessmentList = await DatabaseService.GetAssessments();
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);

            // Handles Notifications.
            foreach (Assessment assessmentRecord in AssessmentList)
            {
                if (assessmentRecord.StartNotification == true)
                {
                    if (assessmentRecord.CreationDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{assessmentRecord.Name} begins today!", notifyId);
                    }
                }
            }
            // Source for CollectionView
            CourseCollectionView.ItemsSource = await DatabaseService.GetCourses(_selectedTermId);
        }

        private async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Course course = (Course)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new CourseEdit(course));
            }
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            var termId = _selectedTermId;
            await Navigation.PushAsync(new CourseAdd(termId));
        }

        private async void EditTerm_Clicked(object sender, EventArgs e)
        {
            var termId = _selectedTermId;
            await Navigation.PushAsync(new TermEdit(termId));
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Term and associated Courses?", "Delete this Term?", "Yes", "No");

            if (answer == true)
            {
                var id = _selectedTermId;

                await DatabaseService.RemoveTerm(id);

                await DisplayAlert("Term deleted.", "Term and all related courses have been deleted", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}