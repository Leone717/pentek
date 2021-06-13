using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BcsopAdoszam
{
    public partial class Form1 : Form
    {
        public static DateTime kezdet = new DateTime(1867, 1, 1, 0, 0, 0);
        public static Random generalt = new Random();
        public static int[] adoszam = new int[10];
        //public static int[] szemszam = new int[11];
        public static int sorszam, ev, honap, nap;
        int neme;
        int szorzat = 0;
        DateTime szulDat;
        //Kapcsolodás kapcsolat
        private Kapcsolodas ujkapcsolat;
        //1. 2. 3. 

        public int uresmezoellenoriz()  //üres név mezejének ellenőrzése
        {
            int ures = 0;
            if (tbNev.Text == "")
                ures += 1;//+1
            return ures;
        }

        public void torol() //textBox, label mezők kitisztítása, bevitelesemény után
        {
            tbNev.Clear(); //textBox1.Text="";
            lbAdoSzam.Text = "";
            lbSzulDat.Text = "";
            lbAdoszam0.Text = "";
        }

        public void adofeltolt()    //adóaz feltöltése
        {

            string sorszoveg = "00" + sorszam.ToString();
            sorszoveg = sorszoveg.Substring(sorszoveg.Length - 3);
            adoszam[0] = 8;//1.
            adoszam[1] = Convert.ToInt32(lbAdoszam0.Text.Substring(0, 1));//1 -2.
            adoszam[2] = Convert.ToInt32(lbAdoszam0.Text.Substring(1, 1));//2 -3.
            adoszam[3] = Convert.ToInt32(lbAdoszam0.Text.Substring(2, 1));//3 -4.
            adoszam[4] = Convert.ToInt32(lbAdoszam0.Text.Substring(3, 1));//4 -5.
            adoszam[5] = Convert.ToInt32(lbAdoszam0.Text.Substring(4, 1));//5 -6.
            adoszam[6] = Convert.ToInt32(sorszoveg.Substring(0, 1));//1 -7.
            adoszam[7] = Convert.ToInt32(sorszoveg.Substring(1, 1)); //2 -8.
            adoszam[8] = Convert.ToInt32(sorszoveg.Substring(2, 1));//3 -9.

            for (int i = 0; i < adoszam.Length; i++)
                szorzat += adoszam[i] * (i + 1);
            adoszam[9] = szorzat % 11;
        }

        /*public void szemszamfeltolt() személyiaz feltöltése
        {

            string sorszoveg = "00" + sorszam.ToString();
            sorszoveg = sorszoveg.Substring(sorszoveg.Length - 3);
            if (ev <= 1999)
                ev -= 1900;
            else
                ev -= 2000;
            string sorev = "00" + ev.ToString();
            sorev = sorev.Substring(sorev.Length - 2);
            string sorhonap = "00" + honap.ToString();
            sorhonap = sorhonap.Substring(sorhonap.Length - 2);
            string sornap = "00" + nap.ToString();
            sornap = sornap.Substring(sornap.Length - 2);
            if (ev <= 1999)
            {
                if (neme == 1)
                    szemszam[0] = 1;
                else
                    szemszam[0] = 2;
            }
            if (ev >= 2000)
            {
                if (neme == 1)
                    szemszam[0] = 3;
                else
                    szemszam[0] = 4;
            }
            szemszam[1] = Convert.ToInt32(sorev.Substring(0, 1));//1
            szemszam[2] = Convert.ToInt32(sorev.Substring(1, 1));//1
            szemszam[3] = Convert.ToInt32(sorhonap.Substring(0, 1));//2
            szemszam[4] = Convert.ToInt32(sorhonap.Substring(1, 1));//3
            szemszam[5] = Convert.ToInt32(sornap.Substring(0, 1));//4
            szemszam[6] = Convert.ToInt32(sornap.Substring(1, 1));//5
            szemszam[7] = Convert.ToInt32(sorszoveg.Substring(0, 1));//1
            szemszam[8] = Convert.ToInt32(sorszoveg.Substring(1, 1)); //2
            szemszam[9] = Convert.ToInt32(sorszoveg.Substring(2, 1));//3


            for (int i = 0; i < szemszam.Length; i++)
                szorzat += szemszam[i] * (i + 1);
            szemszam[10] = szorzat % 11;

        }*/
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) //személyi az. gomb
        {
            /*neme = 1;
            DialogResult result = MessageBox.Show("Férfi?", "Kérem adja meg a nemét!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
                neme = 2;
            szemszamfeltolt();
            if (szemszam[10] == 10)
            {
                if (sorszam == 999)
                {
                    sorszam = 1;
                }
                else
                {
                    sorszam += 1;
                }
                szemszamfeltolt();
            }
            label3.Text = "A személyi azonosítószám: ";

            for (int i = 0; i < szemszam.Length; i++)
            {
                label3.Text += szemszam[i].ToString();
            }
            label3.Visible = true;
            button3.Visible = false;*/
        }

        private void button1_Click(object sender, EventArgs e)//kilépés gomb
        {
            Application.Exit();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            sorszam = generalt.Next(0, 998) + 1;
            szulDat = monthCalendar1.SelectionStart;
            ev = monthCalendar1.SelectionStart.Year;
            honap = monthCalendar1.SelectionStart.Month;
            nap = monthCalendar1.SelectionStart.Day;
            lbAdoszam0.Visible = false;
            lbAdoszam0.Text = (szulDat.Subtract(kezdet).Days).ToString();
            sorszam = generalt.Next(0, 999) + 1;
            button2.Visible = true;
            button3.Visible = true;

            lbSzulDat.Text = szulDat.ToString();
            lbSzulDat.Visible = true;
            //MessageBox.Show(sorszam.ToString());
        }

        private void btnHozzaad_Click(object sender, EventArgs e)//adatok hozzáadása
        {
            int nemIrtAMezobeSemmit = uresmezoellenoriz();//üres mező(k) most csak tbNev ell.
            if (nemIrtAMezobeSemmit > 0)
            {
                MessageBox.Show("Adja meg a nevét");//figyelmeztető üzenet az üres mezőre
                //torol();
            }
            else
            {
                //Ököl (ökör) szabály
                // INSERT INTO tablanev VALUES ('"+textBox2.Text+"',)
                //Ha az érték nem string, akkor tablanev VALUES ('+textBox2.Text+',)
                // ha szám, de nem int akkor legyen string!!!!!!
                string ujPartner = "INSERT INTO adoszam VALUES('" + lbAdoSzam.Text + "','" + tbNev.Text + "','" + lbSzulDat.Text + "')";
                //bevitt adatok ellenőrzésére:
                //MessageBox.Show(lbAdoSzam.Text);
                //MessageBox.Show(tbNev.Text);
                //MessageBox.Show(szulDat.ToShortDateString().Substring(0,13));
                ujkapcsolat.Ujadat(ujPartner);
                torol();//mezők kiűrítése
                DialogResult valasz = MessageBox.Show("Kíván-e újabb személy adatait rögzíteni?", "AAAAA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (valasz == DialogResult.No)//ha a válasz no, elköszön és bezárja a programot
                {
                    MessageBox.Show("Köszönöm, hogy használta a programot! További szép napot!");
                    Application.Exit();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbAdoszam0.Visible = false;
            label3.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            lbSzulDat.Visible = false;//Szüldat label
            lbAdoSzam.Visible = false;//adoszam label
            tbNev.Visible = false;//nev label
            btnHozzaad.Visible = false;//hozzaádas g. megj.
            ujkapcsolat = new Kapcsolodas();//!!!!!!!!!!KAPCSOLAT
        }

        private void button2_Click_1(object sender, EventArgs e)//adószám
        {
            adofeltolt();

            if (adoszam[9] == 10)
            {
                if (sorszam == 999)
                {
                    sorszam = 1;
                }
                else
                {
                    sorszam += 1;
                }
                adofeltolt();
            }
            lbAdoszam0.Text = "Adószám: ";

            for (int i = 0; i < adoszam.Length; i++)
            {
                lbAdoszam0.Text += adoszam[i].ToString();
                lbAdoSzam.Text += adoszam[i].ToString();   //label8 textje csak az adószám lesz
            }
            lbAdoszam0.Visible = true;
            button2.Visible = false;

            lbAdoSzam.Visible = true;
            btnHozzaad.Visible = true;
            tbNev.Visible = true;
        }

    }
}
