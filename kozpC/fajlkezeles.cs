using System;
using System.IO;
using System.Text;

namespace gyakorlasFajlkezeles
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rd = new Random();
            //szoveg.txt írása
            try
            {
                FileStream fs = new FileStream("szoveg.txt", FileMode.Append, FileAccess.Write, FileShare.None);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                int rdn = rd.Next(90, 121);//90-120 közötti számok generálása
                sw.Write(rdn + "\n");
                sw.Close();
                fs.Close();
                Console.WriteLine("szoveg.txt elkészült, írtam a fájlba");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt");
                Console.WriteLine(ex.Message);
            }
            //szoveg.txt olvasása
            try
            {
                FileStream fs = new FileStream("szoveg.txt", FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader sr = new StreamReader(fs);
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
