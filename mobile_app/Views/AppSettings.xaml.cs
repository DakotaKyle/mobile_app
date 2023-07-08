using System;
using mobile_app.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace mobile_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        async void LoadSampleData_Clicked(object sender, EventArgs e)
        {
            if (Settings.FirstRun)
            {
                await DatabaseService.LoadSampleData();

                Settings.FirstRun = false;

                await Navigation.PopToRootAsync();

            }
        }

        private async void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.ClearSampleData();
            Preferences.Clear();
        }
    }
}