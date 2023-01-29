using System.Diagnostics.Metrics;
using System.Runtime.Serialization.Formatters;

namespace Assignment1
{
    enum Choice
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3,
        Quit =4
    }
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
                Console.WriteLine("Please choose from one of the following options:\n\t1 - 3D Printing Estimator \n\t2 - Rent Prices \n\t3 - Rock Paper Scissor \n\t4 - Payroll Calculator \n\t5 - Quit");

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
                        PrintJobEstimator();
                        break;

                    case 2:
                        RentPrices();
                        break;

                    case 3:
                        RPSGame();
                        break;

                    case 4:
                        CalculateDisplayPayroll();
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

        /*	Problem #1: Print Job Estimator 
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


        /*	Problem #2:Rent Prices
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


        /*	Problem #3: Rock, Paper and Scissor 
         	What the program does in your own words. 
        	 Tester: Bob Doe (student in class).    */

        const int MIN_CHOICE = 1;
        const int MAX_CHOICE = 4;


        public static void RPSGame()
        {
            Choice computerChoice, userChoice;
            Console.WriteLine("What will you choose?");

            do
            {
                 userChoice = GetUserChoice();

                if (userChoice != Choice.Quit)
                {
                    computerChoice = GetComputerChoice();
                    Console.WriteLine($"The computer has picked {computerChoice}");
                    Console.WriteLine($"The user has picked {userChoice}");
                    DetermineWinLoss(userChoice, computerChoice);
                }
            }
            while (userChoice != Choice.Quit);

            Console.WriteLine("Thank you for playing Rock Paper Scissor.");

            Console.WriteLine("\nEnter a key to continue");
            Console.ReadLine();
        }
        public static Choice GetComputerChoice()
        {
            Random computerChoice = new Random();
            return (Choice)computerChoice.Next(MIN_CHOICE, MAX_CHOICE);
        }
        public static Choice GetUserChoice()
        {
            int userChoice;

            Console.WriteLine("Choose the corresponding number of your choice. \n 1 - Rock \n 2 - Paper \n 3 - Scissor \n 4 - Quit");
            bool valid = int.TryParse(Console.ReadLine(), out userChoice);

            while (!valid || userChoice < MIN_CHOICE || userChoice > MAX_CHOICE)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error! Input not within the provided range, was left blank or was a character: Enter your choice again:");
                Console.ForegroundColor = ConsoleColor.White;
                valid = int.TryParse(Console.ReadLine(), out userChoice);
            }

            return (Choice)userChoice;
        }
        public static void DetermineWinLoss(Choice userChoice, Choice computerChoice)
        {
            if (userChoice == computerChoice)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Game Result: DRAW. Play again!\n");
            }
            else if (userChoice == Choice.Rock && computerChoice == Choice.Scissor)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! The rock smashes scissor.\n");
            }
            else if (userChoice == Choice.Scissor && computerChoice == Choice.Paper)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! scissor cuts paper.\n");
            }
            else if (userChoice == Choice.Paper && computerChoice == Choice.Rock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game Result: You WIN! Paper wraps rock.\n");
            }
            else if (userChoice == Choice.Scissor && computerChoice == Choice.Rock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! The rock smashes scissor.\n");
            }
            else if (userChoice == Choice.Paper && computerChoice == Choice.Scissor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! scissor cuts paper.\n");
            }
            else if (userChoice == Choice.Rock && computerChoice == Choice.Paper)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Result: You Lose! Paper wraps rock.\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        /*	Problem #4: Payroll Calculator
                   What the program does in your own words. 
                    Tester: Bob Doe (student in class).    */

        const decimal MAX_REGULAR = 40.00m, MAX_OVERTIME=20.00m, OVERTIME_RATE=1.50m, DOUBLE_RATE=2.00M; 
        public static void CalculateDisplayPayroll()
        {
            string[] employees = {"Marcus", "Ethan", "Philip", "Julien", "Naomie", "Rayane"};
            decimal[] hourlyRate = { 16.75m, 23.95m, 18.18m, 21.00m, 24.50m, 25.71m };
            decimal[] hoursWorked = {0.00m,22.00m,40.00m,51.00m,60.00m,71.80m };
            decimal[] regularPay = new decimal[employees.Length];
            decimal[] overtimePay = new decimal[employees.Length];
            decimal[] doubleOvertimePay = new decimal[employees.Length];
            decimal[] pay = new decimal[employees.Length];

            CalculatePayroll(hourlyRate, hoursWorked, regularPay, overtimePay, doubleOvertimePay, pay);

            for (int i=0;i<employees.Length;i++)
            {
                Console.WriteLine($"{employees[i]} {hoursWorked[i]} hours {hourlyRate[i]}/hr Regular Pay {regularPay[i]:c} Overtime Pay {overtimePay[i]:c} Double Overtime Pay {doubleOvertimePay[i]:c} Total Pay {pay[i]:c}");
            }

        }

        public static void CalculatePayroll(decimal[] rate, decimal[] hours,
            decimal[] regularPay, decimal[] overtimePay, decimal[] doubleOvertimePay, decimal[] pay)
        {
            

            for (int i = 0; i < rate.Length; i++)
            {
                regularPay[i] = GetRegularPay(rate[i], hours[i]);
                overtimePay[i] = GetOvertimePay(rate[i], hours[i] - MAX_REGULAR);
                doubleOvertimePay[i] = GetDoubleOvertimePay(rate[i], hours[i] - (MAX_REGULAR+MAX_OVERTIME));
                pay[i]= regularPay[i] + overtimePay[i] + doubleOvertimePay[i];
            }
             
        }

        public static decimal GetRegularPay(decimal rate, decimal hours)
        {
            decimal regularPay;
            if (hours <= MAX_REGULAR)
                regularPay = hours * rate;
            else
                regularPay = MAX_REGULAR * rate;

            return regularPay;
        }
        public static decimal GetOvertimePay(decimal rate, decimal hours)
        {
            decimal overtimePay;
            if (hours < 0)
                overtimePay = 0.00m;
            else if (hours <= MAX_OVERTIME)
                overtimePay = hours * rate* OVERTIME_RATE;
            else
                overtimePay = MAX_OVERTIME * rate * OVERTIME_RATE;

            return overtimePay;
        }
        public static decimal GetDoubleOvertimePay(decimal rate, decimal hours)
        {
            if (hours <= 0)
                return 0.00m;
            else
                return hours * rate* DOUBLE_RATE;

        }

    }
}
