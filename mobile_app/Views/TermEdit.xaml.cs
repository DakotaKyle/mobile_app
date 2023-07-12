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
		public TermEdit ()
		{
			InitializeComponent ();
		}

		public TermEdit(int termId)
		{
			InitializeComponent ();
		}
	}
}