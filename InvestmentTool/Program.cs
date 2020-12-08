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
            Investment tesla = new Investment("Tesla");
            Investment lufth = new Investment("Lufthansa");
            switch (action)
            {
                case "check":
                    Console.WriteLine("\nYour returns:");
                    Console.WriteLine("\nINVESTMENT\tWEEKLY\tTOTAL");
                    //TESLA
                    Console.WriteLine(tesla.Name + "\t" + tesla.RenditeBerechnen("week") + "10%\t15%");
                    Console.WriteLine("MSCI EM\t\t12%\t17%");
                    break;
                case "input":
                    //Renditen eingeben
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
        //properties
        private string Name { get; set; }
        public Investment(string investmentName)
        {
            Name = investmentName;
        }
        public decimal AktuellerWert { get; set; }
        public decimal Kaufwert { get; set; }
        public decimal WertVor1Woche { get; set; }

        //methods
        public decimal RenditeBerechnen(string Rendite)
        {
            switch (Rendite)
            {
                case "abs":
                    decimal absoluteRendite;
                    if (AktuellerWert - Kaufwert < 0)
                    {
                        absoluteRendite = ((AktuellerWert - Kaufwert) / Kaufwert) * 100;
                        return absoluteRendite;
                    }
                    else
                    {
                        absoluteRendite = ((AktuellerWert - Kaufwert) / AktuellerWert) * 100;
                        return absoluteRendite;
                    }
                case "week":
                    decimal woechendlicheRendite;
                    decimal WertVor1Woche;
                    switch (Name)
                    {
                        case "Tesla":
                            string _tesla = @"data\tesla.txt";
                            //herausfinden wie viele Zeilen die Datei hat
                            int Zeilen = File.ReadLines(_tesla).Count();
                            WertVor1Woche = Convert.ToDecimal(File.ReadLines(_tesla).Skip(Zeilen - 2).Take(1).First());
                            AktuellerWert = Convert.ToDecimal(File.ReadLines(_tesla).Skip(Zeilen - 1).Take(1).First());
                            woechendlicheRendite = ((AktuellerWert - WertVor1Woche) / WertVor1Woche) * 100;
                            return woechendlicheRendite;
                        case "Lufthansa":
                            WertVor1Woche = 30;
                            woechendlicheRendite = ((AktuellerWert - WertVor1Woche) / WertVor1Woche) * 100;
                            return woechendlicheRendite;
                        default:
                            return 404;
                    }
                default:
                    return 404;
            }
        }
    }
}
