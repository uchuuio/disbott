using System.Threading.Tasks;
using Disbott.Controllers;

using Discord.Commands;

namespace Disbott.Views
{
    [Name("BlackJack")]
    public class BlackJackModule : ModuleBase
    {
        [Command("blackjack")]
        [Remarks("Starts a new round of blackjack")]
        public async Task StartBlackJack()
        {

        }
    }
}
