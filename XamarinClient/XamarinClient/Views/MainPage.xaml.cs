using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinClient.ViewModels;

namespace XamarinClient.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            //Binding to the locator key
            BindingContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).Main;
        }
	}
}
