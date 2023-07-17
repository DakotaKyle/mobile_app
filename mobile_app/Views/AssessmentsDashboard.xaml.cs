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
            AssessmentCollectionView.ItemsSource = await DatabaseService.GetAssessments(_selectedCourseId);
        }
    }
}