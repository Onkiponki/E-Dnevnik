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
    public partial class Sifarnik : Form
    {
        SqlConnection veza;
        SqlDataAdapter adapter;
        
        string kogaZovem;
        public Sifarnik(string tabela)
        {
            kogaZovem = tabela;
            InitializeComponent();
        }

        private void Sifarnik_Load(object sender, EventArgs e)
        {
            string naredba = $"select * from {kogaZovem}";
            adapter = new SqlDataAdapter(naredba,Konekcija.zakonektuj());
            DataTable podaci = new DataTable();
            adapter.Fill(podaci);
            dataGridView1.DataSource = podaci;

        }

        private void okay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
