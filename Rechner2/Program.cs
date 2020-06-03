using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Rechner2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                Berechne(input);
            }

        }

        private static void Berechne(string input)
        {
            var zahlen = new List<double>();
            var operatoren = new List<string>();
            var tockens = Tockenize(input);

            foreach (var eingabe in tockens)
            {
                if (eingabe == "=")
                {
                    continue;
                }

                if (IsOperator(eingabe))
                {
                    operatoren.Add(eingabe);
                    continue;
                }
                zahlen.Add(double.Parse(eingabe));
            }

            for(int x = 0; zahlen.Count > 1;x++)
            {
                if (operatoren.Contains(Operatoren.Potenz))
                {
                    for (int i = 0; i < operatoren.Count; i++)
                    {
                        if (operatoren[i] == Operatoren.Potenz)
                        {
                            switch (operatoren[i])
                            {
                                case Operatoren.Potenz:
                                    zahlen.Insert(i + 1, Rechner.Rechne(operatoren[i], zahlen, i));
                                    zahlen.RemoveAt(i);
                                    zahlen.RemoveAt(i + 1);
                                    operatoren.RemoveAt(i);
                                    break;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (operatoren.Contains(Operatoren.Geteilt) || operatoren.Contains(Operatoren.Mal))
                    {
                        for (int i = 0; i < operatoren.Count; i++)
                        {
                            if (operatoren[i] == Operatoren.Geteilt || operatoren[i] == Operatoren.Mal)
                            {
                                switch (operatoren[i])
                                {
                                    case Operatoren.Mal:
                                        zahlen.Insert(i + 1, Rechner.Rechne(operatoren[i], zahlen, i));
                                        zahlen.RemoveAt(i);
                                        zahlen.RemoveAt(i + 1);
                                        operatoren.RemoveAt(i);
                                        break;
                                    case Operatoren.Geteilt:
                                        zahlen.Insert(i + 1, Rechner.Rechne(operatoren[i], zahlen, i));
                                        zahlen.RemoveAt(i);
                                        zahlen.RemoveAt(i + 1);
                                        operatoren.RemoveAt(i);
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        switch (operatoren[i])
                        {
                            case Operatoren.Plus:
                                zahlen.Insert(i + 1, Rechner.Rechne(operatoren[i], zahlen, i));
                                zahlen.RemoveAt(i);
                                zahlen.RemoveAt(i + 1);
                                operatoren.RemoveAt(i);
                                break;
                            case Operatoren.Minus:
                                zahlen.Insert(i + 1, Rechner.Rechne(operatoren[i], zahlen, i));
                                zahlen.RemoveAt(i);
                                zahlen.RemoveAt(i + 1);
                                operatoren.RemoveAt(i);
                                break;
                        }
                    }
                }  
            }
            Console.WriteLine(zahlen[0] + " ");

        }


        public static bool IsOperator(string eingabe)
        {
            string[] opperators = new string[] {Operatoren.Plus, Operatoren.Minus, Operatoren.Mal, Operatoren.Geteilt, Operatoren.Potenz, "s="};

            if (opperators.Contains(eingabe))
            {
                return true;
            }
            return false;
        }

        public static List<string> Tockenize(string eingabe)
        {
            var zerhacktes = new List<string>();
            var curent = "";
            foreach (var ch in eingabe)
            {
                if (IsOperator(ch.ToString()) || ch == ' ')
                {
                    if (curent != "")
                    {
                        zerhacktes.Add(curent);
                        curent = "";
                    }
                    if (ch != ' ')
                    {
                        zerhacktes.Add(ch.ToString());
                    }
                }
                else
                {
                    curent += ch;
                }
            }

            if (curent != "")
            {
                zerhacktes.Add(curent);
                curent = "";
            }
            return zerhacktes;
        }

        class Rechner
        {
            public static double Rechne(string zeichen, IList<double> zahlen, int postiton)
            {
                double ergebnis = 0;
                ergebnis = zahlen[postiton];

                switch (zeichen)
                {
                    case Operatoren.Plus:
                        ergebnis += zahlen[postiton + 1];
                        break;
                    case Operatoren.Minus:
                        ergebnis -= zahlen[postiton + 1];
                        break;
                    case Operatoren.Mal:
                        ergebnis *= zahlen[postiton + 1];
                        break;
                    case Operatoren.Geteilt:
                        ergebnis /= zahlen[postiton + 1];
                        break;
                    case Operatoren.Potenz:
                        for(int i = 1; i < zahlen[postiton + 1];i++)
                        {
                            ergebnis *= zahlen[postiton];
                        }
                        break;
                }
                return ergebnis;
            }
        }
        class Operatoren
        {
            public const string Plus = "+";
            public const string Minus = "-";
            public const string Mal = "*";
            public const string Geteilt = "/";
            public const string Potenz = "^";
        }
    }
}
