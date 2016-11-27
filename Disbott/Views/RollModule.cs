using System;
using System.Threading.Tasks;

using Discord.Commands;
using System.Text.RegularExpressions;
using Disbott.Models.Objects;
using Disbott.Controllers;

namespace Disbott.Views
{
    [Name("Roll")]
    public class RollModule : ModuleBase
    {
        string manyDice = "To avoid spam you cannot roll more than 100 dice or a d100 (Sorry)";
        string d1 = "Grats you rolled a d1, hope you are proud";
        string inccorrectFormat = "Invalid Input. Roll a dice by using the format 'xdy' ";

        [Command("roll")]
        [Remarks("Rolls a Dice!")]
        public async Task Roll(string userInput)
        {
            //Validate the User input to make sure is in corret format
            Match diceValidation = RollController.ValidateDiceRoll(userInput);

            // If the Validation is Successful
            if (diceValidation.Success)
            {
                var msg = Context.Message;
                var discordId = msg.Author.Username;
                int numberOfDiceRolledInt = Convert.ToInt32(RollController.GetNumberOfDice(userInput).Value);
                int numberOfSidesOnDiceInt = Convert.ToInt32(RollController.GetNumberOfSides(userInput).Value);
                
                // If the number of dice or number of sides is above 100 display this message
                if (numberOfSidesOnDiceInt > 100 || numberOfDiceRolledInt > 100)
                {
                    await ReplyAsync(manyDice);
                }

                // If the number of sides is 1 display this message
                else if (numberOfSidesOnDiceInt == 1)
                {
                    await ReplyAsync(d1);
                }
               
                // If the User input is correct and passes the checks run this 
                else
                {
                    DiceResults rolledDice = RollController.Rolling(numberOfDiceRolledInt, numberOfSidesOnDiceInt);
                    // Send all the Information back into the chat to the user
                    await ReplyAsync($"{discordId} Rolled: \r\n{rolledDice.Results} \r\nTotal: {rolledDice.Total.ToString()}");
                }
            }
            else
            {
                //If the input is in an incorrect format display this message.
                await ReplyAsync(inccorrectFormat);
            }
        }
    }
}
