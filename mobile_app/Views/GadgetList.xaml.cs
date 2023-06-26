using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile_app.Models;
using mobile_app.Services;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GadgetList : ContentPage
    {
        public GadgetList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GadgetCollectionView.ItemsSource = await DatabaseService.GetGadgets();
        }

        private async void GadgetCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Gadget gadget = (Gadget)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new GadgetEdit(gadget));
            }
        }
    }
}