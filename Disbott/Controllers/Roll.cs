using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Disbott.Models.Objects;

namespace Disbott.Controllers
{
    public static class RollController
    {
        /// <summary>
        /// Validates the Dice roll user input
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static Match ValidateDiceRoll(string userInput)
        {
            // Set the dice to be the value the user entered
            string dice = userInput;

            // The Regex pattern to match the user input against
            string pattern = "([1-9]\\d*)?d([1-9]\\d*)([/x][1-9]\\d*)?([+-]\\d+)?";

            // Check if the User input matches the pattern for the dice
            Match isValidated = Regex.Match(dice, pattern);

            return isValidated;
        }

        /// <summary>
        /// Method to get the number of dice from the user input
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static Match GetNumberOfDice(string userInput)
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

        /// <summary>
        /// Method to get the number of sides from the user input
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public static Match GetNumberOfSides(string userInput)
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

        /// <summary>
        /// Method to roll the dice
        /// </summary>
        /// <param name="dicerolled"></param>
        /// <param name="dicesides"></param>
        /// <returns></returns>
        public static DiceResults Rolling(int dicerolled, int dicesides)
        {
            // creates a random number and stores it as the variable rnd
            Random rnd = new Random();

            // Creates a new empty array(list) of numbers
            List<int> arrayList = new List<int>();

            // Rolling the dice 1 at a time until the full amount have been rolled
            for (int i = 0; i < dicerolled; i++)
            {
                // returns a random value between 1 and the number of sides+1
                int roll = rnd.Next(1, (dicesides + 1));

                // sets the value the roll landed on
                int totalRolled = roll;

                // adds the value of the roll to the array
                arrayList.Add(totalRolled);
            }

            string rolls = string.Join(", ", arrayList);
            int total = arrayList.Sum();

            DiceResults Results = new DiceResults(rolls, total);

            return Results;
        }
    }
}
