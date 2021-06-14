using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//1. feladat
namespace Balkezesek_vizsgagyak
{   //2. feladat
    public class balkezes
    {
        string nev;
        DateTime elso;
        DateTime utolso;
        int suly;
        int magassag;

        public balkezes(string sor)
        {
            string[] m = sor.Split(";");
            nev = m[0];
            elso = Convert.ToDateTime(m[1]);
            utolso = Convert.ToDateTime(m[2]);
            suly = Convert.ToInt32(m[3]);
            magassag = Convert.ToInt32(m[4]);
        }

        public double MagassagCm
        {
            get
            {
                return magassag * 2.54;
            }
        }
        
        public string Nev 
        {
            get
            {
                return this.nev;
            }
            set
            {
                this.nev = value;
            }
        }

        public DateTime Elso
        {
            get
            {
                return this.elso;
            }
            set
            {
                this.elso = value;
            }
        }

        public DateTime Utolso
        {
            get
            {
                return this.utolso;
            }
            set
            {
                this.utolso = value;
            }
        }
        public int Suly
        {
            get
            {
                return this.suly;
            }
            set
            {
                this.suly = value;
            }
        }
        public int Magassag
        {
            get
            {
                return this.magassag;
            }
            set
            {
                this.magassag = value;
            }
        }

        public static IEnumerable<balkezes> betolt(string file)
        {
            foreach (var sor in File.ReadAllLines(file, Encoding.UTF8).Skip(1))
            {
                yield return new balkezes(sor);
            }
        }
        public static List<balkezes> betolt2(string fajl)
        {
            List<balkezes> seged = new List<balkezes>();
            foreach (var sor in File.ReadAllLines(fajl, Encoding.UTF8).Skip(1))
            {
                seged.Add(new balkezes(sor));
            }
            return seged;
        }
 

    }
    class Program
    {
        static void Main(string[] args)
        {
            
            List<balkezes> balkezesek = balkezes.betolt("balkezesek.csv").ToList();
            
            List<balkezes> balkezesek2 = balkezes.betolt2("balkezesek.csv");

            Console.WriteLine($"3. feladat: {balkezesek.Count()}");

            //4. feladat:
            Console.WriteLine("4. feladat:");
            foreach (var i in balkezesek2.Where(x => x.Utolso.Year == 1999 && x.Utolso.Month == 10))
            {
                Console.WriteLine("\t {0}, {1}", i.Nev, i.MagassagCm);
            }
           
            Console.WriteLine("5. feladat:");
            int evszam;
            Console.WriteLine("Kérek egy 1990 és 1999 közötti évszámot!: ");
            do
            {
                
                evszam = Convert.ToInt32(Console.ReadLine());
                if (evszam > 1999 || evszam < 1990)
                {
                    Console.WriteLine("Hibás adat, kérek egy 1990 és 1999 közötti évszámot!");
                } 
            } while (evszam > 1999 || evszam < 1990);

            Console.WriteLine("6. feladat: {0:F2}", balkezesek.Where(x => x.Elso.Year <= evszam && x.Utolso.Year >= evszam).Average(x => x.Suly));

            Console.ReadKey();
        }
    }
}
