using MySql.Data.MySqlClient;
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

namespace MenuEsSql
{
    public partial class Form1 : Form
    {
        private Kapcsolodas ellenorzokapcsolat;
        string user, jelszo, hasznaltjelszo;
        string currentDir;

        public void NincsGyerek()
        {
            toolStripStatusLabel3.Text = "Nincs megnyitott, aktív belső ablak!";
        }

        public Form1()
        {
            InitializeComponent();
            ellenorzokapcsolat = new Kapcsolodas();
            this.IsMdiContainer = false;//nem tartalmazhat több formot
            SetDesktopLocation(0, 0);//kijelző bal felső sarkában legyen
            Width = Screen.PrimaryScreen.Bounds.Width;  //aktuális kijelző teljes méretéhez igazít
            Height = Screen.PrimaryScreen.Bounds.Height; //teljes képernyős lesz az ablak

        }

        //Kilépés eljárások a tsmi elemekhez
        private void tsmiKilépés_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiMasikKilep_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiKisAblak_Click(object sender, EventArgs e) //Form2-ből kisablak létrehozó fgv
        {
            string ablaktipus = "1;" + this.Width.ToString() + ";" + this.Height.ToString(); //eredmény: 1;440;230

            Form2 kicsi = new Form2(ablaktipus);
            bool MarLetreHoztam = false;
            for(int i = 0; i<Application.OpenForms.Count; i++)    //foreach helyett, megoldás ablaktöbbszörözés
            //foreach (Form nyitottAblak in Application.OpenForms)
            {
                //if (nyitottAblak.Text == "kisAblak")
                if(Application.OpenForms[i].Text == "kisAblak")
                {
                    MarLetreHoztam = true;
                    kicsi.Activate();
                    kicsi.BringToFront();
                    kicsi.Show();
                }
            }
            if (MarLetreHoztam == false)
            {
                kicsi = new Form2(ablaktipus);
                if (this.IsMdiContainer == false)
                {
                    this.IsMdiContainer = true;
                }
                kicsi.MdiParent = this;
                kicsi.Activate();
                kicsi.BringToFront();
                kicsi.Show();
            }
         }

        private void tsmiNagyAblak_Click(object sender, EventArgs e) //Form2-ből nagyablak létrehozó fgv
        {
            string ablaktipus = "2;" + this.Width.ToString() + ";" + this.Height.ToString(); //eredmény: 2;440;230

            Form2 nagy = new Form2(ablaktipus);
            bool MarLetreHoztam = false;
            for(int i = 0; i<Application.OpenForms.Count; i++)    //foreach helyett, megoldás ablaktöbbszörözés
            //foreach (Form nyitottAblak in Application.OpenForms)
            {
                //if (nyitottAblak.Text == "nagyAblak")
                if (Application.OpenForms[i].Text == "nagyAblak")
                {
                    MarLetreHoztam = true;
                    nagy.Activate();
                    nagy.BringToFront();
                    nagy.Show();
                }
            }
            if (MarLetreHoztam == false)
            {
                nagy = new Form2(ablaktipus);
                if (this.IsMdiContainer == false)
                {
                    this.IsMdiContainer = true;
                }
                nagy.MdiParent = this;
                nagy.Activate();
                nagy.BringToFront();
                nagy.Show();
            }
        }

