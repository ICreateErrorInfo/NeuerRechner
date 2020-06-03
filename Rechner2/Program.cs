using System;
using System.Linq;
using System.Collections.Generic;

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

            foreach (var op in GetOperatoren())
            {
                for (int i = 0; i < operatoren.Count; i++)
                {
                    if (operatoren[i] == op)
                    {
                        zahlen.Insert(i + 1, Rechner.Rechne(op, zahlen, i));
                        zahlen.RemoveAt(i);
                        zahlen.RemoveAt(i + 1);
                        operatoren.RemoveAt(i);
                        i--;
                    }
                }
            }
            Console.WriteLine(zahlen[0] + " ");
        }


        public static bool IsOperator(string eingabe)
        {
            if (GetOperatoren().Contains(eingabe))
            {
                return true;
            }
            return false;
        }

        public static IEnumerable<string> GetOperatoren()
        {
            yield return Operatoren.Potenz;
            yield return Operatoren.Mal;
            yield return Operatoren.Geteilt;
            yield return Operatoren.Minus;
            yield return Operatoren.Plus;
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
                        for (int i = 1; i < zahlen[postiton + 1]; i++)
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
