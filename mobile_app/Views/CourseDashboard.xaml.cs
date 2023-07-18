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

            var CourseList = await DatabaseService.GetCourses(_selectedTermId);
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);

            // Handles course notifications.
            foreach (Course courseRecord in CourseList)
            {
                if (courseRecord.StartNotification == true)
                {
                    if (courseRecord.StartDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"Your scheduled to start {courseRecord.Name} today!", notifyId);
                    }
                    if (courseRecord.EndDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"Your scheduled to finish {courseRecord.Name} today!", notifyId);
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
    }
}