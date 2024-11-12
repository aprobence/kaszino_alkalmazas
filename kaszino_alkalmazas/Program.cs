using System;
using System.Formats.Tar;
using System.Linq.Expressions;

namespace kaszino_alkalmazas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int penz = 1000;
            int vegosszeg = 0;

            Console.WriteLine("Adja meg a választását (0 = BlackJack, 1 = High-Low játék, 2 = Piros vagy Fekete, 3 = Intervallum találós játék, 4 = Jackpot):");
            int valasztas = Convert.ToInt32(Console.ReadLine());

            switch (valasztas)
            {
                case 0:
                    vegosszeg = Blackjack(penz);
                    penz = vegosszeg;
                    Console.WriteLine($"Jelenlegi egyenleged: {penz}");
                    break;
                case 1:
                    vegosszeg = highLowJatek(penz);
                    penz = vegosszeg;
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás. Kérlek, próbáld újra.");
                    break;
            }
        }

        static int tethuzas(int penz)
        {
            Console.WriteLine($"Tedd meg a téted (jelenlegi egyenleged: {penz}):");
            int tet = Convert.ToInt32(Console.ReadLine());
            while (tet > penz || tet <= 0)
            {
                Console.WriteLine($"Ilyen tétet nem tehetsz, próbáld újra (jelenlegi egyenleged: {penz}):");
                tet = Convert.ToInt32(Console.ReadLine());
            }
            return tet;
        }

        static int HuzLapot()
        {
            Random rnd = new Random();
            int pont = rnd.Next(1, 14);
            if (pont > 10)
            {
                pont = 10;
            }
            return pont;
        }

        static int Blackjack(int penz)
        {
            int teted = tethuzas(penz);
            int jatekosPont = HuzLapot() + HuzLapot();
            int kaszinoPont = HuzLapot() + HuzLapot();

            Console.WriteLine($"Lapjaid összege: {jatekosPont}");
            Console.WriteLine("Hit vagy Stand?");
            string pluszLap = Console.ReadLine().ToLower();
            if (pluszLap == "hit")
            {
                jatekosPont += HuzLapot();
            }

            while (kaszinoPont < 17)
            {
                kaszinoPont += HuzLapot();
            }

            if (kaszinoPont > 21 || (jatekosPont <= 21 && jatekosPont > kaszinoPont))
            {
                Console.WriteLine($"Nyertél, a téted hozzá lesz adva az egyenlegedhez. Kaszinó pontja: {kaszinoPont}, Te pontod: {jatekosPont}");
                return penz + teted;
            }
            else if (jatekosPont == kaszinoPont)
            {
                Console.WriteLine($"Döntetlen, a tétet visszakapod. Kaszinó pontja: {kaszinoPont}, Te pontod: {jatekosPont}");
                return penz;
            }
            else
            {
                Console.WriteLine($"Vesztettél, a téted levonásra kerül. Kaszinó pontja: {kaszinoPont}, Te pontod: {jatekosPont}");
                return penz - teted;
            }
        }

        static int highLowJatek(int penz)
        {
            int teted = tethuzas(penz);
            int kartya = HuzLapot();
            Console.WriteLine($"A kártya értéke: {kartya}.\nA következő lap értéke nagyobb vagy kisebb lesz?");
            string nagyobb = Console.ReadLine().ToLower();
            int ujabb = HuzLapot();
            if (ujabb > kartya || nagyobb == "nagyobb")
            {
                Console.WriteLine($"Jól tippetél, az új kártya értéke: {ujabb}");
                return penz + teted;
            }
            else if (ujabb == kartya) {
                Console.WriteLine("Egyenlő, a tétedet visszakapod.");
                return penz;
            }
            else
            {
                Console.WriteLine($"A tipped helytelen, az új kártya értéke: {ujabb}");
                return penz - teted;
            }
        }
    }
}
