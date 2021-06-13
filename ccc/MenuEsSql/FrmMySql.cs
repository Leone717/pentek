using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MenuEsSql
{
    public partial class FrmMySql : Form
    {
        Point balfenn = new Point();
        private Kapcsolodas ujkapcsolat;
        string felhasznalo, lista, azoJelszava;
        int count = 0, rowIndex = 0;

        public int uresmezoellenoriz()
        {
            int ures = 0;
            if (tbNev.Text == "")
                ures += 1;//+1
            if (tbAdo.Text == "")
                ures += 1;//+2
            if (tbCim.Text == "")
                ures += 1;//+4
            if (tbTelefon.Text == "")
                ures += 1;//8
            if (tbEmail.Text == "")
                ures += 1;//+16
            return ures;
        }
        public void torol() //textBox mezők kitisztítása
        {
            tbNev.Clear(); //textBox1.Text="";
            tbAdo.Clear();
            tbCim.Clear();
            tbTelefon.Clear();
            tbEmail.Clear();
        }

        public void tablatoltes()   //adott tábla betöltése
        {
            DataTable dtNyitotabla;
            lista = "SELECT * FROM partnerek";
            dtNyitotabla = ujkapcsolat.Tablaz(lista);
            if (dtNyitotabla.Rows.Count > 0)
            {
                dtgwPartnerlista.DataSource = dtNyitotabla;
            }
            dtgwPartnerlista.Rows[0].Selected = true;
            //Ha a rekordok száma kevesebb mint 2; ne jelenjen meg a navigáció
        }
        public FrmMySql(string kapott)
        {
            InitializeComponent();
            ujkapcsolat = new Kapcsolodas();
            balfenn = new Point(50, 50);
            this.Location = balfenn;
            string[] connecthez;
            connecthez = kapott.Split(',');
            felhasznalo = connecthez[0];
            azoJelszava = connecthez[1];
            connecthez = null;

        }
        private void btnHozzaad_Click(object sender, EventArgs e)
        {
            int nemIrtAMezobeSemmit = uresmezoellenoriz();
            if (nemIrtAMezobeSemmit > 0)
            {
                MessageBox.Show("Töltsön ki minden mező!!!!");
                //torol();
            }
            else
            {
                //Ököl (ökör) szabály
                // INSERT INTO tablanev VALUES ('"+textBox2.Text+"',)
                //Ha az érték nem string, akkor tablanev VALUES ('+textBox2.Text+',)
                // ha szám, de nem int akkor legyen string!!!!!!
                string ujPartner = "INSERT INTO partnerek VALUES('" + tbAdo.Text + "','" + tbNev.Text + "','" + tbCim.Text + "', '" + tbTelefon.Text + "','" + tbEmail.Text + "')";
                ujkapcsolat.Ujadat(ujPartner);

                tablatoltes();
                dtgwPartnerlista.Refresh();
                dtgwPartnerlista.Update();
                torol();
            }
        }

        private void Partnerlista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dtgwPartnerlista.Rows[e.RowIndex];

                tbAdo.Text = row.Cells[0].Value.ToString();
                tbNev.Text = row.Cells[1].Value.ToString();
                tbCim.Text = row.Cells[2].Value.ToString();
                tbTelefon.Text = row.Cells[3].Value.ToString();
                tbEmail.Text = row.Cells[4].Value.ToString();
                btnHozzaad.Visible = false;
                label8.Visible = true;
            }
        }
        private void btnModosit_Click(object sender, EventArgs e)
        {
            int ures = uresmezoellenoriz();
            if (ures > 0)
            {
                MessageBox.Show("Töltsön ki minden mezőt!!!!");
                //torol();
            }
            else
            {
                string adatModosit = "UPDATE partnerek SET nev = '" + tbNev.Text + "',cim = '" + tbCim.Text + "',telefon = '" + tbTelefon.Text + "', email = '" + tbEmail.Text + "' WHERE adoszam= '" + tbAdo.Text + "'";

                ujkapcsolat.Modosit(adatModosit);
                tablatoltes();
                dtgwPartnerlista.Refresh();
                dtgwPartnerlista.Update();
                torol();
            }
        }

        private void btnTorol_Click(object sender, EventArgs e)
        {

            DialogResult melyikGomb = MessageBox.Show("Ez a művelet véglegesen törli az adatot!!!!", "Figyelmeztetés", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (melyikGomb == DialogResult.OK)
            {
                string adatTorol = "DELETE FROM partnerek WHERE adoszam = '" + tbAdo.Text + "'";
                ujkapcsolat.Torol(adatTorol);

                torol();
                tablatoltes();
                dtgwPartnerlista.Refresh();
                dtgwPartnerlista.Update();
            }
        }

        private void btnListaz_Click(object sender, EventArgs e)
        {
            dtgwPartnerlista.Visible = false;
            btnListaz.Visible = false;


            DataTable dtEgysortabla;
            lista = "SELECT nev, adoszam FROM partnerek";
            dtEgysortabla = ujkapcsolat.Tablaz(lista);

            if (dtEgysortabla.Rows.Count > 0)
            {
                dtgwLathatatlan.DataSource = dtEgysortabla;
            }
            lstwNevek.Items.Clear();

            //Listview beolvasása
            var people = GetPeopleList();
            foreach (var person in people)
            {
                var row = new String[] { person.nev, person.adoszam };
                var lvi = new ListViewItem(row);
                lvi.Tag = person;
                lstwNevek.Items.Add(lvi);
            }

            lstwNevek.GridLines = true;
            lstwNevek.View = View.Details;
            //listView1.Columns.Add("Partnerek nevei", 159);
            lstwNevek.Visible = true;
            //ujkapcsolat.CloseConnection();
        }

        private List<Person> GetPeopleList()
        {
            var nevAdoszam = new List<Person>();

            for (int i = 0; i < dtgwLathatatlan.Rows.Count - 1; i++)
            {
                nevAdoszam.Add(new Person() { nev = dtgwLathatatlan.Rows[i].Cells[0].Value.ToString(), adoszam = dtgwLathatatlan.Rows[i].Cells[1].Value.ToString() });
            }
            return nevAdoszam;
        }

        private void btnValaszt_Click(object sender, EventArgs e)
        {
            tbAdo.Clear();
            dtgwLathatatlan.DataSource = null;
            var kivalasztott = new Person();
            if (lstwNevek.SelectedItems.Count > 0)
            {
                kivalasztott = (Person)lstwNevek.SelectedItems[0].Tag;
                tbAdo.Text = kivalasztott.adoszam;
                //MessageBox.Show(textBox2.Text);
            }
            string lekerdezes = "SELECT * FROM partnerek WHERE adoszam='" + tbAdo.Text.Trim() + "'";
            DataTable dtEgysortabla;

            dtEgysortabla = ujkapcsolat.Tablaz(lekerdezes);

            if (dtEgysortabla.Rows.Count > 0)
            {
                dtgwLathatatlan.DataSource = dtEgysortabla;
                this.dtgwLathatatlan.Rows[0].Selected = true;
                tbAdo.Text = dtgwLathatatlan.Rows[0].Cells[0].Value.ToString();
                tbNev.Text = dtgwLathatatlan.Rows[0].Cells[1].Value.ToString();
                tbCim.Text = dtgwLathatatlan.Rows[0].Cells[2].Value.ToString();
                tbTelefon.Text = dtgwLathatatlan.Rows[0].Cells[3].Value.ToString();
                tbEmail.Text = dtgwLathatatlan.Rows[0].Cells[4].Value.ToString();
            }
            btnValaszt.Visible = false;
            lstwNevek.Visible = false;
            btnListaz.Visible = true;
            tablatoltes();
            dtgwPartnerlista.Visible = true;
            //elérni, hogy a kiválasztott rekord az azonos legyen a listából kiválasztott rekorddal
        }

        private void btnUres_Click(object sender, EventArgs e)//mezők kiűrítése gomb
        {
            if (!String.IsNullOrEmpty(tbNev.Text))
            {
                torol();
                btnHozzaad.Visible = true;
                label8.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e) //bezárás gomb
        {
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)    //Timer a dtgwParnerlista sorai számának frissítéséhez
        {
            //count = dtgwPartnerlista.RowCount;
            lbStatus.Text = "A nyilvántartásban jelenleg " + dtgwPartnerlista.RowCount.ToString() + " ügyfél van.";
            if (dtgwPartnerlista.RowCount <= 2)
            {
                btnBefore.Visible = false;
                btnNext.Visible = false;
                btnFirst.Visible = false;
                btnLast.Visible = false;
            }
            else
            {
                btnBefore.Visible = true;
                btnNext.Visible = true;
                btnFirst.Visible = true;
                btnLast.Visible = true;
            }
        }

        // Navigációs vezérlők
        private void btnFirst_Click(object sender, EventArgs e) //Elejére gomb
        {
            if (rowIndex < dtgwPartnerlista.RowCount)
            {
                if (rowIndex >= 0)
                {
                    dtgwPartnerlista.Rows[rowIndex].Selected = false;
                    dtgwPartnerlista.Rows[0].Selected = true;
                }
                else
                {
                    MessageBox.Show("A legelső rekord van kijelölve", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)//Végére button
        {
            if (rowIndex < dtgwPartnerlista.RowCount)
            {
                if (rowIndex < dtgwPartnerlista.RowCount - 1) //index értéke 0val kezdődik így -1
                {
                    dtgwPartnerlista.Rows[rowIndex].Selected = false; //kijelölés megszűnik adott soron
                    dtgwPartnerlista.Rows[dtgwPartnerlista.RowCount - 1].Selected = true; //növelem 1el a kijelölt sort
                }
                else
                {
                    MessageBox.Show("A legutolsó rekord van kijelölve", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnBefore_Click(object sender, EventArgs e)    //Előző gomb
        {
            if (rowIndex < dtgwPartnerlista.RowCount)
            {
                if (rowIndex > 0)   //ha 0, akkor nincs hova az elejére ugorni, mivel eleve ott vok
                {
                    dtgwPartnerlista.Rows[rowIndex].Selected = false; //kijelölés megszűnik adott soron
                    dtgwPartnerlista.Rows[--rowIndex].Selected = true; //először csökkentek 1-el
                }
                else
                {
                    MessageBox.Show("A legelső rekord van kijelölve", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e) //Következő button
        {
            if (rowIndex < dtgwPartnerlista.RowCount)
            {
                if (rowIndex < dtgwPartnerlista.RowCount - 1) //index értéke 0val kezdődik így -1
                {
                    dtgwPartnerlista.Rows[rowIndex].Selected = false; //kijelölés megszűnik adott soron
                    dtgwPartnerlista.Rows[++rowIndex].Selected = true; //növelem 1el a kijelölt sort
                }
                else
                {
                    MessageBox.Show("A legutolsó rekord van kijelölve", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dtNyitotabla;
            //like %tbSearch.Text%
            string keresettAdat = "SELECT * FROM partnerek WHERE nev like '%" + tbSearch.Text + "%'";
            dtNyitotabla = ujkapcsolat.Tablaz(keresettAdat);
            if (dtNyitotabla.Rows.Count > 0)
            {
                dtgwPartnerlista.DataSource = dtNyitotabla;
            }
            dtgwPartnerlista.Refresh();
            dtgwPartnerlista.Update();

        }


        private void label7_MouseHover(object sender, EventArgs e)//a Keresés label-re víve az egeret lefut
        {
            keresoEllenorzes();
        }

        private void keresoEllenorzes() //Tájékoztatás, ha nem üres a tbSearch textBox
        {
            if (!String.IsNullOrEmpty(tbSearch.Text))
            {
                MessageBox.Show("Ha a teljes listát akarja látni, töröljön ki minden karaktert a keresőmezőből!!!!!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstwNevek_SelectedIndexChanged(object sender, EventArgs e) //lstwNevek(jobb oldali Listanevadoszam követése
        {
            var kivalasztott = new Person();
            if (lstwNevek.SelectedItems.Count > 0)
            {
                if (lstwNevek.SelectedItems[0].Tag != null)
                {
                    kivalasztott = (Person)lstwNevek.SelectedItems[0].Tag;
                }
                else
                {
                    kivalasztott = null;
                }

                btnValaszt.Visible = true;
            }
        }

        private void FrmMySql_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Figyelmeztetés! Mielőtt új adatokat írnál a mezőkbe kattints a \"A mezők kiürítése\" gombra!!!");
            btnHozzaad.Visible = false;
            label8.Visible = true;
            tablatoltes();
        }
        private void button2_Click(object sender, EventArgs e)  //KI-BE váltógomb
        {
            button2.FlatStyle = FlatStyle.Flat;

            if (button2.Text == "Passzív")
            {
                button2.Image = Image.FromFile("on.jpg");
                button2.BackColor = Color.White;
                button2.ForeColor = Color.White;
                button2.Text = "Aktív";
            }
            else if (button2.Text == "Aktív")
            {
                button2.Image = Image.FromFile("off.jpg");
                button2.BackColor = Color.White;
                button2.ForeColor = Color.White;
                button2.Text = "Passzív";
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Először töröld a beviteli mezőket, majd írj be új adatokat");
        }

        private void dtgwPartnerlista_MouseHover(object sender, EventArgs e)
        {
            int ures = uresmezoellenoriz();
            if (dtgwPartnerlista.RowCount < 1 && ures == 0)
                keresoEllenorzes();
        }




        private void btnDiags_Click(object sender, EventArgs e)     //Diagram gomb eseménye a megjelnítéshez
        {
            dtgwPartnerlista.Visible = false;
            btnDiags.Visible = false;
            dtgwTranzakcio.Visible = true;
            btnDiagsTorol.Visible = true;

            DataTable dtcharttabla;
            lista = "SELECT tranzakcioid, mennyiseg FROM tranzakcio";
            dtcharttabla = ujkapcsolat.Tablaz(lista);
            if (dtcharttabla.Rows.Count > 0)
            {
                dtgwTranzakcio.DataSource = dtcharttabla;
            }


            chrtDiagram.Visible = true;
            ChartArea diagramTerulet = new ChartArea();
            diagramTerulet.AxisX.MajorGrid.LineColor = Color.LightGray;
            diagramTerulet.AxisY.MajorGrid.LineColor = Color.LightGray;
            diagramTerulet.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            diagramTerulet.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            chrtDiagram.ChartAreas.Add(diagramTerulet);

            chrtDiagram.Series.Add(new Series());
            //0. és 1. oszlop értékei:
            chrtDiagram.Series[0].XValueMember = dtgwTranzakcio.Columns[0].DataPropertyName;
            chrtDiagram.Series[0].YValueMembers = dtgwTranzakcio.Columns[1].DataPropertyName;
            chrtDiagram.DataSource = dtgwTranzakcio.DataSource;

            chrtDiagram.Series[0].ChartType = SeriesChartType.Column;
            string tipus = "png"; //"jpg", "bmp"
            string nev = mentesinev(tipus);
            chrtDiagram.SaveImage(nev, ChartImageFormat.Png);//Ha nem png át kell irni

        }
        private void btnDiagsTorol_Click(object sender, EventArgs e)    //DiagramTöröl gomb a Diagram alatt
        {
            dtgwTranzakcio.Visible = false;
            dtgwPartnerlista.Visible = true;
            btnDiagsTorol.Visible = false;
            btnDiags.Visible = true;
            chrtDiagram.Visible = false;


            chrtDiagram = null;
            //chrtDiagram.Dispose();
        }

        private string mentesinev(string tipus)
        {
            DateTime MentesIdopont = DateTime.Now;
            int year = MentesIdopont.Year;
            int month = MentesIdopont.Month;
            int day = MentesIdopont.Day;
            int hour = MentesIdopont.Hour;
            int minute = MentesIdopont.Minute;
            int second = MentesIdopont.Second;
            int millisecond = MentesIdopont.Millisecond;

            string path = year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + "." + tipus;
            return path;
        }
    }
}
