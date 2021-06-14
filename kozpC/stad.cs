using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gyakorlas_Stadionok
{
    public class Stadion
    {
        string név;
        string város;
        int csapatokSzáma;
        DateTime mikortól;
        DateTime meddig;
    

    public string Név
     {
            get
            {
                return this.név;
            }

            set
            {
                this.név = value;
            }
     }

    public string Város
     {
            get
            {
                return this.város;
            }

            set
            {
                this.város = value;
            }
     }

    public int CsapatokSzáma
    {
            get
            {
                return this.csapatokSzáma;
            }

            set
            {
                this.csapatokSzáma = value;
            }
    
    }

    public DateTime Mikortól
        {
            get
            {
                return this.mikortól;
            }

            set
            {
                this.mikortól = value;
            }
        }

    public DateTime Meddig
        {
            get
            {
                return this.meddig;
            }

            set
            {
                this.meddig = value;
            }
        }

        public Stadion (string sor)
        {
            string[] adatok = sor.Split(";");
            this.Név= adatok[0] ;
            this.Város = adatok[1];
            this.CsapatokSzáma = Convert.ToInt32(adatok[2]);
            this.Mikortól = Convert.ToDateTime(adatok[3]);
            this.Meddig = Convert.ToDateTime(adatok[4]);
        }

        public static List<Stadion> Betöltés(string fájlnév)
        {
            List<Stadion> segéd = new List<Stadion>();
            foreach (string sor in File.ReadAllLines(fájlnév, Encoding.UTF8).Skip(1))
            {
                segéd.Add(new Stadion(sor));
            }
            return segéd;
        }

     class Program
        {
            static void Main(string[] args)
            {
                List<Stadion> stadionok = Stadion.Betöltés("stadionok.csv");

                Console.WriteLine("3. feladat: {0} db stadion adatai találhatóak", stadionok.Count());

                Console.WriteLine("4. feladat:");
                foreach (var item in stadionok.Where(x => x.Meddig.Year == 1890))
                {
                    Console.WriteLine("\t{0}, {1}", item.Név, item.Város);
                }

                Console.WriteLine("5. feladat: Kérek egy évszámot 1800 és 2019 között");
                int évszám = 0;
                do
                {
                    try
                    {
                        évszám = Convert.ToInt32(Console.ReadLine());
                        if (évszám > 2019 || évszám < 1800) throw new Exception();
                    }
                    catch
                    {
                        Console.WriteLine("Hibás adat, kérek egy 1800 és 2019 közötti számot!");
                    }
                } while (évszám > 2019 || évszám < 1800);
                Console.WriteLine("6. feladat {0} stadion", stadionok.Count(x => x.Mikortól.Year <= évszám && x.Meddig.Year >= évszám));

                Console.ReadLine();



                }
            }
        }




}
 