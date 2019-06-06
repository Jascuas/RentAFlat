using RentAFlat.Configuration;
using RentAFlat.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RentAFlat
{
    public partial class App : Application
    {
        private static IoCConfiguration _Locator;

        //UNA PROPIEDAD QUE NOS DEVUELVA UN new IoCConfiguration
        //O QUE RECUPERE EL QUE YA TENGAMOS CREADO
        public static IoCConfiguration Locator
        {
            get
            {
                return _Locator = _Locator ?? new IoCConfiguration();
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new PrincipalMaster();
          //  MainPage = new NavigationPage(new PrincipalMaster());//master page
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
