using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Text.RegularExpressions;
using System.Linq;

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
                var DiscordID = msg.Author.Username;
                int numberOfDiceRolledInt = Convert.ToInt32(RollMethod.GetNumberOfDice(userInput).Value);
                int numberOfSidesOnDiceInt = Convert.ToInt32(RollMethod.GetNumberOfSides(userInput).Value);
                
                // If the number of dice or number of sides is above 100 display this message
                if (numberOfSidesOnDiceInt > 100 || numberOfDiceRolledInt > 100)
                {
                    await msg.Channel.SendMessageAsync("To avoid spam you cannot roll more than 100 dice or a d100 (Sorry)");
                }

                // If the number of sides is 1 display this message
                else if (numberOfSidesOnDiceInt == 1)
                {
                    await msg.Channel.SendMessageAsync("Grats you rolled a d1, hope you are proud");
                }
               
                // If the User input is correct and passes the checks run this 
                else
                {
                    var rolledDice = RollMethod.Rolling(numberOfDiceRolledInt, numberOfSidesOnDiceInt);
                    int total = rolledDice.Sum();
                    string rolls = string.Join(", ", rolledDice);
                    
                    // Send all the Information back into the chat to the user
                    await msg.Channel.SendMessageAsync($"{DiscordID} Rolled: \r\n{rolls.ToString()} \r\nTotal: {total.ToString()}");
                }
            }
            else
            {
                //If the input is in an incorrect format display this message.
                await msg.Channel.SendMessageAsync("Invalid Input. Roll a dice by using the format 'xdy' ");
            }
        }
    }
}
