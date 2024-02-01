using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hologram2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Watch.Clicked += Watch_Clicked;
        }

        private async void Watch_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Watch());
        }
    }
}

