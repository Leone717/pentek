using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpeningWeekend0609
{
    class Film
    {
        public string EredetiCim { get; private set; }
        public string MagyarCim { get; private set; }
        public DateTime Bemutato { get; private set; }
        public string Forgalmazo { get; private set; }
        public int Bevetel { get; private set; }
        public int Latogato { get; private set; }

        public Film(string sor)
        {
            string[] m = sor.Split(';');
            EredetiCim = m[0];
            MagyarCim = m[1];
            Bemutato = DateTime.Parse(m[2]);
            Forgalmazo = m[3];
            Bevetel = int.Parse(m[4]);
            Latogato = int.Parse(m[5]);
        }

        public bool Wstart
        {
            get
            {
                List<String> seged = new List<string>(EredetiCim.Split(' '));
                seged.AddRange(MagyarCim.Split(' '));
                return seged.All(x => x.StartsWith("W", true, System.Globalization.CultureInfo.CurrentCulture));
            }
        }
    }
        class Program
    {
        static void Main(string[] args)
        {
            List<Film> filmek = new List<Film>();
            foreach(var i in File.ReadAllLines("nyitohetvege.txt").Skip(1))
            {
                filmek.Add(new Film(i));
            }

            Console.WriteLine($"3. feladat: Filmek száma az állományban: {filmek.Count}");

            //4. feladat

            ulong bevetel = 0;
            foreach (var i in filmek)
            {
                if (i.Forgalmazo == "UIP")
                {
                    bevetel = bevetel + (uint)i.Bevetel;
                    //Console.WriteLine(i.Bevetel);
                }
            }
            Console.WriteLine($"4. feladat: UIP Duna Film forgalmazó 1. hetes bevételeinek összege: {bevetel} Ft");

            //5. feladat
            Film legtobblatogato = filmek.First();
            foreach(var i in filmek)
            {
                if(legtobblatogato.Latogato < i.Latogato)
                {
                    legtobblatogato = i;
                }
            }
            Console.WriteLine("5. feladat: Legtöbb látogató az első héten:");
            Console.WriteLine("Eredeti cím: " + legtobblatogato.EredetiCim );
            Console.WriteLine("Magyar cím: " + legtobblatogato.MagyarCim);
            Console.WriteLine("Forgalmazó: " + legtobblatogato.Forgalmazo);
            Console.WriteLine("Bevétel az első héten" + legtobblatogato.Bevetel + " Ft");
            Console.WriteLine("Látogatók száma: " + legtobblatogato.Latogato + " fő");

            Console.WriteLine($"6. feladat: Ilyen film {(filmek.Any(x=>x.Wstart) ? " " : " nem ")} volt!");

            var stat = filmek.GroupBy(g => g.Forgalmazo);
            List<string> ki = new List<string>();
            ki.Add("fogralmazo, filmekSzama");
            foreach (var i in stat)
            {
                if (i.Count() > 1)
                {
                    ki.Add($"{i.Key};{i.Count()}");
                }
            }
            //File.WriteAllLines("stat.csv", ki);

            //8. feladat
            double maxDiffNapokban = 0;
            var InterCom = filmek.Where(x => x.Forgalmazo == "InterCom").ToList();
            for (int i = 0; i < InterCom.Count - 1; i++)
            {
                double aktDiffNapokban = (InterCom[i + 1].Bemutato - InterCom[i].Bemutato).TotalDays;
                if (aktDiffNapokban > maxDiffNapokban)
                {
                    maxDiffNapokban = aktDiffNapokban;
                }
            }
            Console.WriteLine($"8. feladat: A leghosszabb időszak két InterCom-os bemutató között: {maxDiffNapokban}nap");
            
                
        Console.ReadKey();
        }
    }
}
