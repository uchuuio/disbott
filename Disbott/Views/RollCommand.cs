using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Text.RegularExpressions;
using Disbott.Models.Objects;
using Disbott.Controllers;

namespace Disbott.Views
{
    [Module]
    public class RollCommand
    {
        string manyDice = "To avoid spam you cannot roll more than 100 dice or a d100 (Sorry)";
        string d1 = "Grats you rolled a d1, hope you are proud";
        string inccorrectFormat = "Invalid Input. Roll a dice by using the format 'xdy' ";

        [Command("roll"), Description("Rolls a Dice!")]
        public async Task roll(IUserMessage msg, string userInput)
        {
            //Validate the User input to make sure is in corret format
            Match diceValidation = Roll.ValidateDiceRoll(userInput);

            // If the Validation is Successful
            if (diceValidation.Success)
            {
                var DiscordID = msg.Author.Username;
                int numberOfDiceRolledInt = Convert.ToInt32(Roll.GetNumberOfDice(userInput).Value);
                int numberOfSidesOnDiceInt = Convert.ToInt32(Roll.GetNumberOfSides(userInput).Value);
                
                // If the number of dice or number of sides is above 100 display this message
                if (numberOfSidesOnDiceInt > 100 || numberOfDiceRolledInt > 100)
                {
                    await msg.Channel.SendMessageAsync(manyDice);
                }

                // If the number of sides is 1 display this message
                else if (numberOfSidesOnDiceInt == 1)
                {
                    await msg.Channel.SendMessageAsync(d1);
                }
               
                // If the User input is correct and passes the checks run this 
                else
                {
                    DiceResults rolledDice = Roll.Rolling(numberOfDiceRolledInt, numberOfSidesOnDiceInt);
                    // Send all the Information back into the chat to the user
                    await msg.Channel.SendMessageAsync($"{DiscordID} Rolled: \r\n{rolledDice.Results} \r\nTotal: {rolledDice.Total.ToString()}");
                }
            }
            else
            {
                //If the input is in an incorrect format display this message.
                await msg.Channel.SendMessageAsync(inccorrectFormat);
            }
        }
    }
}
