using System;
using Hologram2.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;

namespace Hologram2.Services
{
    public class Videos
    {
        private async Task CheckForReadFile()
        {
            var files = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (files != PermissionStatus.Granted)
            {
                _ = await Permissions.RequestAsync<Permissions.StorageRead>();
            }
        }
        public async Task PickAndSaveVideoAsync()
        {
            try
            {
                await CheckForReadFile();
                var pickResult = await MediaPicker.PickVideoAsync();
                if (pickResult != null)
                {
                    using (var stream = await pickResult.OpenReadAsync())
                    {
                        string localPath = SaveFileToLocalDirectory(stream, pickResult.FileName);
                        var videoItem = new VideoItem
                        {
                            VideoPath = localPath
                        };

                        await DatabaseService.SaveVideoItemAsync(videoItem);
                        // Notify user of success
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
            }
        }

        private string SaveFileToLocalDirectory(Stream stream, string fileName)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
            return filePath;
        }
    }
}
