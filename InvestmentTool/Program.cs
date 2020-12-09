using System;
using System.IO;
using System.Linq;

namespace VermoegenPrototyp
{
    class Program
    {
        static void Main(string[] args)
        {
            //UTF8 encoding for €
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //comment
            string pathNames = @"data\Investments.txt";
            File.WriteAllText(pathNames, "");
            string pathWRs = @"data\WRs.txt";
            File.WriteAllText(pathWRs, "");
            string pathTRs = @"data\TRs.txt";
            File.WriteAllText(pathTRs, "");

            //comment
            Investment tesla = new Investment("Tesla");
            Investment lufth = new Investment("Lufthansa");
            Investment etf = new Investment("MSCI World");

            //comment
            Console.WriteLine("Returns:\n");
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
            //string[] WReturns = 
            //IDEE: bei konstruktor array anlegen in welches namen reinkommen dann schleife um für jeden namen rendite auszugeben
            //user intro
            //Console.WriteLine("type check or input");
            //WhichAction();
            Console.ReadKey();
        }
        public decimal WR(string Name)
        {
            
            return 20;
        }
        public void WhichAction()
        {
            string action = Convert.ToString(Console.ReadLine());
            //initializing investments
            switch (action)
            {
                //switch case am ende auch besser programmieren mit weniger code
                case "check":
                    //present returns
                    //whichaction noch einmal aufrufen mit einer if davor ob man noch was machen will
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

        private void AddWeeklyReturn(decimal weeklyR)
        {
            string pathWRs = @"data\WRs.txt";
            File.AppendAllText(pathWRs, Convert.ToString(weeklyR) + "\n");
        }
        private void AddTotalReturn(decimal totalR)
        {
            string pathTRs = @"data\TRs.txt";
            File.AppendAllText(pathTRs, Convert.ToString(totalR) + "\n");
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
            File.AppendAllText(pathNames, title);
            File.AppendAllText(pathNames, "\n");
        }
    }
}
