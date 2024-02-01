using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hologram2.Models;
using Hologram2.Services;
using Xamarin.Forms;
namespace Hologram2
{	
	public partial class Watch : ContentPage
	{
        private List<VideoItem> videos;
        private string encodedFileName;
        public Watch()
        {
            InitializeComponent();
            LoadVideos();
        }
        private async void LoadVideos()
        {
            try
            {
                videos = await DatabaseService.GetVideoItemsAsync();
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), videos.Last().VideoPath);
                if (File.Exists(filePath))
                {
                    if (Server.IsServerRunning())
                    {
                        string fileName = Path.GetFileName(videos.Last().VideoPath);
                        encodedFileName = "http://127.0.0.1:8081/" + fileName;
                    }
                }
            }
            catch
            {
               
            }
            finally
            {
                if(encodedFileName == null)
                {
                    encodedFileName = "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
                }
                string htmlContent = $@"

<!DOCTYPE html>
<html>
<head>
    <title>Page Title</title>
    <style>
        html, body {{
            margin: 0;
            padding: 0;
            height: 100vh;
            width: 100vw;
            overflow: hidden;
            background-color: black;
        }}
        section {{
            display: grid;
            grid-template-columns: 1fr 1fr 1fr;
            grid-template-rows: 1fr 1fr 1fr;
            grid-gap: 2px;
            padding: 1em;
            height: 100%;
            width: 100%;
        }}
        video {{
            width: 100%;
            aspect-ratio: 16/9;
            outline: 1px solid black;              
        }}
        video:nth-child(1) {{
            grid-column-start: 2;
            align-self: center;
            transform:scale(1.08)
        }}
        video:nth-child(2) {{
            grid-row-start: 2;
            transform: rotate(270deg) scale(1.08);
            
        }}
        video:nth-child(3) {{
            grid-row-start: 2;
            grid-column-start: 3;
            transform: rotate(90deg) scale(1.08);
            
        }}
        video:nth-child(4) {{
            grid-column-start: 2;
            grid-row-start: 3;
            transform: rotate(180deg) scale(1.08);
        }}
        .controls {{
            display:flex;
            align-items: center;
            justify-content: flex-start;
            position: absolute;
            bottom: 20px;
            left: 50%;
            transform: translateX(-50%) scale(2.5);
            z-index: 10;
        }}
        .controls.animable{{
            animation: hiding 2s linear forwards
        }}
        @keyframes hiding{{
            from{{
                opacity: 100
            }}to{{
                opacity: 0;
            }}
        }}
        .slider {{
            -webkit-appearance: none;
            appearance: none;
            width: 80%;
            height: 15px;
            border-radius: 5px;
            background: #d3d3d3;
            outline: none;
            opacity: 0.7;
            -webkit-transition: .2s;
            transition: opacity .2s;
        }}

        .slider:hover {{
            opacity: 1;
        }}

        .slider::-webkit-slider-thumb {{
            -webkit-appearance: none;
            appearance: none;
            width: 15px;
            height: 15px;
            border-radius: 50%;
            background: #4CAF50;
            cursor: pointer;
        }}

        .slider::-moz-range-thumb {{
            width: 15px;
            height: 15px;
            border-radius: 50%;
            background: #4CAF50;
            cursor: pointer;
        }}
        #playPuaseButton{{
            font-size:1.24rem;
            cursor:pointer;
        }}
    </style>
</head>
<body>
    <section>
        <video src='{encodedFileName}' muted autoplay loop></video>
        <video src='{encodedFileName}' muted autoplay loop></video>
        <video src='{encodedFileName}' muted autoplay loop></video>
        <video src='{encodedFileName}' muted autoplay loop></video>
    </section>
   
   <div class=""controls animable"">
       <span id=""playPuaseButton"" onclick='start(this)''>⏸️</span>
        <input type=""range"" class=""slider"" id=""videoSeeker"" min=""0"" max=""100"" value=""0"">
    </div>
    <script>
        const videos = document.querySelectorAll('video');
        const seeker = document.getElementById('videoSeeker');
        videoplays = true;
        hideControls = true;
        document.body.addEventListener(""click"", ()=>{{
            
            if(hideControls){{
                document.getElementsByClassName(""controls"")[0].classList.remove(""animable"");
                hideControls = false;
            }}else{{
                document.getElementsByClassName(""controls"")[0].classList.add(""animable"");
                hideControls = true;
            }}
        }})
        let maxDuration = 0;
        const start = (e) => {{
            if(videoplays){{
                videos.forEach(elem=>elem.pause());
                videoplays = false;
                e.innerHTML = '▶️';
            }}else{{
                videos.forEach(elem => elem.play());
                videoplays = true;
                e.innerHTML = '⏸️';
            }}
        }}
        function syncVideos(time) {{
            videos.forEach(video => {{
                video.currentTime = time;
            }});
            
        }}
        videos.forEach(video => {{
            video.addEventListener('loadedmetadata', () => {{
                const maxDuration = Math.max(...Array.from(videos).map(v => v.duration));
                seeker.max = maxDuration;
            }});
        }});
        videos[0].addEventListener('timeupdate', ()=>{{
            const  currentTimes = Array.from(videos).map(v => v.currentTime);
            const averageTime = currentTimes.reduce((a, b)=>a+b, 0)/currentTimes.length;
            seeker.value = averageTime;
        }})
        seeker.addEventListener('input', () => {{
            const seekTime = seeker.value;
            syncVideos(seekTime);
        }});
    </script>
</body>
</html>

";
                var htmlSource = new HtmlWebViewSource
                {
                    Html = htmlContent
                };
                HologramBase.Source = htmlSource;

            }
        }
    }
}

