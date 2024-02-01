using System;
using System.IO;
using Xamarin.Essentials;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Actions;

namespace Hologram2.Services
{
    public static class Server
    {
        private static WebServer _webServer;
        private static bool _status = false;
        public static async Task StartServerAsync()
        {
            try
            {
                _webServer = new WebServer(o => o
                        .WithUrlPrefix("http://*:8081") // Changed to 127.0.0.1 and different port
                        .WithMode(HttpListenerMode.EmbedIO))
                    .WithStaticFolder("/", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), true);

                Console.WriteLine("Starting server...");
                _status = true;
                await _webServer.RunAsync();
                Console.WriteLine("Server running on http://127.0.0.1:8081");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server failed to start: {ex.Message}");
                _status = false;
            }
        }


        public static void StopServer()
        {
            _webServer?.Dispose();
            _status = false;
        }
        public static bool IsServerRunning()
        {
            return _status;
        }
    }
}
