using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jackie0610
{
    class Stat
    {
        int versenyzesEve;
        int versenyekszama;
        int megnyertversenyek;
        int dobogok;
        int elsohelyrolindulas;
        int leggyorsabbkor;

        public int VersenyzesEve { get => versenyzesEve; set => versenyzesEve = value; }
        public int Versenyekszama { get => versenyekszama; set => versenyekszama = value; }
        public int Megnyertversenyek { get => megnyertversenyek; set => megnyertversenyek = value; }
        public int Dobogok { get => dobogok; set => dobogok = value; }
        public int Elsohelyrolindulas { get => elsohelyrolindulas; set => elsohelyrolindulas = value; }
        public int Leggyorsabbkor { get => leggyorsabbkor; set => leggyorsabbkor = value; }

        public Stat(string sor)
        {
            string[] m = sor.Split("\t");
            versenyzesEve = Convert.ToInt32(m[0]);
            versenyekszama = Convert.ToInt32(m[1]);
            megnyertversenyek = Convert.ToInt32(m[2]);
            dobogok = Convert.ToInt32(m[3]);
            elsohelyrolindulas = Convert.ToInt32(m[4]);
            leggyorsabbkor = Convert.ToInt32(m[5]);
        }
    }
    class Jackie
    {
        static void Main(string[] args)
        {
            List<Stat> statisztika = new List<Stat>();
            foreach (var i in File.ReadAllLines("jackie.txt").Skip(1))
            {
                statisztika.Add(new Stat(i));
            }

            Console.WriteLine($"3. feladat: {statisztika.Count}");

            // 4. feladata

            Stat evLegtobbverseny = statisztika.First();
            foreach (var i in statisztika)
            {
                if (evLegtobbverseny.Versenyekszama < i.Versenyekszama)
                {
                    evLegtobbverseny = i;
                }
            }
            Console.WriteLine($"4. feladat: {evLegtobbverseny.VersenyzesEve}");

            //5. feladat
            int hatvanasEvek = 0;
            int hetvenesEvek = 0;

            foreach(var i in statisztika)
            {
                if (i.VersenyzesEve < 1970)
                {
                    hatvanasEvek += i.Megnyertversenyek;
                }
                else
                {
                    hetvenesEvek += i.Megnyertversenyek;
                }
            }
            Console.WriteLine("5. feladat: ");
            Console.WriteLine($"70-es évek: {hetvenesEvek} megnyert verseny");
            Console.WriteLine($"60-as évek: {hatvanasEvek} megnyert verseny");

            Console.WriteLine($"6. feladat: jackie.html");
            StreamWriter sw = new StreamWriter("jackie.html", false, System.Text.Encoding.UTF8);
            sw.WriteLine("<!doctype html>");
            sw.WriteLine("<html>");
            sw.WriteLine("<head></head>");
            sw.WriteLine("<style>td {" +
                "\tborder:1px solid black;" +
                "}" +
                "</style>");
            sw.WriteLine("<body>");
            sw.WriteLine("<h1>Jackie Stewart</h1>");
            sw.WriteLine("<table>");
            foreach (Stat stat in statisztika)
            {
                sw.WriteLine($"<tr>" +
                    $"<td>{stat.VersenyzesEve}</td>" +
                    $"<td>{stat.Versenyekszama}</td>" +
                    $"<td>{stat.Megnyertversenyek}</td>" +
                    $"</tr>");
            }
            sw.WriteLine("</table>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
            sw.Flush();
            sw.Close();
            Console.ReadLine();

            Console.ReadKey();
        }
    }
}
