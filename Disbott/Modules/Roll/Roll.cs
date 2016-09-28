using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Text.RegularExpressions;

namespace Disbott.Modules.Roll
{
    [Module]
    public class Roll
    {

        [Command("roll"), Description("Rolls a Dice!")]
        public async Task roll(IUserMessage msg, string userInput)
        {
            //Validate the User input to make sure is in corret format
            Match diceValidation = RollMethod.ValidateDiceRoll(userInput);

            // If the Validation is Successful
            if (diceValidation.Success)
            {
                // Get the number of Dice that were rolled
                Match numberOfDiceRolled = RollMethod.GetNumberOfDice(userInput);

                //Get the number of Sides on each dice
                Match numberOfSidesOnDice = RollMethod.GetNumberOfSides(userInput);

                //Roll the dice
                RollMethod.rolling(msg, numberOfDiceRolled, numberOfSidesOnDice);
            }
            else
            {
                //If the input is in an incorrect format display this message.
                await msg.Channel.SendMessageAsync("Invalid Input. Roll a dice by using the format 'xdy' ");
            }
        }
    }
}
