using mobile_app.Models;
using mobile_app.Services;
using Plugin.LocalNotifications;
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
    public partial class CourseDashboard : ContentPage
    {

        private readonly int _selectedTermId;
        private readonly string _selectedNotes;

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
                    if (assessmentRecord.DueDate == DateTime.Today)
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
    }
}