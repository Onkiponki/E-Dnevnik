using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Dnevnik
{
    public partial class Glavna2 : Form
    {
        public Glavna2()
        {
            InitializeComponent();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raspodela form1 = new Raspodela();
            form1.Show();
        }

        private void oceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ocena form1 = new Ocena();
            form1.Show();
        }

        private void osobeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spisak form1 = new Spisak();
            form1.Show();
        }

        private void smeroviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik form1 = new Sifarnik("Smer");
            form1.Show();
        }

        private void skGodineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik form1 = new Sifarnik("Skolska_godina");
            form1.Show();
        }

        private void predmetiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik form1 = new Sifarnik("Predmet");
            form1.Show();
        }

        private void upisniceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Upisnica form1 = new Upisnica();
            form1.Show();
        }
    }
}
