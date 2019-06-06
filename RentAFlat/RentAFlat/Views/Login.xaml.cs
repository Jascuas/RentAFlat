using RentAFlat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentAFlat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
            this.botonlogin.Clicked += Botonlogin_Clicked;
		}

        private void Botonlogin_Clicked(object sender, EventArgs e)
        {
            SessionService session = App.Locator.SessionService;
        }
    }
}