using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GyakorlasKifejezesekImi
{
    class Program
    {
        static void Main(string[] args)
        {

            Random r = new Random();
            var n = r.Next(500, 1000);

            Console.WriteLine("Generált Random számunk:" + n);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            HashSet<int> hash = new HashSet<int>();

            foreach (var i in File.ReadAllLines("megyeszekhelyek.txt").Skip(1))
            {
                string[] m = i.Split(";");
                dict.Add(m[0], m[1]);
            }

            Console.WriteLine($"A Játékban {dict.Count} megyéből választunk ki N darabot");

            int N = 0;
            do
            {
                Console.Write($"Kérem a kitalálandó megyeszékhelyek számát [2-{dict.Count}] N=");
                N = int.Parse(Console.ReadLine());
            } while (N < 2 || N > dict.Count);

            HashSet<int> hash11 = new HashSet<int>();
            do
            {
                int db = r.Next(dict.Count);
                hash11.Add(db);
            } while (hash11.Count < N);

            Console.WriteLine("6. feladat: Segítség");
            int maxHossz = 0;
            int minHossz = int.MaxValue;
            foreach (var i in hash11)
            {
                if (dict.ElementAt(i).Value.Length > maxHossz) maxHossz = dict.ElementAt(i).Value.Length;
                if (dict.ElementAt(i).Value.Length < minHossz) minHossz = dict.ElementAt(i).Value.Length;
            }

            int jótippDb = 0;
            foreach (var i in hash11)
            {
                Console.Write("{0} ---> {1}", dict.ElementAt(i).Key, dict.ElementAt(i).Value[0]);
                string tipp = Console.ReadLine();
                if (dict.ElementAt(i).Value[0] + tipp == dict.ElementAt(i).Value) jótippDb++;
            }


            Console.ReadKey();
   
        }
    }
}
