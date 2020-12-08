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



            //user intro
            Console.WriteLine("type check or input");
            WhichAction();
            Console.ReadKey();
        }
        static void WhichAction()
        {
            string action = Convert.ToString(Console.ReadLine());
            //initializing investments
            switch (action)
            {
                //switch case am ende auch besser programmieren mit weniger code
                case "check":
                    Investment tesla = new Investment("Tesla");
                    Investment lufth = new Investment("Lufthansa");
                    Console.WriteLine("\nYour returns:");
                    Console.WriteLine("\nINVESTMENT\tWEEKLY\tTOTAL");
                    //TESLA
                    Console.WriteLine(tesla.Name + "\t" + tesla.RenditeBerechnen("week") + "%\t" + tesla.RenditeBerechnen("abs") + "%");
                    //LUFTHANSA
                    Console.WriteLine(lufth.Name + "\t" + lufth.RenditeBerechnen("week") + "%\t" + lufth.RenditeBerechnen("abs") + "%");
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
            GetCurrent(Name);
            GetBuyIn(Name);
        }

        //fields
        public string Name;
        private decimal CurrentPrice;
        private decimal BuyInPrice;
        private decimal PriceAWeekAgo;

        //methods
        private void GetCurrent(string title)
        {
            string path = @"data\" + Name + ".txt";
            int rows = File.ReadLines(path).Count();
            CurrentPrice = Convert.ToDecimal(File.ReadLines(path).Skip(rows - 1).Take(1).First());
        }
        private void GetBuyIn(string title)
        {

        }

        public decimal RenditeBerechnen(string Rendite)
        {
            switch (Rendite)
            {
                case "abs":
                    decimal absoluteRendite;
                    if (CurrentPrice - BuyInPrice < 0)
                    {
                        absoluteRendite = ((CurrentPrice - BuyInPrice) / BuyInPrice) * 100;
                        return absoluteRendite;
                    }
                    else
                    {
                        absoluteRendite = ((CurrentPrice - BuyInPrice) / CurrentPrice) * 100;
                        return absoluteRendite;
                    }
                case "week":
                    decimal weeklyReturn;
                    switch (Name)
                    {
                        case "Tesla":
                            weeklyReturn = ((CurrentPrice - PriceAWeekAgo) / PriceAWeekAgo) * 100;
                            return weeklyReturn;
                        case "Lufthansa":
                            weeklyReturn = ((CurrentPrice - PriceAWeekAgo) / PriceAWeekAgo) * 100;
                            return weeklyReturn;
                        default:
                            return 404;
                    }
                default:
                    return 404;
            }
        }
    }
}
