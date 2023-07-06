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
    public partial class WidgetAdd : ContentPage
    {
        private readonly int _selectedGadgetId;

        public WidgetAdd()
        {
            InitializeComponent();
        }

        public WidgetAdd(int gadgetId)
        {
            InitializeComponent();
            _selectedGadgetId = gadgetId;
        }

        private async void SaveWidget_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WidgetName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(WidgetColorPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing color", "Please enter a color", "OK");
                return;
            }

            await DatabaseService.AddWidget(_selectedGadgetId, WidgetName.Text, WidgetColorPicker.SelectedItem.ToString(), CreationDatePicker.Date, Notification.IsToggled, NoteEditor.Text);
            await Navigation.PopAsync();
        }

        private async void CancelWidget_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}