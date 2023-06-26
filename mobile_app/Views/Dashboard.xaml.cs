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
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() // Handles Notifications.
        {
            base.OnAppearing();

            var widgetList = await DatabaseService.GetWidgets();
            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);

            foreach (Widget widgetRecord in widgetList )
            {
                if (widgetRecord.StartNotification == true)
                {
                    if (widgetRecord.CreationDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{widgetRecord.Name} begins today!", notifyId);
                    }
                }
            }
        }

        private async void AddGadget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GadgetAdd());
        }

        private async void ViewGadget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GadgetList());
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }
    }
}