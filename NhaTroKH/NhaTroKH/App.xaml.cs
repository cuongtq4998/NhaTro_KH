using System;
using NhaTroKH.viewUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NhaTroKH
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPageUI()); 
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
