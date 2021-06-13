using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EgyesReszek
    {
    public partial class Beolvasas : Form
        {
        //osztályszíntü változók deklarálása
        List<napsutes> napsugar = new List<napsutes>();
        public static string egysor;
        public static string[] soreszek;
        public Beolvasas()
            {
            InitializeComponent();
            }

        private void button3_Click(object sender, EventArgs e)//bezárás gomb
            {
            this.Close();
            }

        private void button1_Click(object sender, EventArgs e)//beolvasás listába gomb
            {

            try
                {
                FileStream fs = new FileStream("BP_napfenytartam.txt", FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                egysor = sr.ReadLine();
                soreszek = egysor.Split(';');
                //első sor beállítása

                listView1.View = View.Details;
                for (int i = 0; i < soreszek.Length; i++)
                    {
                    listView1.Columns.Add(soreszek[i]);
                    }
                // Auto-size the columns
                for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                    listView1.Columns[i].Width = -2;
                    listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
                    }
                while (!sr.EndOfStream)
                    {
                    egysor = sr.ReadLine();
                    soreszek = egysor.Split(';');
                    ListViewItem item = new ListViewItem(soreszek);
                    listView1.Items.Add(item);
                    }
                // Resize the columns
                for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                    listView1.Columns[i].Width = -2;
                    listView1.Columns[i].TextAlign = HorizontalAlignment.Center;
                    }
                sr.Close();
                fs.Close();
                }

            catch (Exception)
                {
                Console.WriteLine("hiba történt a fájl beolvasása közben");

                }
            // Resize the columns
            for (int x = 0; x <listView1.Columns.Count; x++)
                {
                listView1.Columns[x].Width = -2;
                }
            }

        private void button2_Click(object sender, EventArgs e)//beolvasás dataGridView-ba gomb
            {
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
                }
            catch (Exception)
                {
                Console.WriteLine("hiba történt a fájl beolvasása közben");

                }
            }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)//listview eseménye
            {
            string kivalasztott = "";
            if (listView1.SelectedItems.Count > 0)
                {
                
                 kivalasztott = String.Concat(listView1.SelectedItems[0].SubItems[0].Text, " ; ", listView1.SelectedItems[0].SubItems[1].Text," ; ", listView1.SelectedItems[0].SubItems[2].Text," ; ", listView1.SelectedItems[0].SubItems[3].Text," ; ",listView1.SelectedItems[0].SubItems[4].Text, " ; ", listView1.SelectedItems[0].SubItems[5].Text);
                 MessageBox.Show("A kiválasztott adatsor: " + kivalasztott, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
            else
                    {
                    kivalasztott = null;
                    }
               
                }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//dtgw eseménye
            {
            if (e.RowIndex >= 0)
                {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

               string adatok = string.Concat(row.Cells[0].Value.ToString(), " ; ", row.Cells[1].Value.ToString(), " ; ", row.Cells[2].Value.ToString(), " ; ", row.Cells[3].Value.ToString(), " ; ", row.Cells[4].Value.ToString(), " ; ", row.Cells[5].Value.ToString());
                MessageBox.Show("A kiválasztott adatsor: " + adatok, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        
}
