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
    public partial class Form0 : Form
    {
        //a legelső form, ahonnan irányítom a másik két form megnyitását, bezárását
        public Form0()
        {
            InitializeComponent();
        }

        private void Form0_Load(object sender, EventArgs e)
        {
            //Form1 betöltése, fájl beolvasása vezérlőelembe majd Form1 bezárása
            Form1 form1 = new Form1();
            form1.ShowDialog();

            //Form2 betöltése a táblákkal
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
