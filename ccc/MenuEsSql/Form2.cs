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
    public partial class Form2 : Form
    {
        Point balfenn = new Point();
        string[] kapottertek;
        int szelesseg, magassag;
        public Form2(string ertekek) //konstruktor ertekek= "1;440;230" 
        {
            
            InitializeComponent();
            kapottertek = ertekek.Split(';');
            MessageBox.Show(kapottertek.Length.ToString());
            label1.Text = kapottertek[0];
            label2.Text = kapottertek[1];
            label3.Text = kapottertek[2];
            szelesseg = int.Parse(label2.Text);
            magassag = int.Parse(label3.Text);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (label1.Text == "1")
            {
                balfenn = new Point(0, 0);
                this.Text = "kisAblak";
                Location = balfenn;
                Size = new Size(szelesseg / 8 * 5, magassag / 5 * 4);
                BackColor = Color.Beige;
                this.AutoScroll = true;
            }

            if (label1.Text == "2")
            {
                balfenn = new Point((szelesseg / 8), 0);
                this.Text = "nagyAblak";
                Location = balfenn;
                Size = new Size(szelesseg / 16 * 13, magassag / 16 * 15);
                BackColor = Color.Yellow;
                this.AutoScroll = true;
            }
        listatolt();
        }
        private void listatolt()
        {
            lstSzemelyek.Items.Clear();
            var egyszemely = GetSzemelyLista();
            foreach (var adat in egyszemely)
            {
                var row = new string[] { adat.vnev, adat.knev, adat.szuldat };
                var lvi = new System.Windows.Forms.ListViewItem(row);
                lvi.Tag = adat;
                lstSzemelyek.Items.Add(lvi);
            }
            lstSzemelyek.GridLines = true;
            lstSzemelyek.View = View.Details;
        }

        private List<szemelyek> GetSzemelyLista()
        {
            string[] egysor = new string[3];
            var listasor = new List<szemelyek>();
            //file megnyitás, olvasás
            FileStream fs = new FileStream("listaalapok.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            egysor = sr.ReadLine().Split(';');
            while (!sr.EndOfStream)
            {
                egysor = sr.ReadLine().Split(';');
                listasor.Add(new szemelyek() { vnev = egysor[0].ToString(), knev = egysor[1].ToString(), szuldat = egysor[2].ToString() });
            }
            //file lezárása
            sr.Close();
            fs.Close();
            return listasor;
        }

        private void Form2_Move(object sender, EventArgs e)
        {
            Location = balfenn;
        }

        private void btnbezar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
