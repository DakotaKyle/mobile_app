using mobile_app.Models;
using mobile_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WidgetEdit : ContentPage
    {
        private readonly int _selectedWidgetId;

        public WidgetEdit()
        {
            InitializeComponent();
        }

        public WidgetEdit(Widget selectedWidget)
        {
            InitializeComponent();

            _selectedWidgetId = selectedWidget.Id;
            WidgetId.Text = selectedWidget.Id.ToString();
            WidgetName.Text = selectedWidget.Name.ToString();
            WidgetColorPicker.SelectedItem = selectedWidget.Color;
            NotesEditor.Text = selectedWidget.Notes;
            CreationDatePicker.Date = selectedWidget.CreationDate;
            Notification.IsToggled = selectedWidget.StartNotification;
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

            await DatabaseService.UpdateWidget(_selectedWidgetId, WidgetName.Text, WidgetColorPicker.SelectedItem.ToString(), CreationDatePicker.Date, Notification.IsToggled, NotesEditor.Text);
            await Navigation.PopAsync();
        }

        private async void DeleteWidget_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Widget?", "Delete this Widget?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(WidgetId.Text);

                await DatabaseService.RemoveWidget(id);

                await DisplayAlert("Widget deleted.", "Widget deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        private async void ShareURL_Clicked(object sender, EventArgs e)
        {
            var text = WidgetName.Text;

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text."
            });
        }
    }
}