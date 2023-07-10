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
        public CourseAdd()
        {
            InitializeComponent();
        }

        private async void SaveGadget_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GadgetName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(GadgetColorPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing color", "Please enter a color", "OK");
                return;
            }

            await DatabaseService.AddCourse(GadgetName.Text, GadgetColorPicker.SelectedItem.ToString(), CreationDatePicker.Date);
            await Navigation.PopAsync();
        }
    }
}