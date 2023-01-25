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
                Console.WriteLine("Please choose from one of the following options:\n\t1 - Print Pattern \n\t2 - Paint Job Estimator \n\t3 - Rent Prices \n\t4 - Rock, Paper and Scissors game \n\t5 - Quit");

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
                        //SafestLivingArea();
                        PrintCSharp();
                        break;

                    case 2:
                        PaintJobEstimator();
                        break;

                    case 3:
                       // PenniesForPay();
                        RentPrices();
                        break;

                    case 4:
                        RPSGame();
                        break;
                }
            }
            while (choice != 5);
        }

        /*	Problem #1: Safest Living Area 
         	What the program does in your own words. 
        	 Tester: Bob Doe (student in class).    */

        public static void SafestLivingArea()
        {
            const int NUMBER_OF_REGIONS = 5;
            int minIndex;

            string[] regions = new string[NUMBER_OF_REGIONS] { "north", "south", "west", "east", "central" };
            int[] regionAccidents = new int[NUMBER_OF_REGIONS];

            for (int i = 0; i < NUMBER_OF_REGIONS; i++)
            {
                regionAccidents[i] = GetNumAccidents(regions[i]);
            }

            minIndex = FindLowestAccidentsArea(regionAccidents);

            PrintData(regions, regionAccidents, minIndex);

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();
        }
        public static int GetNumAccidents(string region)
        {
            int accidentNumber;

            Console.WriteLine($"How many automobile accidents were reported in the {region} region?");
            bool valid = int.TryParse(Console.ReadLine(), out accidentNumber);

            while (!valid || accidentNumber < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error! No negative numbers allowed. Please input a positive number: ");
                valid = int.TryParse(Console.ReadLine(), out accidentNumber);
                Console.ForegroundColor = ConsoleColor.White;
            }

            return accidentNumber;
        }
        public static int FindLowestAccidentsArea(int[] accidentsArray)
        {
            int min = int.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < accidentsArray.Length; i++)
            {
                if (accidentsArray[i] < min)
                {
                    min = accidentsArray[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
        public static void PrintData(string[] regions, int[] accidentsArray, int lowestIndex)
        {
            Console.WriteLine($"The region with the lowest number of accidents is the {regions[lowestIndex]} region with {accidentsArray[lowestIndex]} accident(s).");

        }


        /**/

        public static void PrintCSharp()
        {
            int SIZE = 10, ROWS = 5, CBORDER = 3, POUNDSTART = 6, POUNDEND=8 ;

            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < SIZE; column++)
                {
                    if (column == 0 || column == POUNDSTART || column == POUNDEND)
                        Console.Write("* ");
                    else if ((row == 0 || row == ROWS-1) && column <= CBORDER)
                        Console.Write("* ");
                    else if ((row == 1 || row == CBORDER) && column >= POUNDSTART-1)
                        Console.Write("* ");
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
        }
        /*	Problem #2: Paint Job Estimator 
           What the program does in your own words. 
            Tester: Bob Doe (student in class).    */

        const int MINUMBER_OF_ROOMS = 1;
        const decimal LABOUR_HOURLY_PAY = 30.00M, MIN_GALLON_PRICE = 40.00M;
        const float SQUAREFEET_PER_GALLON = 225F;
        public static void PaintJobEstimator()
        {
            int numberOfRooms, gallonsNeeded;
            decimal pricePerGallon, paintCost, totalLabourCost, totalCharges;
            float totalArea, labourHoursRequired;

            numberOfRooms = GetNumRooms();
            pricePerGallon = GetPricePerGallon();
            totalArea = GetTotalArea(numberOfRooms);
            gallonsNeeded = GetGallonsOfPaint(totalArea);
            paintCost = GetPaintCost(gallonsNeeded, pricePerGallon);
            labourHoursRequired = GetHoursRequired(totalArea);
            totalLabourCost = GetTotalLabourCost(labourHoursRequired);
            totalCharges = GetTotalCharges(paintCost, totalLabourCost);

            PrintEstimatedCharges(totalArea, gallonsNeeded, pricePerGallon, paintCost, labourHoursRequired, totalLabourCost, totalCharges);

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();

        }

        public static int GetNumRooms()
        {
            int numberOfRooms;

            Console.Write("Enter the number of rooms to be painted: ");
            bool valid = int.TryParse(Console.ReadLine(), out numberOfRooms);

            while (!valid || numberOfRooms < MINUMBER_OF_ROOMS)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. Number of rooms must not be less than 1. Enter the number of rooms to be painted: ");
                Console.ForegroundColor = ConsoleColor.White;
                valid = int.TryParse(Console.ReadLine(), out numberOfRooms);

            }

            return numberOfRooms;
        }

        public static decimal GetPricePerGallon()
        {
            decimal price;

            Console.Write("Enter the price of the paint, per gallon (minimum $40):");
            bool valid = decimal.TryParse(Console.ReadLine(), out price);

            while (!valid || price < MIN_GALLON_PRICE)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Error. Price of a gallon of paint must not be less than {MIN_GALLON_PRICE:c}. Enter the price of the paint, per gallon (minimum $40):");
                Console.ForegroundColor = ConsoleColor.White;
                valid = decimal.TryParse(Console.ReadLine(), out price);

            }
            return price;
        }

        public static float GetTotalArea(int numRooms)
        {
            float totalArea = 0, RoomSize;

            for (int i = 0; i < numRooms; i++)
            {
                Console.Write($"Room {i + 1}: Enter the area of wall space in square feet: ");
                bool valid = float.TryParse(Console.ReadLine(), out RoomSize);

                while (!valid || RoomSize < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error, value must be above 0. Enter the area of wall space in square feet: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    valid = float.TryParse(Console.ReadLine(), out RoomSize);
                }

                totalArea += RoomSize;
            }
            return totalArea;
        }

        public static int GetGallonsOfPaint(float totalArea)
        {
            double gallons;

            gallons = Math.Ceiling(totalArea / SQUAREFEET_PER_GALLON);

            return (int)gallons;
        }
        public static decimal GetPaintCost(int gallonsOfPaint, decimal pricePerGallon)
        {
            return gallonsOfPaint * pricePerGallon;
        }
        public static float GetHoursRequired(float totalArea)
        {
            return totalArea / SQUAREFEET_PER_GALLON;

        }
        public static decimal GetTotalLabourCost(float labourHoursRequired)
        {
            return (decimal)labourHoursRequired * LABOUR_HOURLY_PAY;
        }

        public static decimal GetTotalCharges(decimal paintCost, decimal totalLabourCost)
        {
            return paintCost + totalLabourCost;
        }

        public static void PrintEstimatedCharges(float totalArea, int gallonsOfPaint, decimal pricePerGallon,
               decimal paintCost, float labourHoursRequired, decimal totalLabourCost, decimal totalCharges)
        {
            Console.WriteLine("ESTIMATED CHARGES");
            Console.WriteLine("-----------------");
            Console.WriteLine($"{"Total area:",-40} {totalArea:n2}");
            Console.WriteLine($"{"Gallons of paint:",-40} {gallonsOfPaint}");
            Console.WriteLine($"{"Price per gallon:",-40} {pricePerGallon:c}");
            Console.WriteLine($"{"Paint cost:",-40} {paintCost:c}");
            Console.WriteLine($"{"Labour hours required:",-40} {labourHoursRequired:n2}");
            Console.WriteLine($"{"Labour cost @{LABOUR_HOURLY_PAY}/h:",-40} {totalLabourCost:c}");
            Console.WriteLine($"{"Total charges:",-40} {totalCharges:c}");


        }




        /*	Problem #3: Pennies for Pay
              What the program does in your own words. 
               Tester: Bob Doe (student in class).    */

        public static void PenniesForPay()
        {
            int numberOfDays;

            numberOfDays = GetNumberOfDays();
            DisplayTable(numberOfDays);

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();
        }
        public static int GetNumberOfDays()
        {
            int numberOfDays;

            Console.Write("Enter the number of days: ");
            bool valid = int.TryParse(Console.ReadLine(), out numberOfDays);

            while (!valid || numberOfDays < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. Number of days must not be less than 1 and should be an number. Enter the number of days: ");
                Console.ForegroundColor = ConsoleColor.White;
                valid = int.TryParse(Console.ReadLine(), out numberOfDays);
            }

            return numberOfDays;
        }
        public static void DisplayTable(int numberOfDays)
        {
            double dailyPennies = 1;
            double dailyDollars = 0.01;
            double totalDollars = 0.01;
            Console.WriteLine($"------------------------------------------------------------------------------");
            Console.WriteLine($"{"Day",-20} | {"Earning that day",-30} | {"Total earnings to date"} ");

            for (int i = 0; i < numberOfDays; i++)
            {
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine($"{i + 1,-20} | {dailyPennies,-30} | {totalDollars:c}");
                dailyPennies += dailyPennies;
                dailyDollars += dailyDollars;
                totalDollars += dailyDollars;
            }
            Console.WriteLine("------------------------------------------------------------------------------");

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




        /*	Problem #4: Rock, Paper and Scissors 
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

            Console.WriteLine("Thank you for playing Rock, Paper, Scissors.");

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();
        }
        public static int getComputerChoice()
        {
            Random choice = new Random();
            return choice.Next(MIN_CHOICE, MAX_CHOICE + 1);
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
                Console.WriteLine("Game Result: You WIN! The rock smashes scissors.\n");
            }
            else if (userChoice == SCISSOR && computerChoice == PAPER)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! Scissors cuts paper.\n");
            }
            else if (userChoice == PAPER && computerChoice == ROCK)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! Paper wraps rock.\n");
            }
            else if (userChoice == SCISSOR && computerChoice == ROCK)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! The rock smashes scissors.\n");
            }
            else if (userChoice == PAPER && computerChoice == SCISSOR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! Scissors cuts paper.\n");
            }
            else if (userChoice == ROCK && computerChoice == PAPER)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! Paper wraps rock.\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
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

    }
}
