using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseBall
{
    class Program
    {
        static void Main(string[] args)
        {
            string fajlnev = "fizetesek86.csv";
            List<Jatekos> feladatok = Jatekos.Betoltes(fajlnev);
            Console.WriteLine("\n3. feladat: A játékosok száma: " + feladatok.Count);

            Console.WriteLine("\n4. feladat: A legtöbbet kereső játékos: ");
            Jatekos maxfizetes = feladatok.First();
            foreach (var elemek in feladatok.Skip(1))
            {
                if(elemek.Fizetes >maxfizetes.Fizetes)
                {
                    maxfizetes = elemek;
                }
            }
            Console.WriteLine("Neve: " + maxfizetes.Jatekosnev + "\nFizetése dolláarban: " + maxfizetes.Fizetes);

            Console.WriteLine("\n5. feleadat: Az 1986-ban először pályára lépő játékosok: \n");
            foreach (var elsopalya86 in feladatok.Skip(1))
            {
                Console.WriteLine(elsopalya86.Jatekosnev + " nevű játékos, fizetése USD-ben: "+ elsopalya86.Fizetes);
            }

            Console.WriteLine("\n6. feleadat: fél millió $-nál többet kereső, legidősebb három játékos: \n");
            var reszGyujtemeny = new List<Jatekos>();
            var megfelelo = new List<Jatekos>();
            //kigyujtjuk az 500000$-nál többet keresőket
            foreach (var i in feladatok)
            {
                if (i.Fizetes > 500000)
                {
                    reszGyujtemeny.Add(i);
                    //Console.WriteLine(i.Jatekosnev);
                }
            }
            //sorba rendezzük születési dátum alapján
            List<Jatekos> SortedList = reszGyujtemeny.OrderBy(o => o.Szuldat).ToList();
            //for ciklussal az első 3 legidősebbet gyűjtjük ki
            for (int j = 0; j < 3; j++)
            {
                megfelelo.Add(SortedList[j]);
            }
            //kiíratás
            for (int k = 0; k < 3; k++)
            {
                Console.WriteLine(SortedList[k].Jatekosnev); 
            }

            Console.ReadKey();

        }
    }
}
