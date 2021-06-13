using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuEsSql
{
    public partial class belepes : Form
    {
        //objetktum megjelenítéséhez szolgáló változók
        private Kapcsolodas belepkapcsolat;  //a regnél új adat beszúrásához szolgál erről a forlmról   
        public string adatok;
        public int regisztral = 1;
        public string ujFelhasznalo;
        // Csak olvasható tulajdonság a jelszóbekérő szövegmező tartalmának lekérdezésére. 

        /// <summary>
        /// Referencia szerinti átadás
        /// ref kulcsszó használata
        /// 1. definició helyén
        /// public string valamitCsinal(ref string elso, ref string masodik)
        /// { string valtozonev="";
        /// elso+="fa";
        /// masodik+="pálinka";
        /// valtozonev= elso+" "+masodik;
        /// return valtozonev
        /// }
        /// 
        /// 2. hivás helyén
        /// string egy="alma";
        /// string ketto = "körte";
        /// string osszefuzott= valamitCsinal(ref egy,ref ketto);
        /// kiirásoknál egy="almafa" és ketto="körtepálinka"
        /// </summary>
        public string minden
        {
            get
            {
                return adatok;
            }
        }
        public belepes()
        {
            InitializeComponent();
            belepkapcsolat = new Kapcsolodas(); //kapcsolódás osztályból jön létre, connectionStringet tartalmazza
        }

        public void adatokbeirasa()
        {
            try
            {
                ujFelhasznalo = "INSERT INTO felhasznalok (fnev,jelszosha2,jelszosha5, jelszomd5,email)  VALUES('" + tbfnev.Text + "','" + titkositas.SHA2Hash(tbjelszo.Text) + "','" + titkositas.SHA5Hash(tbjelszo.Text) + "', '" + titkositas.MD5Hash(tbjelszo.Text) + "','" + tbemail.Text + "')";
                belepkapcsolat.Ujadat(ujFelhasznalo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Validálási metódusok aktiválása
        static Regex Valid_Name = CsakBetu();
        static Regex Valid_Contact = CsakSzam();
        static Regex Valid_Password = JelszoEllenoriz();
        static Regex Valid_Email = EmailEllenoriz();

        //Email address ellenőrzés
        public static Regex EmailEllenoriz()
        {
            string Email_Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(Email_Pattern, RegexOptions.IgnoreCase); 
        }

        //Csak karakterek típusu mező validálása
        public static Regex CsakBetu()
        {
            string StringNoNumber_Pattern = "^[a-zA-Z]*$";

            return new Regex(StringNoNumber_Pattern, RegexOptions.IgnoreCase);
        }

        //Csak számok típusu mező validálása  -ebben a projektben nincs!!!!!
        public static Regex CsakSzam()
        {
            string NoStringJustNumber_Pattern = "^[0-9]*$";

            return new Regex(NoStringJustNumber_Pattern, RegexOptions.IgnoreCase);
        }

        //Jelszó validálás -kisbetű-nagybetű-szám-kérdőjel-felkiáltójel-pont, legalább nyolc, max tizenöt karakter
        private static Regex JelszoEllenoriz()
        {
            //string Password_Pattern = "(?!.^[0-9]*$)(?!^.[a-zA-Z]*$)^([a-zA-Z0-9]{8,16})$";
            //string Password_Pattern = "^(?=.*\d)(?=.*[a - z])(?=.*[A - Z])(?!.* )(?=.*[^a - zA - Z0 - 9]).{ 8,16}$";
            //egy kisbetű, egy nagybetű, egy szám és egy speciális karacter[#?!@$%^&*-]8-16 a hossza.

            string Password_Pattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$";
            return new Regex(Password_Pattern, RegexOptions.IgnoreCase);
        }

        private void button1_Click(object sender, EventArgs e)  //belépés gomb
        {
            /*if (String.IsNullOrEmpty(tbfnev.Text) || String.IsNullOrEmpty(tbjelszo.Text) || String.IsNullOrWhiteSpace(tbfnev.Text) || String.IsNullOrWhiteSpace(tbjelszo.Text))
            vagy
                if (tbfnev.Text == "" || tbjelszo.Text == "")
            */
            //egyszerűbb verziója következő sorban:
            if (tbfnev.Text != "" && tbjelszo.Text != "")
            {
                adatok = tbfnev.Text + ";" + titkositas.SHA2Hash(tbjelszo.Text) + ";" + titkositas.SHA5Hash(tbjelszo.Text) + ";" + titkositas.MD5Hash(tbjelszo.Text);

            }
            else
            {
                MessageBox.Show("Sajnálom, de minden mezőt ki kell tölteni!!!!!");
                adatok = " ; ; ; ";
            }
        }
        private void belepes_Load(object sender, EventArgs e)
        {
            timer1.Interval = 5000; //30 000 az a harminc másodperc
            timer1.Enabled = true;

            label1.Visible = false; //felhaszmálónév label
            tbfnev.Visible = false; //felhasználónév Textbox

            label2.Visible = false; //jelszó label alatta Textbox
            tbjelszo.Visible = false;

            lbIsmetjelszo.Visible = false;	//ismételt jelszó label és Textbox
            tbIsmetjelszo.Visible = false;

            label3.Visible = false;	//email mező
            tbemail.Visible = false;

            button1.Visible = false;	//belépés gomb
            button3.Visible = false;    //regisztráció gomb
                                        //tbfnev.Focus();
        }

        private void button2_Click(object sender, EventArgs e) //mégse gomb
        {
            timer1.Enabled = false; //dialogResult cancel értékét adja, azaz bezáródik a belepes form
        }

        private void timer1_Tick(object sender, EventArgs e)    //timer beállítása, mikor a 30mp reg gombra kattint
        {
            regisztral = 0;
            timer1.Enabled = false;
            button4.Visible = false;

            label1.Visible = true;
            tbfnev.Visible = true;

            label2.Visible = true;
            tbjelszo.Visible = true;

            button3.Visible = false;

            button1.Visible = true;
            //tbfnev.Focus();
        }

        private void button4_Click(object sender, EventArgs e) //30mp kattintson ide, ha regelni szeretne gomb
        {
            timer1.Enabled = false;
            button4.Visible = false;
            label1.Visible = true;
            tbfnev.Visible = true;

            label2.Visible = true;
            tbjelszo.Visible = true;

            lbIsmetjelszo.Visible = true;
            tbIsmetjelszo.Visible = true;

            label3.Visible = true;
            tbemail.Visible = true;

            button1.Visible = false;
            button3.Visible = true;
        }

        private void tbIsmetjelszo_Leave(object sender, EventArgs e)
        {
            if (tbjelszo.Text != tbIsmetjelszo.Text)
            {
                MessageBox.Show("A jelszavak nem azonosak");
                tbjelszo.Text = "";
                tbIsmetjelszo.Text = "";
                tbjelszo.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)  //Regisztráció gomb
        {
            DialogResult MelyikGombraKattintott = MessageBox.Show("Valóban regisztrálni szeretne?", "Regisztráció?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (tbfnev.Text != "" && tbjelszo.Text != "" && tbemail.Text != "")
            {
                if (MelyikGombraKattintott == DialogResult.Yes)
                {
                    adatok = tbfnev.Text + ";" + titkositas.SHA2Hash(tbjelszo.Text) + ";" + titkositas.SHA5Hash(tbjelszo.Text) + ";" + titkositas.MD5Hash(tbjelszo.Text);
                    adatokbeirasa();
                }
                else
                {
                    adatok = " ; ; ; ;";
                }
            }
        }

        private void tbjelszo_Leave(object sender, EventArgs e) //jelszó ellenőrzés, mező elhagyása után
        {
            if (regisztral == 1)
            {
                if (Valid_Password.IsMatch(tbjelszo.Text) != true)
                {
                    MessageBox.Show("A jelszó legalább 8, legfeljebb 16 karakter hosszú lehet. Legyen benne kisbetű, nagybetű, szám és különleges karakter!", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tbjelszo.Focus();
                    return;
                }
            }
        }

        private void tbemail_Leave(object sender, EventArgs e)  //email ellenőrzés, mező elhagyása után
        {
            if (regisztral == 1)
            {
                if (Valid_Email.IsMatch(tbemail.Text) != true)

                    MessageBox.Show("Nem valós e-mail cím!", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbemail.Focus();
                return;
            }
        }
        private void tbfnev_Leave(object sender, EventArgs e) //felhasználónév ellenőrzés, mező elhagyása után
        {
            if (regisztral == 1)
            {
                if (Valid_Name.IsMatch(tbfnev.Text) != true)
                {
                    MessageBox.Show("A felhasználónév csak betüket tartalmazhat!!!", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tbfnev.Focus();
                    return;
                }
            }
        }
        private void tbjelszo_Enter(object sender, EventArgs e) //fnev ellenőrzés létezik-e már
        {
            if (regisztral == 1)
            {
                if (String.IsNullOrEmpty(tbfnev.Text))
                {
                    DataTable dtNev = new DataTable();
                    string nevmegnez = "SELECT * FROM felhasznalok WHERE fnev ='" + tbfnev.Text + "'";
                    dtNev = belepkapcsolat.Tablaz(nevmegnez);
                    if (dtNev.Rows.Count > 0)
                    {
                        MessageBox.Show("!!! Válasszon más azonosítót!!!");
                        tbfnev.Focus();
                        return;
                    }
                    tbfnev.Focus();
                    return;
                }
            }
        }
    }
}
