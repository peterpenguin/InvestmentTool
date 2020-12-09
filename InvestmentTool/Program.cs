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
            string path = @"data\Investments.txt";
            File.WriteAllText(path, "");

            //comment
            Investment tesla = new Investment("Tesla");
            Investment lufth = new Investment("Lufthansa");

            //comment
            string[] Names = File.ReadAllLines(path);
            foreach(string element in Names)
            {
                Console.WriteLine(element);
            }
            //IDEE: bei konstruktor array anlegen in welches namen reinkommen dann schleife um für jeden namen rendite auszugeben
            //user intro
            //Console.WriteLine("type check or input");
            //WhichAction();
            Console.ReadKey();
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
            string path = @"data\Investments.txt";
            File.AppendAllText(path, title);
            File.AppendAllText(path, "\n");
        }
    }
}
