using System.Threading.Tasks;
using WebSocketSharp;

using Discord.Commands;

namespace Disbott.Views
{
    [Name("Radio")]
    public class RadioModule : ModuleBase
    {
        [Command("request")]
        [Remarks("Requests to queue the youtube video into the playlist")]
        public async Task RequestSong()
        {
            var ws = new WebSocket("ws://localhost:3000");
            ws.Connect();
            ws.SendAsync();
        }

        [Command("current")]
        [Remarks("Gets currently playing song")]
        public async Task CurrentSong()
        {

        }

        [Command("skip")]
        [Remarks("Skips to the next song")]
        public async Task SkipSong()
        {

        }

        [Command("playlist")]
        [Remarks("Tells user where to find the playlist")]
        public async Task Playlist()
        {

        }
    }
}
