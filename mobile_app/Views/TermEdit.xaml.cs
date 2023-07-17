using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile_app.Models;
using mobile_app.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_app.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermEdit : ContentPage
	{
        private readonly int _selectedTermId;

		public TermEdit ()
		{
			InitializeComponent ();
		}

		public TermEdit(int termId)
		{
			InitializeComponent ();
            _selectedTermId = termId;

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

            await DatabaseService.UpdateTerm(_selectedTermId, TermName.Text, StartDatePicker.Date, EndDatePicker.Date);
            await Navigation.PopToRootAsync();
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

            await Navigation.PopToRootAsync();
        }
    }
}