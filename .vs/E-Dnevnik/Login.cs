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
    public partial class Login : Form
    {
        SqlDataAdapter adapter;
        DataTable podaci;
        SqlConnection veza = Konekcija.zakonektuj();
        string naredba = "select ime, pass from Osoba";
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void log_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(naredba, veza);
            podaci = new DataTable();
            adapter.Fill(podaci);
            if(ime.Text=="" || pass.Text=="")
            {
                MessageBox.Show("Unesite Vas ID i password");
            }
            else
            {
                SqlDataAdapter noviAdapter = new SqlDataAdapter($"select pass from Osoba where id = {ime.Text}", veza);
                DataTable podatak = new DataTable();
                noviAdapter.Fill(podatak);
                if(pass.Text == podatak.Rows[0]["pass"].ToString())
                {
                    Meni forma = new Meni();
                    this.Hide();
                    forma.Show();
                }
            }
        }
    }
}
