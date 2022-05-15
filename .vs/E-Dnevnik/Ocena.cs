using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_Dnevnik
{
    public partial class Ocena : Form
    {
        SqlConnection veza = Konekcija.zakonektuj();
        string naredba;
        public Ocena()
        {
            InitializeComponent();
        }

        private void Ocena_Load(object sender, EventArgs e)
        {
            FeelingTheCombosY();
        }
        private void FeelingTheCombosY()
        {
            naredba = "select * from Skolska_godina";
            SqlDataAdapter adapter = new SqlDataAdapter(naredba, veza);
            DataTable godina = new DataTable();
            adapter.Fill(godina);
            cmbGodina.DataSource = godina;
            cmbGodina.ValueMember = "id";
            cmbGodina.DisplayMember = "naziv";
            cmbGodina.SelectedValue = 2;
        }
    }
}
