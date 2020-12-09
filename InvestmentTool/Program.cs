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
            //only return weeklyreturn when 3 prices exist - when 2 exist - return weeklyr = totalr
            //only return total return when 2 prices exist
            //input investments by asking for price of the investment + asking if something gor bought
            //give the user the possibility to create an investment my asking name + type of investment + either quantity and price or weight + kgprice aso

            //UTF8 encoding for €
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //init investments
            Investment tesla = new Investment("Tesla");
            Investment lufth = new Investment("Lufthansa");
            Investment etf = new Investment("MSCI World");

            //put in prices or check returns
            Console.WriteLine("Type input or check");
            WhichAction();
        }
        static void WhichAction()
        {
            string action = Convert.ToString(Console.ReadLine());
            //initializing investments
            switch (action)
            {
                case "check":
                    string pathNames = @"data\Investments.txt";
                    string pathWRs = @"data\WRs.txt";
                    string pathTRs = @"data\TRs.txt";
                    Console.WriteLine("-----\nReturns:\n");
                    Console.WriteLine("\t\tweekly\t\ttotal");
                    int rows = File.ReadLines(pathNames).Count();
                    string[] Names = File.ReadAllLines(pathNames);
                    string[] WRs = File.ReadAllLines(pathWRs);
                    string[] TRs = File.ReadAllLines(pathTRs);
                    Console.WriteLine(Names[0] + "\t\t" + Math.Round(Convert.ToDecimal(WRs[0]), 1) + "%\t\t" + Math.Round(Convert.ToDecimal(TRs[0]), 1) + "%");
                    for (int i = 1; i < rows; i++)
                    {
                        Console.WriteLine(Names[i] + "\t" + Math.Round(Convert.ToDecimal(WRs[i]), 1) + "%\t\t" + Math.Round(Convert.ToDecimal(TRs[i]), 1) + "%");
                    }
                    Console.WriteLine("\nAnother action or close app? (type action/close)");
                    AnotherAction();
                    break;
                case "input":
                    //user input into txt file for every investment + dont forget date!!
                    //whichaction noch einmal aufrufen mit einer if davor ob man noch was machen will
                    break;
                default:
                    Console.WriteLine("Type in valid command. (check/input)");
                    WhichAction();
                    break;
            }
        }
        static void AnotherAction()
        {
            string decision = Convert.ToString(Console.ReadLine());
            switch (decision)
            {
                case "action":
                    Console.WriteLine("\nType input or check");
                    WhichAction();
                    break;
                case "close":
                    break;
                default:
                    Console.WriteLine("Type in valid command. (action/close)");
                    AnotherAction();
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

        //properties
        public decimal TR
        {
            get
            {
                return TotalReturn;
            }
            set
            {
                TotalReturn = value;
            }
        }
        public decimal WR
        {
            get
            {
                return WeeklyReturn;
            }
            set
            {
                WeeklyReturn = value;
            }
        }

        //methods
        private void GetPrices()
        {
            //checken ob files existieren
            string path = @"data\" + Name + ".txt";
            int rows = File.ReadLines(path).Count();
            CurrentPrice = Convert.ToDecimal(File.ReadLines(path).Skip(rows - 1).Take(1).First());
            BuyInPrice = Convert.ToDecimal(File.ReadLines(path).Skip(1).Take(1).First());
            PriceAWeekAgo = Convert.ToDecimal(File.ReadLines(path).Skip(rows - 3).Take(1).First());
        }
        private void CalculateReturns()
        {
            TotalReturn = ((CurrentPrice - BuyInPrice) / BuyInPrice) * 100;
            WeeklyReturn = ((CurrentPrice - PriceAWeekAgo) / PriceAWeekAgo) * 100;
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
                File.AppendAllText(pathNames, title);
                File.AppendAllText(pathNames, "\n");
            }
        }
        private void AddWeeklyReturn(decimal weeklyR)
        {
            string pathWRs = @"data\WRs.txt";
            if (File.Exists(pathWRs) == true)
            {
                File.AppendAllText(pathWRs, Convert.ToString(weeklyR) + "\n");
            }
            else
            {
                File.Create(pathWRs);
                File.AppendAllText(pathWRs, Convert.ToString(weeklyR) + "\n");
            }

        }
        private void AddTotalReturn(decimal totalR)
        {
            string pathTRs = @"data\TRs.txt";
            if (File.Exists(pathTRs) == true)
            {
                File.AppendAllText(pathTRs, Convert.ToString(totalR) + "\n");
            }
            else
            {
                File.Create(pathTRs);
                File.AppendAllText(pathTRs, Convert.ToString(totalR) + "\n");
            }

        }
    }
}
