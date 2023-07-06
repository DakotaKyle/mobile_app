using mobile_app.Services;
using mobile_app.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Settings.FirstRun)
            {
                DatabaseService.LoadSampleData();
                Settings.FirstRun = false;
            }

            var dashboard = new Dashboard();
            var navPage = new NavigationPage(dashboard);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