        private void tsmiMySql_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "Aktív ablak: MySqlAblak";
            string atad = user + "," + jelszo;
            FrmMySql aMySql = new FrmMySql(atad);
            bool MarLetrehoztam = false;    //aktív form ellenőrzésére
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "MYSQL kezelés") {
                    MarLetrehoztam = true;
                    if (this.IsMdiContainer == false)
                        {
                            this.IsMdiContainer = true;
                        }
                    aMySql.MdiParent = this;
                    //Így adhatom meg a méretét a gyermekablaknak
                    aMySql.Size = new Size((this.Width - 200), (this.Height - 100));
                    aMySql.FormClosed += (s, args) => NincsGyerek();
                    aMySql.Show();
                    aMySql.Activate();
                    aMySql.BringToFront();
                }
            }
            if (MarLetrehoztam == false)
            {
                aMySql = new FrmMySql(atad);
                if (this.IsMdiContainer == false)
                {
                    this.IsMdiContainer = true;
                }
                aMySql.FormClosed += (s, args) => NincsGyerek();
                aMySql.MdiParent = this;
                aMySql.Size = new Size((this.Width - 200), (this.Height - 100));
                aMySql.Show();
                aMySql.Activate();
                aMySql.BringToFront();

            }
        }

        private void tsmiMentes_Click(object sender, EventArgs e)
        {
            currentDir = ""; //@"C:\Users\oktato\"; az user főmappáját mutatja
            try
            {
                var fb = new FolderBrowserDialog();//csak könyvtárat választ ki ezért folderBrowser
                if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)//ha ez ok akkor:
                {
                    currentDir = fb.SelectedPath;//megnézi válaszottam e ki könyvtárat és ha igen bekerül a currenDirbe
                    MessageBox.Show("Az adatokat a " + currentDir + " könyvtárba mentem", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);//"biztos jól döntöttél?"
                    ellenorzokapcsolat.Mentes(currentDir);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba " + ex.Message + " " + ex.Source);
            }
        }

        private void tsmiVisszatolt_Click(object sender, EventArgs e)
        {
            if (lbMentett.Visible == false)
            {
                lbMentett.Visible = true;
            } 
            currentDir = "";    //@C:\Users\oktato\";
            try
            {
                var fb = new FolderBrowserDialog();
                if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentDir = fb.SelectedPath;//kiválasztunk a könyvtárat, de nem azzal dolgozunk!
                    var dirInfo = new DirectoryInfo(currentDir);
                    var files = dirInfo.GetFiles("*.sql");//kigyűjti az sql fájlokat
                    foreach (var mentes in files)
                    {
                        lbMentett.Items.Add(mentes.Name);//hozzáadja az sql fájlokat
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message + " " + ex.Source);
            }
        }

        private void lbMentett_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {   //var: megengedem hogy amit először teszek bele olyan lesz a többi típus is
                var selectedFile = lbMentett.SelectedItems[0].ToString();
                if (!String.IsNullOrEmpty(selectedFile) && !string.IsNullOrEmpty(currentDir))
                {
                    var fullPath = Path.Combine(currentDir, selectedFile);//elérési útvonal típusú adat
                    lbMentett.Visible = false;//már a listBox nem kell
                    lbMentett.Dispose();//felszabadítja a memória által lefoglalt területet
                    ellenorzokapcsolat.Visszatolt(fullPath);//meghívom a visszatöltés metódust
                    MessageBox.Show("Az adatok adatbázis-táblákba történő visszaovlasása sikeres volt!!!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string a, b, c, d, f;
            belepes belep = new belepes();
            if(this.IsMdiContainer == true)
            {
                this.IsMdiContainer = false;
            }
            if (lbMentett.Visible == true)  //21.05.29. !
                lbMentett.Visible = false;  //
            
            belep.ShowDialog(); //belépés form megjelenítése
            if (belep.DialogResult == DialogResult.OK)
            {
                Random r = new Random();                //a 3 eljárás közül egy random-ot ellenőrzünk le 
                string[] adatoktomb = new string[4];
                string listakeszit;

                adatoktomb = belep.minden.Split(';');   //adatoktömb fel lesz töltve a belép minden stringnek Splittelt értékeivel

                user = adatoktomb[0];
                jelszo = adatoktomb[r.Next(1, 4)];  //a 3 féle kódolás közül CSAK AZ EGYIKET használjuk
                adatoktomb = null;             //biztonsági szempontbol nullázzuk a tömb értékért, így csak változóban marad adat
                toolStripStatusLabel1.Text = "Bejelentkezett felhasználó: " + user; //"alsó menü" elemek textjének kiíratása   
                toolStripStatusLabel2.Text = "    -    ";
                toolStripStatusLabel3.Text = "Aktív ablak: Nyitóablak ";


                if (user == "")
                {
                    menuTilt();
                }
                else
                {
                    try
                    {
                        DataTable dtNyitotabla = new DataTable();
                        //ökölszabály: string esetén SHIFT+1 SHITF+2 SHIFT+3 változónév/vezérlőelem.tulajdonsag SHIFT+3 SHIFT+2 SHIFT+1
                        //ökölszabály: nem string esetén SHIFT+1 SHIFT+3 változónév/vezérlőelem.tulajdonsag SHIFT+3 SHIFT+1
                        listakeszit = "SELECT * FROM felhasznalok WHERE fnev='" + user + "'";
                        dtNyitotabla = ellenorzokapcsolat.Tablaz(listakeszit);

                        if (dtNyitotabla.Rows.Count > 0)    //leelnőrzöm, hogy van-e sora a készített táblának
                        {
                            dtgwJelszoTabla.DataSource = dtNyitotabla;
                            this.dtgwJelszoTabla.Rows[0].Selected = true;   //kiválasztom az első sort, 0. indexüt
                            a = dtgwJelszoTabla.Rows[0].Cells[0].Value.ToString();//id kiválasztott sor értékeinek mentése 
                            b = dtgwJelszoTabla.Rows[0].Cells[1].Value.ToString();//név                             abcdef
                            c = dtgwJelszoTabla.Rows[0].Cells[2].Value.ToString();//jelszavak 3 féle titkosítással sha256
                            d = dtgwJelszoTabla.Rows[0].Cells[3].Value.ToString();//sha512
                            f = dtgwJelszoTabla.Rows[0].Cells[4].Value.ToString();//md5

                            if (jelszo == c || jelszo == d || jelszo == f)  //jsz ellenőrzése, 3 titkosítás közül 
                            {
                                MessageBox.Show("Üdvözlöm kedves " + user + " jó munkát és szép napot kívánok!");
                            }
                            else
                            {
                                MessageBox.Show("Sajnálom, de hibás adatokat írt be!!!");
                                menuTilt(); 
                            }
                        }
                        else
                            {
                            MessageBox.Show("Sajnálom, de Ön még nem regisztrált felhasználó!!!");
                            menuTilt();
                            }

                        }
                        catch (Exception)
                            {
                            MessageBox.Show("Váratlan rendszerhiba történt");
                            menuTilt();
                            }
                }
            }
            if (belep.DialogResult == DialogResult.Cancel)
            //else
            {
                menuTilt();
            }
        }
        private void menuTilt() //első 3 menüpont letiltása helytelen belépési adatok esetén
        {
            this.fájlToolStripMenuItem.Enabled = false;
            this.tsmiAdatkezel.Enabled = false;
            this.tsmiAdatbazisok.Enabled = false;
        }

    }
}
