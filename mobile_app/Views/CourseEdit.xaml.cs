using mobile_app.Models;
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
    public partial class CourseEdit : ContentPage
    {

        private readonly int _selectedGadgetId;

        public CourseEdit()
        {
            InitializeComponent();
        }
        public CourseEdit(Course course)
        {
            InitializeComponent();

            _selectedGadgetId = course.Id;
            GadgetId.Text = course.Id.ToString();
            GadgetName.Text = course.Name;
            GadgetColorPicker.SelectedItem = course.Color;
            CreationDatePicker.Date = course.CreationDate;
        }

        async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GadgetName.Text))
            {
                await DisplayAlert("Missing name", "Please enter a name", "OK");
            }

            if (string.IsNullOrWhiteSpace(GadgetColorPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing color", "Please enter a color", "OK");
            }

            await DatabaseService.UpdateCourse(_selectedGadgetId, GadgetName.Text, GadgetColorPicker.SelectedItem.ToString(), DateTime.Parse(CreationDatePicker.Date.ToString()));
            await Navigation.PopAsync();
        }

        async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Gadget and related Widgets?", "Delete this Gadget?", "Yes", "No");

            if (answer == true)
            {
                var id = _selectedGadgetId;
                
                await DatabaseService.RemoveCourse(id);

                await DisplayAlert("Gadget deleted.", "Gadget deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        async void AddWidget_Clicked(object sender, EventArgs e)
        {
            var gadgetId = _selectedGadgetId;

            await Navigation.PushAsync(new WidgetAdd(gadgetId));
        }

        async void WidgetCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var widget = (Widget)e.CurrentSelection.FirstOrDefault();

            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new WidgetEdit(widget));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            WidgetCollectionView.ItemsSource = await DatabaseService.GetWidgets();
        }
    }
}