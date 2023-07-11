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
	public partial class TermAdd : ContentPage
	{
        private readonly int _selectedCourseId;

		public TermAdd ()
		{
			InitializeComponent ();
		}

        public TermAdd(int id)
        {
            InitializeComponent ();

            _selectedCourseId = id;
        }

        private async void SaveTerm_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(StartDatePicker.Date.ToString()))
            {
                await DisplayAlert("Invalid Date", "Please Pick a start date", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(EndDatePicker.Date.ToString()))
            {
                await DisplayAlert("Invalid Date", "Please Pick an end date", "OK");
                return;
            }

            await DatabaseService.AddTerm(_selectedCourseId, TermName.Text, StartDatePicker.Date, EndDatePicker.Date);
            await Navigation.PopToRootAsync();
        }
    }
}