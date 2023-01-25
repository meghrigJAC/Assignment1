using System.Diagnostics.Metrics;
using System.Runtime.Serialization.Formatters;

namespace Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("************************************************\n");
                Console.WriteLine("Welcome to \"Programming 2 - Assignment 1\"\n");
                Console.WriteLine("Created by <YOUR NAME>\n");
                Console.WriteLine("************************************************\n");
                Console.WriteLine("Please choose from one of the following options:\n\t1 - Print Pattern \n\t2 - 3D Printing Estimator \n\t3 - Rent Prices \n\t4 - Rock Paper Scissor \n\t5 - Quit");

                bool valid = int.TryParse(Console.ReadLine(), out choice);

                while (!valid || choice > 5 || choice < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please choose from one of the above options (1 to 5).");
                    Console.ForegroundColor = ConsoleColor.White;
                    valid = int.TryParse(Console.ReadLine(), out choice);
                }

                switch (choice)
                {
                    case 1:
                        PrintCSharp();
                        break;

                    case 2:
                        PrintJobEstimator();
                        break;

                    case 3:
                        RentPrices();
                        break;

                    case 4:
                        RPSGame();
                        break;
                }
            }
            while (choice != 5);
        }


        /*Problem 1 Print Pattern*/

        public static void PrintCSharp()
        {
            int SIZE = 10, ROWS = 5, CBORDER = 3, POUNDSTART = 6, POUNDEND = 8;

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < SIZE; column++)
                {
                    if (column == 0 || column == POUNDSTART || column == POUNDEND)
                        Console.Write("* ");
                    else if ((row == 0 || row == ROWS - 1) && column <= CBORDER)
                        Console.Write("* ");
                    else if ((row == 1 || row == CBORDER) && column >= POUNDSTART - 1)
                        Console.Write("* ");
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        /*	Problem #2: Print Job Estimator 
          What the program does in your own words. 
           Tester: Bob Doe (student in class).    */

        const decimal MIN_SPOOL_PRICE = 35.00M;
        const int GRAM_TO_KG = 1000;
        public static void PrintJobEstimator()
        {
            int numberOfStudents, spoolsOfFilament, totalTime;
            decimal pricePerSpool, printCost;
            float totalWeight;

            numberOfStudents = GetNumberOfStudents();
            pricePerSpool = GetPricePerSpool();
            totalWeight = GetTotalWeight(numberOfStudents);
            spoolsOfFilament = GetSpoolsOfFilament(totalWeight);
            printCost = GetPrintCost(spoolsOfFilament, pricePerSpool);
            totalTime = GetTotalHours(numberOfStudents);
            PrintCost(totalWeight, spoolsOfFilament, pricePerSpool, printCost, totalTime);
        }

        public static int GetNumberOfStudents()
        {
            int numberOfStudents;

            Console.Write("Enter the number of students: ");
            bool valid = int.TryParse(Console.ReadLine(), out numberOfStudents);

            while (!valid || numberOfStudents < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. Number of students must not be less than 1. Enter the number of students: ");
                Console.ForegroundColor = ConsoleColor.White;
                valid = int.TryParse(Console.ReadLine(), out numberOfStudents);

            }

            return numberOfStudents;
        }

        public static decimal GetPricePerSpool()
        {
            decimal price;

            Console.Write($"Enter the price of a spool of filament (minimum {MIN_SPOOL_PRICE:c}):");
            bool valid = decimal.TryParse(Console.ReadLine(), out price);

            while (!valid || price < MIN_SPOOL_PRICE)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Error. Price of a spool of filament must not be less than {MIN_SPOOL_PRICE:c}. Enter the price of the spool of filament");
                Console.ForegroundColor = ConsoleColor.White;
                valid = decimal.TryParse(Console.ReadLine(), out price);

            }
            return price;
        }

        public static float GetTotalWeight(int numberOfStudents)
        {
            float totalWeight = 0, objectWeight;

            for (int i = 0; i < numberOfStudents; i++)
            {
                Console.Write($"Student {i + 1}: Enter the weight in grams: ");
                bool valid = float.TryParse(Console.ReadLine(), out objectWeight);

                while (!valid || objectWeight < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error, value must be above 0. Enter the weight in grams: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    valid = float.TryParse(Console.ReadLine(), out objectWeight);
                }

                totalWeight += objectWeight;
            }
            return totalWeight;
        }

        public static int GetSpoolsOfFilament(float totalWeight)
        {
            double spools;

            spools = Math.Ceiling(totalWeight / GRAM_TO_KG);

            return (int)spools;
        }

        public static decimal GetPrintCost(int spoolsOfFilament, decimal pricePerSpool)
        {
            return spoolsOfFilament * pricePerSpool;
        }

        public static int GetTotalHours(int numberOfStudents)
        {
            int totalTime = 0, objectTime;

            for (int i = 0; i < numberOfStudents; i++)
            {
                Console.Write($"Student {i + 1}: Enter the time in minutes: ");
                bool valid = int.TryParse(Console.ReadLine(), out objectTime);

                while (!valid || objectTime < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error, value must be above 0. Enter the time in minutes: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    valid = int.TryParse(Console.ReadLine(), out objectTime);
                }

                totalTime += objectTime;
            }
            totalTime = (int)Math.Ceiling((double)totalTime / (double)60);

            return totalTime;
        }

        public static void PrintCost(float totalWeight, int spoolsOfFilament, decimal pricePerSpool, decimal printCost, int numberOfHours)
        {
            Console.WriteLine("ESTIMATED COST");
            Console.WriteLine("-----------------");
            Console.WriteLine($"{"Total Weight:",-40} {totalWeight:n2}");
            Console.WriteLine($"{"Spools of filament:",-40} {spoolsOfFilament}");
            Console.WriteLine($"{"Price per Spool:",-40} {pricePerSpool:c}");
            Console.WriteLine($"{"Print cost:",-40} {printCost:c}");
            Console.WriteLine($"{"Number of hours needed:",-40} {numberOfHours}");
        }


        /*	Problem #3:Rent Prices
      What the program does in your own words. 
       Tester: Bob Doe (student in class).    */


        public static void RentPrices()
        {
            int numberOfYears;
            decimal rentPrice;

            numberOfYears = GetNumberOfYears();
            rentPrice = GetRentPrice();

            DisplayTable(numberOfYears, rentPrice);

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();
        }
        public static int GetNumberOfYears()
        {
            int numberOfYears;

            Console.Write("Enter the number of years: ");
            bool valid = int.TryParse(Console.ReadLine(), out numberOfYears);

            while (!valid || numberOfYears < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. Number of years must not be less than 1 and should be an number. Enter the number of years: ");
                Console.ForegroundColor = ConsoleColor.White;
                valid = int.TryParse(Console.ReadLine(), out numberOfYears);
            }

            return numberOfYears;
        }
        public static decimal GetRentPrice()
        {
            decimal rentPrice;

            Console.Write("Enter the rent price: ");
            bool valid = decimal.TryParse(Console.ReadLine(), out rentPrice);

            while (!valid || rentPrice < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. Price must greater than 0 and should be a decimal number. Enter the rent price: ");
                Console.ForegroundColor = ConsoleColor.White;
                valid = decimal.TryParse(Console.ReadLine(), out rentPrice);
            }

            return rentPrice;
        }
        public static void DisplayTable(int numberOfYears, decimal rentPrice)
        {
            int year = 2023;
            decimal INCREASE_RATE = 2.3M;
            decimal originalPrice = rentPrice;
            decimal newPrice= originalPrice + originalPrice * INCREASE_RATE / 100;


            Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Year",-20} | {"Original price",-30} | {"New price", -30} | {"Increase", -30} ");

            for (int i = 0; i < numberOfYears; i++)
            {
                Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{year,-20} | {originalPrice,-30:c} | {newPrice, -30:c} | {originalPrice - rentPrice, -30:c}");
                year += 1;
                originalPrice= newPrice;
                newPrice= newPrice + newPrice * INCREASE_RATE / 100;

            }
            Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
        }


        /*	Problem #4: Rock, Paper and Scissor 
         	What the program does in your own words. 
        	 Tester: Bob Doe (student in class).    */

        const int MIN_CHOICE = 1;
        const int MAX_CHOICE = 3;


        public static void RPSGame()
        {
            int computerChoice, userChoice;
            string[] choices = { "Rock", "Paper", "Scissor" };

            Console.WriteLine("What will you choose?");

            do
            {
                userChoice = GetUserChoice();

                if (userChoice != 0)
                {
                    computerChoice = getComputerChoice();
                    Console.WriteLine($"The computer has picked {choices[computerChoice - 1]}");
                    Console.WriteLine($"The user has picked {choices[userChoice - 1]}");
                    DetermineWinLoss(userChoice, computerChoice);
                }
            }
            while (userChoice != 0);

            Console.WriteLine("Thank you for playing Rock Paper Scissor.");

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();
        }
        public static int getComputerChoice()
        {
            Random choice = new Random();
            return choice.Next(MIN_CHOICE, MAX_CHOICE + 1);
        }
        public static int GetUserChoice()
        {
            int userChoice;

            Console.WriteLine("Choose the corresponding number of your choice. \n 1 - Rock \n 2 - Paper \n 3 - Scissor \n 4 - Quit");
            bool valid = int.TryParse(Console.ReadLine(), out userChoice);

            while (!valid || userChoice < 0 || userChoice > 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error! Input not within the provided range, was left blank or was a character: Enter your choice again:");
                Console.ForegroundColor = ConsoleColor.White;
                valid = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return userChoice;
        }
        public static void DetermineWinLoss(int userChoice, int computerChoice)
        {
            const int ROCK = 1, PAPER = 2, SCISSOR = 3;

            if (userChoice == computerChoice)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Game Result: DRAW. Play again!\n");
            }
            else if (userChoice == ROCK && computerChoice == SCISSOR)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! The rock smashes scissor.\n");
            }
            else if (userChoice == SCISSOR && computerChoice == PAPER)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! scissor cuts paper.\n");
            }
            else if (userChoice == PAPER && computerChoice == ROCK)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! Paper wraps rock.\n");
            }
            else if (userChoice == SCISSOR && computerChoice == ROCK)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! The rock smashes scissor.\n");
            }
            else if (userChoice == PAPER && computerChoice == SCISSOR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! scissor cuts paper.\n");
            }
            else if (userChoice == ROCK && computerChoice == PAPER)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! Paper wraps rock.\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        

    }
}
