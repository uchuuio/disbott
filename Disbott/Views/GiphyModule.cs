using System.Threading.Tasks;

using Discord.Commands;

using Disbott.Controllers;

namespace Disbott.Views
{
    [Name("Giphy")]
    public class GiphyModule : ModuleBase
    {
        [Command("giphy")]
        [Remarks("Gets a random gif")]
        public async Task Giphy([Remainder]string search = null)
        {
            var value = await GiphyController.GetRandomGif(search);
            await ReplyAsync(value);
        }
    }
}
