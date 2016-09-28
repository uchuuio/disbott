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

namespace Disbott.Modules.Roll
{
    [Module]
    public class Roll
    {
        RollMethod RollMethod = new RollMethod();

        [Command("roll"), Description("Rolls a Dice!")]
        public async Task roll(IUserMessage msg, string userInput)
        {
            //Validate the User input to make sure is in corret format
            Match diceValidation = ValidateDiceRoll(userInput);

            // If the Validation is Successful
            if (diceValidation.Success)
            {
                // Get the number of Dice that were rolled
                Match numberOfDiceRolled = GetNumberOfDice(userInput);

                //Get the number of Sides on each dice
                Match numberOfSidesOnDice = GetNumberOfSides(userInput);

                //Roll the dice
                rolling(msg, numberOfDiceRolled, numberOfSidesOnDice);
            }
            else
            {
                //If the input is in an incorrect format display this message.
                await msg.Channel.SendMessageAsync("Invalid Input. Roll a dice by using the format 'xdy' ");
            }
        }

        // Method to Validat the dice roll
        private Match ValidateDiceRoll(string userInput)
        {
            // Set the dice to be the value the user entered
            string dice = userInput;

            // The Regex pattern to match the user input against
            string pattern = "([1-9]\\d*)?d([1-9]\\d*)([/x][1-9]\\d*)?([+-]\\d+)?";

            // Check if the User input matches the pattern for the dice
            Match isValidated = Regex.Match(dice, pattern);
            return isValidated;
        }

        // Method to get the number of DIce
        private Match GetNumberOfDice(string userInput)
        {
            // Set the number of dice value to be equal to the user Input
            string numberOfDice = userInput;

            // The Regex pattern to  match the user input against
            string numberOfDicePattern = "^.*?(?=d)";

            //Check if the number of dice the user input matches the pattern
            Match numberOfDiceRolled = Regex.Match(numberOfDice, numberOfDicePattern);

            // Get the amount of dice the user rolled
            return numberOfDiceRolled;
        }

        // Method to get the number of sides on each dice
        private Match GetNumberOfSides(string userInput)
        {
            // Set the number of sides on each dice to be equal to the iser input
            string numberOfSidesOnDice = userInput;

            //The regex pattern to match against the user input
            string numberOfSidesOnDicePattern = "[^d]*$";
            
            // Check if the amount of sides on the dice matches the pattern
            Match numberOfSidesOnDiceRolled = Regex.Match(numberOfSidesOnDice, numberOfSidesOnDicePattern);

            //Get the amount of sides on each dice
            return numberOfSidesOnDiceRolled;
        }

        //Method to roll the dice
        private async void rolling(IUserMessage msg, Match dicerolled, Match dicesides)
        {
            // Get the username of the person who ran the command
            var DiscordID = msg.Author.Username;

            // Set the Number of Dice to the userinput that we validated above
            int numberOfDiceRolledInt = Convert.ToInt32(dicerolled.Value);

            // Set the Number of Dice sides to the userinput that we validated above
            int numberOfSidesPresentInt = Convert.ToInt32(dicesides.Value);

            // If the number of dice or number of sides is above 100 display this message
            if (numberOfSidesPresentInt > 100 || numberOfDiceRolledInt > 100)
            {
                await msg.Channel.SendMessageAsync("To avoid spam you cannot roll more than 100 dice or a d100 (Sorry)");
            }

            // If the number of sides is 1 display this message
            else if (numberOfSidesPresentInt == 1)
            {
                await msg.Channel.SendMessageAsync("Grats you rolled a d1, hope you are proud");
            }

            // If the User input is correct and passes the checks run this 
            else
            {
                // creates a random number and stores it as the variable rnd
                Random rnd = new Random();

                // Creates a new empty array(list) of numbers
                List<int> arrayList = new List<int>();

                // Rolling the dice 1 at a time until the full amount have been rolled
                for (int i = 0; i < numberOfDiceRolledInt; i++) 
                {
                    // returns a random value between 1 and the number of sides+1
                    int roll = rnd.Next(1, (numberOfSidesPresentInt + 1));

                    // sets the value the roll landed on
                    int totalRolled = roll;

                    // adds the value of the roll to the array
                    arrayList.Add(totalRolled); 
                }

                // Sum of the array is defined here before I add text
                int total = arrayList.Sum(); 

                // Turn the numerical array into a string
                string rolls = string.Join(", ", arrayList);


                await msg.Channel.SendMessageAsync(DiscordID + " Rolled:");
                await msg.Channel.SendMessageAsync(rolls);
                await msg.Channel.SendMessageAsync("Total: " + total.ToString());
            }
        }
    }
}
