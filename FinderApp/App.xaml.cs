using System;
using FinderApp.Data;
using FinderApp.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinderApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPageView());
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
