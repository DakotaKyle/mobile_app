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
            if (string.IsNullOrWhiteSpace(courseName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
                return;
            }

            //await DatabaseService.AddCourse(GadgetName.Text, CreationDatePicker.Date);
            await Navigation.PopAsync();
        }
    }
}