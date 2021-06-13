using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcsopNapfeny
{
    //form1, a fájbeolvasáshoz és adattáblába küldéshez
    public partial class Form1 : Form
    {
        private Kapcsolodas ujkapcsolat; //KAPCSOLAT!!!!!!!!!!!!!!!!!!!!!!
        //osztályszíntü változók deklarálása
        //List<napsutes> napsugar = new List<napsutes>();
        public static string egysor;
        public static string[] soreszek;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fájl beolvasása, DataGridView-ba
            DataTable dt = new DataTable();
            try
            {
                FileStream fs = new FileStream("BP_napfenytartam.txt", FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                egysor = sr.ReadLine();
                soreszek = egysor.Split(';');

                //első sor beállítása
                foreach (string oszlop in soreszek)
                {
                    dt.Columns.Add(oszlop);
                }
                while (!sr.EndOfStream)
                {
                    //napsugar.Add(new napsutes(egysor));
                    DataRow dr = dt.NewRow();
                    egysor = sr.ReadLine();
                    soreszek = egysor.Split(';');
                    for (int i = 0; i < soreszek.Length; i++)
                    {
                        dr[i] = soreszek[i];
                    }
                    dt.Rows.Add(dr);
                }
                sr.Close();
                fs.Close();
                dataGridView1.DataSource = dt;

                //beolvasott DataGridView kimentése adatbázisba !!!!!
                foreach (DataRow row in dt.Rows)
                {

                    //MessageBox.Show(row[0].ToString());
                    //string ujNapfeny = "INSERT INTO `atlag` (`datum`, `y_ss`, `y_sx`, `y_sxd`) VALUES('" + row[0].ToString() + "','" + row[1].ToString() + "','" + row[2].ToString() + "', '" + row[3].ToString() + "')";
                    //MessageBox.Show(ujNapfeny);

                    //Primary key tábla, amihez a foreign key kapcsolódik XAMP újraindítása után töltődik csak
                    ujkapcsolat = new Kapcsolodas();
                    string ujNapfeny = "INSERT INTO atlag VALUES('" + row[0].ToString() + "','" + row[1].ToString() + "','" + row[2].ToString() + "', '" + row[3].ToString() + "')";
                    ujkapcsolat.Ujadat(ujNapfeny);


                    //EZ MŰKÖDIK HIBA NÉLKÜL, NE NYÚLJ HOZZÁ, foregign key-s táblák 
                    ujkapcsolat = new Kapcsolodas();
                    string ujNapfenySotet = "INSERT INTO sotet VALUES('" + row[0].ToString() + "','" + row[4].ToString() + "')";
                    ujkapcsolat.Ujadat(ujNapfenySotet);

                    ujkapcsolat = new Kapcsolodas();
                    string ujNapfenyVilagos = "INSERT INTO vilagos VALUES('" + row[0].ToString() + "','" + row[5].ToString() + "')";
                    ujkapcsolat.Ujadat(ujNapfenyVilagos);

                    //MessageBox.Show(ujNapfenyVilagos);

                    //Egy nagy mindenes tábla feltöltése az összes oszloppal a fájból az utolsó feladatokhoz
                    ujkapcsolat = new Kapcsolodas();
                    string ujNapfenyMindenes = "INSERT INTO minden VALUES('" + row[0].ToString() + "','" + row[1].ToString() + "','" + row[2].ToString() + "','" + row[3].ToString() + "','" + row[4].ToString() + "','" + row[5].ToString() + "')";
                    ujkapcsolat.Ujadat(ujNapfenyMindenes);


                    /*tablatoltes();
                    dtgwPartnerlista.Refresh();
                    dtgwPartnerlista.Update();
                    torol();*/

                }
                MessageBox.Show("A beolvasás sikeresen megvalósítottam, bezárom a formot!");
                //Dispose(); másik bezárási opció
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("A beolvasás nem sikerült, ismeretlen hiba");

            }
        }
    }
}
