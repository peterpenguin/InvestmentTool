using System;
using System.IO;
using System.Linq;

namespace VermoegenPrototyp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO
            //create child classes for p2p credits gold aso
            //input investments by asking for price of the investment + asking if something gor bought
            //give the user the possibility to create an investment my asking name + type of investment + either quantity and price or weight + kgprice aso

            //UTF8 encoding for €
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //.txt files get cleared
            ClearFiles();

            //initialize investments
            InitInv();

            //user intro (outsorce to static method)
            Console.WriteLine("Type input or check");
            InputOrCheck();
        }
        static void ClearFiles()
        {
            string pathNames = @"data\Investments.txt";
            File.WriteAllText(pathNames, "");
            string pathWR = @"data\WRs.txt";
            File.WriteAllText(pathWR, "");
            string pathTR = @"data\TRs.txt";
            File.WriteAllText(pathTR, "");
        }
        static void InitInv()
        {
            Investment tesla = new Investment("Tesla");
            Investment lufth = new Investment("Lufthansa");
            Investment etf = new Investment("MSCI World");
            Stock apple = new Stock("Apple");
        }
        static void InputOrCheck()
        {
            string action = Convert.ToString(Console.ReadLine());
            //initializing investments
            switch (action)
            {
                case "check":
                    //check if enough data
                    //must be at least 4 rows = 2 prices
                    CheckExistence();
                    DisplayReturns();
                    Console.WriteLine("\nAnother action or close app? (type action/close)");
                    ActionOrClose();
                    break;
                case "input":
                    //user input into txt file for every investment + dont forget date!!
                    //whichaction noch einmal aufrufen mit einer if davor ob man noch was machen will
                    break;
                default:
                    Console.WriteLine("\nType in valid command. (check/input)");
                    InputOrCheck();
                    break;
            }
        }
        static void CheckExistence()
        {
            string pathNames = @"data\Investments.txt";
            string[] Names = File.ReadAllLines(pathNames);
            bool action = true;
            for (int i = 0; i < Names.Length; i++)
            {
                string path = @"data\" + Names[i] + ".txt";
                if (File.Exists(path) == true)
                {
                    int rows = File.ReadAllLines(path).Count();
                    if (rows < 4)
                    {
                        Console.WriteLine("Not enough data for " + Names[i]);
                        action = false;
                    }
                }
                else
                {
                    Console.WriteLine("No data for " + Names[i]);
                    action = false;
                }
            }
            if (action == false)
            {
                Console.WriteLine("\nWant to input data or see returns for investments with enough data? (input/returns)");
                AvaibleReturns();
            }
            Console.ReadLine();
        }
        static void AvaibleReturns()
        {
            string decision = Convert.ToString(Console.ReadLine());
            switch (decision)
            {
                case "input":
                    //input method
                    break;
                case "returns":
                    DisplayReturns();
                    break;
                default:
                    Console.WriteLine("Type in valid command. (input/returns)");
                    AvaibleReturns();
                    break;
            }
        }
        static void DisplayReturns()
        {
            string pathNames = @"data\Investments.txt";
            string pathWRs = @"data\WRs.txt";
            string pathTRs = @"data\TRs.txt";
            Console.WriteLine("-----\nReturns:\n");
            Console.WriteLine("\t\tweekly\t\ttotal");
            int rows = File.ReadLines(pathNames).Count();
            string[] Names = File.ReadAllLines(pathNames);
            string[] WRs = File.ReadAllLines(pathWRs);
            string[] TRs = File.ReadAllLines(pathTRs);
            Console.WriteLine(Names[0] + "\t\t" + Round(WRs[0]) + "\t\t" + Round(TRs[0]));
            for (int i = 1; i < rows; i++)
            {
                //check how long the name is when its to short add some leerzeichen then add tesla to the for loop
                Console.WriteLine(Names[i] + "\t" + Round(WRs[i]) + "\t\t" + Round(TRs[i]));
            }
        }
        static string Round(string number)
        {
            if (number.Contains("/") == true)
            {
                return "/";
            }
            else
            {
                decimal rounded = Math.Round(Convert.ToDecimal(number), 1);
                string stringed = Convert.ToString(rounded);
                string finished = stringed + "%";
                return finished;
            }
        }
        static void InputOrClose()
        {
            string decision = Convert.ToString(Console.ReadLine());
            switch (decision)
            {
                case "input":
                    //input method
                    break;
                case "close":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Type in valid command. (action/close)");
                    ActionOrClose();
                    break;
            }
        }
        static void ActionOrClose()
        {
            string decision = Convert.ToString(Console.ReadLine());
            switch (decision)
            {
                case "action":
                    Console.WriteLine("\nType input or check");
                    InputOrCheck();
                    break;
                case "close":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Type in valid command. (action/close)");
                    ActionOrClose();
                    break;
            }
        }
    }
    class Investment
    {
        //contructor
        public Investment(string _Name)
        {
            Name = _Name;
            GetPrices();
            CalculateReturns();
            AddNameToList(Name);
            AddWeeklyReturn(WeeklyReturn);
            AddTotalReturn(TotalReturn);
        }

        //fields
        public string Name;
        private decimal CurrentPrice;
        private decimal BuyInPrice;
        private decimal PriceAWeekAgo;
        private decimal TotalReturn;
        private decimal WeeklyReturn;

        //methods
        private void GetPrices()
        {
            string path = @"data\" + Name + ".txt";
            if (File.Exists(path) == true)
            {
                int rows = File.ReadLines(path).Count();
                if (rows > 3)
                {
                    CurrentPrice = Convert.ToDecimal(File.ReadLines(path).Skip(rows - 1).Take(1).First());
                    BuyInPrice = Convert.ToDecimal(File.ReadLines(path).Skip(1).Take(1).First());
                    PriceAWeekAgo = Convert.ToDecimal(File.ReadLines(path).Skip(rows - 3).Take(1).First());
                }
            }
        }
        private void CalculateReturns()
        {
            if (CurrentPrice != 0 && BuyInPrice != 0)
            {
                TotalReturn = ((CurrentPrice - BuyInPrice) / BuyInPrice) * 100;
                WeeklyReturn = ((CurrentPrice - PriceAWeekAgo) / PriceAWeekAgo) * 100;
            }
        }
        private void AddNameToList(string title)
        {
            string pathNames = @"data\Investments.txt";
            if (File.Exists(pathNames) == true)
            {
                File.AppendAllText(pathNames, title);
                File.AppendAllText(pathNames, "\n");
            }
            else
            {
                File.Create(pathNames);
                AddNameToList(title);
            }
        }
        private void AddWeeklyReturn(decimal weeklyR)
        {
            string pathWRs = @"data\WRs.txt";
            if (File.Exists(pathWRs) == true)
            {
                if (CurrentPrice != 0 && BuyInPrice != 0 && PriceAWeekAgo != 0)
                {
                    File.AppendAllText(pathWRs, Convert.ToString(weeklyR) + "\n");
                }
                else
                {
                    File.AppendAllText(pathWRs, "/\n");
                }
            }
            else
            {
                if (CurrentPrice != 0 && BuyInPrice != 0 && PriceAWeekAgo != 0)
                {
                    File.Create(pathWRs);
                    File.AppendAllText(pathWRs, Convert.ToString(weeklyR) + "\n");
                }
                else
                {
                    File.Create(pathWRs);
                    File.AppendAllText(pathWRs, "/\n");
                }
            }

        }
        private void AddTotalReturn(decimal totalR)
        {
            string pathTRs = @"data\TRs.txt";
            if (File.Exists(pathTRs) == true)
            {
                if (CurrentPrice != 0 && BuyInPrice != 0 && PriceAWeekAgo != 0)
                {
                    File.AppendAllText(pathTRs, Convert.ToString(totalR) + "\n");
                }
                else
                {
                    File.AppendAllText(pathTRs, "/\n");
                }
            }
            else
            {
                if (CurrentPrice != 0 && BuyInPrice != 0 && PriceAWeekAgo != 0)
                {
                    File.Create(pathTRs);
                    File.AppendAllText(pathTRs, Convert.ToString(totalR) + "\n");
                }
                else
                {
                    File.Create(pathTRs);
                    File.AppendAllText(pathTRs, "/\n");
                }
            }

        }
    }
    class Stock : Investment
    {
        //additional constructor
        public Stock (string _Name) : base(_Name)
        {
            //additional constructor statements

        }

        //fields
        //fields specific for stonks

        //methods
        //methods specific for stonks

    }
    class PreciousMetal : Investment
    {
        public PreciousMetal (string _Name) : base (_Name)
        {
            //additional constructor statements
        }
        
        //fields
        //fields specific for metals

        //methods
        //methods specific for metals
    }
}
