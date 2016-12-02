using System.Threading.Tasks;
using Disbott.Controllers;

using Discord.Commands;

namespace Disbott.Views
{
    [Name("Coinflip")]
    public class CoinflipModule : ModuleBase
    {
        [Command("flip")]
        [Remarks("Flips a coin!")]
        public async Task Flip()
        {
            string result = CoinFlipController.Flip();
            await ReplyAsync(result);
        }
    }
}
