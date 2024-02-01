using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hologram2.Services;

namespace Hologram2
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();
            StartServer();
            MainPage = new NavigationPage(new MainPage());
        }
        private async void StartServer()
        {
            await Server.StartServerAsync();
        }
        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

