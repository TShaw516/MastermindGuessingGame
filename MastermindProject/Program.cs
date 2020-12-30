using System;
using System.Text;

namespace MastermindProject
{
    //Randomly generate 4 numbers each digit between 1 and 6
    //Player has 10 guesses to select correct number in console
    //Each digit has a + if correct in both location and value
    //Each digit has a - if correct in value but not location
    //Each digit has no + or - indicator if incorrect in both value and location

    class Program
    {
        static string randomNumber = GenerateAnswer();
        static string guess;
        static int numberOfGuesses = 10;
        static bool gameOver = false;

        static void Main(string[] args)
        {
            MainMenu();
            ExecuteGame();
        }

        static void MainMenu()
        {
            Console.WriteLine("Welcome to MasterMind! Try to guess a 4 digit number.");
            Console.WriteLine("Each number included in this 4 digit number must be between 1 and 6.");
            Console.WriteLine("You have ten tries. Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        static string GenerateAnswer(int length = 4)
        {
            Random random = new Random();
            var builder = new StringBuilder(length);
            for (var index = 0; index < length; index++)
                builder.Append(random.Next(1, 6));
            return builder.ToString();
        }

        static string CheckGuess(string guess)
        {
            var builder = new StringBuilder(guess.Length);
            for (var index = 0; index < guess.Length; index++)
            {
                var digit = guess[index];
                var characterHint = ' ';

                if (randomNumber.Contains(digit))
                characterHint = randomNumber[index] == digit
                ? '+'
                : '-';

                builder.Append(characterHint);
            }
            return builder.ToString();
        }

        static void ExecuteGame()
        {
            while (gameOver == false)
            {
                Console.WriteLine("Enter your guess");
                guess = Console.ReadLine();
                if (!ValidateInput(guess))
                {
                    Console.WriteLine("You must follow the rules.  Please enter a 4 digit number");
                    continue;
                }
                Console.WriteLine(CheckGuess(guess));
                numberOfGuesses--;
                Console.WriteLine($"Attempts Remaining: {numberOfGuesses}");

                if (guess != randomNumber && numberOfGuesses == 0)
                {
                    Console.WriteLine("Sorry You've Ran out of Guesses. Game Over");
                    gameOver = true;
                    ScreenClear();
                }
                else if (guess == randomNumber)
                {
                    Console.WriteLine("You are Correct! You've won the Game.");
                    gameOver = true;
                    ScreenClear();
                }
            }
        }

        static bool ValidateInput(string guess)
        {
            int number;
            bool lengthIsFour;
            bool isNumber;
            lengthIsFour = guess.Length == 4;      
            isNumber = int.TryParse(guess, out number);
            if (BetweenOneAndSix(guess))
            {
                return lengthIsFour && isNumber;
            }
            return false;
        }

        static bool BetweenOneAndSix(string guess)
        {
            bool testingGuess = false;
            for (var index = 0; index < guess.Length; index++)
            {
                int number = int.Parse(guess[index].ToString());
                if (number >= 1 && number <= 6)
                {
                    testingGuess = true;
                }
                break;
            }
            return testingGuess;
        }

        static void ScreenClear()
        {
            Console.WriteLine("Press any key to exit game");
            Console.ReadKey();
            Console.Clear();
        }


    }
}
