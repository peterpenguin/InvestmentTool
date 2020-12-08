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


            //initialising investments
            Investment tesla = new Investment();
            tesla.Name = "Tesla";
            Investment lufth = new Investment();
            lufth.Name = "Lufthansa";

            //user intro
            Console.WriteLine("type check or input");
            Check();


            //Console.WriteLine("absolute Tesla Rendite: " + tesla.RenditeBerechnen("abs") + "%");
            //Console.WriteLine("absolute Lufthansa Rendite: " + lufth.RenditeBerechnen("abs") + "%");
            //Console.WriteLine("wöchendliche Tesla Rendite: " + tesla.RenditeBerechnen("woech") + "%");
            //Console.WriteLine("wöchendliche Lufthansa Rendite: " + tesla.RenditeBerechnen("woech") + "%");
            Console.ReadKey();

        }
        static void Check()
        {
            string action = Convert.ToString(Console.ReadLine());
            switch (action)
            {
                case "check":
                    //check function
                    Console.WriteLine("Check gets opened");
                    break;
                case "input":
                    //input function
                    Console.WriteLine("Input gets opened");
                    break;
                default:
                    Console.WriteLine("Please only type in a valid word.");
                    Check();
                    break;
            }
        }
    }

    class Investment
    {
        //properties
        public string Name { get; set; }
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
                case "woech":
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
