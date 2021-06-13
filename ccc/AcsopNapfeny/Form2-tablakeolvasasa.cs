using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcsopNapfeny
{
    //form2 az adattáblák megjelenítéséhez
    public partial class Form2 : Form
    {
        private Kapcsolodas ujkapcsolat;
        string felhasznalo, lista, azoJelszava;
        int count = 0, rowIndex = 0;
        int szamlalo = 0;

        //napfénylista tábla cellaklikk kezeőlje
        private void dtgwNapfenyLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dtgwNapfenyLista.Rows[e.RowIndex];

                string adatok = string.Concat(row.Cells[0].Value.ToString(), " ; ", row.Cells[1].Value.ToString(), " ; ", row.Cells[2].Value.ToString(), " ; ", row.Cells[3].Value.ToString(), " ; ", "Hátralévő választás: ", 10 - szamlalo);
                MessageBox.Show("A kiválasztott adatsor: " + adatok, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                szamlalo = szamlalo + 1; 
            }
            if (szamlalo == 10)
            {
                DialogResult valasz = MessageBox.Show("Kíván-e újabb személy adatait rögzíteni?", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (valasz == DialogResult.No)
                {
                    MessageBox.Show("Köszönöm, hogy használta a programot! További szép napot!");
                    Application.Exit();
                }
                
            }
        }
        //teljes tábla cellaclick eseménykövetője, teljes kiírással jó megoldás
        private void dtgwMinden_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                //a kattitnott sor és számláló kiíratása
                DataGridViewRow row = this.dtgwMinden.Rows[e.RowIndex];
                string adatok = string.Concat(row.Cells[1].Value.ToString(), " ; ", row.Cells[2].Value.ToString(), " ; ", row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), " ; ", row.Cells[5].Value.ToString(), " ; ", "Hátralévő választás: ", 10 - szamlalo);
                MessageBox.Show("A kiválasztott adatsor: " + adatok, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                szamlalo = szamlalo + 1;
                //8b, a napfényes/sötét napok számmá alakítása és összehasonlítása
                int npf20 = Convert.ToInt32(row.Cells[4].Value);
                int npf80 = Convert.ToInt32(row.Cells[5].Value);
                if (npf20 > npf80)  //feltétel, melyik nagyobb
                {
                    //üzenet és beillesztés a meghatározott táblába
                    MessageBox.Show("Sötétkorszak táblába írom az adatokat");
                    string ujNapfenySotetkorszak = "INSERT INTO sotetkorszak VALUES('" + row.Cells[0].Value.ToString() + "','" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "')";
                    ujkapcsolat.Ujadat(ujNapfenySotetkorszak);
                }
                else
                {
                    //üzenet és beillesztés a meghatározott táblába
                    MessageBox.Show("Az adatokat a afenyidoszaka táblába mentem");
                    string ujNapfenyAfenyidoszaka = "INSERT INTO afenyidoszaka VALUES('" + row.Cells[0].Value.ToString() + "','" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() + "')";
                    ujkapcsolat.Ujadat(ujNapfenyAfenyidoszaka);
                }
            }
            if (szamlalo == 10)
            {
                DialogResult valasz = MessageBox.Show("Kíván-e újabb személy adatait rögzíteni?", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (valasz == DialogResult.No)
                {
                    MessageBox.Show("Köszönöm, hogy használta a programot! További szép napot!");
                    Application.Exit();
                }

            }
        }

        public Form2()
        {
            InitializeComponent();
            ujkapcsolat = new Kapcsolodas();
        }

        public void tablatoltes()   //4db tábla betöltése
        {
            DataTable dtAtlagtabla;
            lista = "SELECT * FROM atlag";
            dtAtlagtabla = ujkapcsolat.Tablaz(lista);
            if (dtAtlagtabla.Rows.Count > 0)
            {
                dtgwNapfenyLista.DataSource = dtAtlagtabla;
            }
            dtgwNapfenyLista.Rows[0].Selected = true;

            //7. CSAK DÁTUM megjelenítése (sql utasításban)
            DataTable dtSotettabla;
            lista = "SELECT datum FROM sotet";
            dtSotettabla = ujkapcsolat.Tablaz(lista);
            if (dtSotettabla.Rows.Count > 0)
            {
                dtgwVilagos.DataSource = dtSotettabla;
            }
            dtgwVilagos.Rows[0].Selected = true;

            DataTable dtVilagostabla;
            lista = "SELECT * FROM vilagos";
            dtVilagostabla = ujkapcsolat.Tablaz(lista);
            if (dtVilagostabla.Rows.Count > 0)
            {
                dtgwSotet.DataSource = dtVilagostabla;
            }
            dtgwSotet.Rows[0].Selected = true;

            DataTable dtMindentabla;
            lista = "SELECT * FROM minden";
            dtMindentabla = ujkapcsolat.Tablaz(lista);
            if (dtMindentabla.Rows.Count > 0)
            {
                dtgwMinden.DataSource = dtMindentabla;
            }
            dtgwMinden.Rows[0].Selected = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //ujkapcsolat = new Kapcsolodas();
            tablatoltes();
            MessageBox.Show("Kérem válasszon egymást követően 10 db évszámot a 4 oszlopos táblázatból!");
        }


    }
}
