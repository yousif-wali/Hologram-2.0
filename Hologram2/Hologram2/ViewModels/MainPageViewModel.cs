using System;
using Hologram2.Services;
using Xamarin.Forms;

namespace Hologram2.ViewModels
{
    public class MainPageViewModel
    {

        public Command PickFile { get; }


        public MainPageViewModel()
        {
            PickFile = new Command(PickingAFile);
        }
        private async void PickingAFile(object obj)
        {
            Videos video = new Videos();
            await video.PickAndSaveVideoAsync();
        }
    }
}

