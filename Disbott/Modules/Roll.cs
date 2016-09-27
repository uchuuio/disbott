using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Disbott.Modules
{
    [Module]
    public class Roll
    {
        [Command("roll"), Description("Rolls a Dice!")]
        public async Task roll(IUserMessage msg, string userInput)
        {
            string dice = userInput;
            string pattern = "([1-9]\\d*)?d([1-9]\\d*)([/x][1-9]\\d*)?([+-]\\d+)?";
            string numberOfDice = "^.*?(?=d)";
            string numbeOfSides = "[^d]*$";

            Match m = Regex.Match(dice, pattern);
            Match numberOfDiceRolled = Regex.Match(dice, numberOfDice);
            Match numberOfSidesPresent = Regex.Match(dice, numbeOfSides);
            if (m.Success)
            {
                Console.WriteLine(m.Value);
                Console.WriteLine(numberOfDiceRolled.Value);
                Console.WriteLine(numberOfSidesPresent.Value);

                int numberOfDiceRolledInt = Convert.ToInt32(numberOfDiceRolled.Value);
                int numberOfSidesPresentInt = Convert.ToInt32(numberOfSidesPresent.Value);

                Random rnd = new Random(); // creates a random number and stores it as the variable rnd
                List<int> arrayList = new List<int>(); // Creates a new empty array(list) of numbers
                for (int i = 0; i < numberOfDiceRolledInt; i++) // Rolling the dice 1 at a time until the full amount have been rolled
                {
                    int roll = rnd.Next(1, (numberOfSidesPresentInt + 1)); // returns a random value between 1 and the number of sides+1
                    int totalRolled = roll; // sets the value the roll landed on
                    arrayList.Add(totalRolled); // adds the value of the roll to the array
                }
                int total = arrayList.Sum(); // Sum of the array is defined here before I add text.

                string rolls = string.Join(", ", arrayList);

                await msg.Channel.SendMessageAsync("You Rolled:");
                await msg.Channel.SendMessageAsync(rolls);
                await msg.Channel.SendMessageAsync("Total: " + total.ToString());
            }
        }
    }
}
