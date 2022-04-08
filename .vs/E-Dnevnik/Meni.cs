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
    public partial class Meni : Form
    {
        public Meni()
        {
            InitializeComponent();
        }


        private void osobaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Spisak forma = new Spisak();
            forma.Show();
        }

        private void Meni_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
