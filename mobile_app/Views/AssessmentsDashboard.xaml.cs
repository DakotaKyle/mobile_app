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
	public partial class AssessmentsDashboard : ContentPage
	{
        private readonly int _selectedCourseId;

		public AssessmentsDashboard ()
		{
			InitializeComponent ();
		}

        public AssessmentsDashboard(int courseId)
        {
            InitializeComponent();
            _selectedCourseId = courseId;
        }

        async void AssessmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assessment = (Assessment)e.CurrentSelection.FirstOrDefault();

            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new AssessmentEdit(assessment));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var AssessmentList = await DatabaseService.GetAssessments(_selectedCourseId);
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);

            // Handles assessment notifications.
            foreach (Assessment assessmentRecord in AssessmentList)
            {
                if (assessmentRecord.StartNotification == true)
                {
                    if (assessmentRecord.DueDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"your {assessmentRecord.Name} begins today!", notifyId);
                    }
                }
            }

            AssessmentCollectionView.ItemsSource = await DatabaseService.GetAssessments(_selectedCourseId);
        }

        async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            var courseId = _selectedCourseId;

            await Navigation.PushAsync(new AssessmentAdd(courseId));
        }
    }
}