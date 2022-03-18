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
    public partial class Form1 : Form
    {
        int gde = 0;
        int flag;
        string naredba = "select * from Osoba";
        DataTable podaci;
        SqlDataAdapter adapter;
        void rennaisance()
        {
            if (podaci.Rows.Count == 0)
            {
                foreach (Control c in Controls)
                {
                    Button b = c as Button;
                    if (b != null)
                    {
                        b.Enabled = false;
                    }
                }
                Insert.Enabled = true;
                foreach (Control c in Controls)
                {
                    TextBox b = c as TextBox;
                    if (b != null)
                    {
                        b.Text = "";
                    }
                }
            }
            else
            {
                id.Text = podaci.Rows[gde]["id"].ToString();
                ime.Text = podaci.Rows[gde]["ime"].ToString();
                prezime.Text = podaci.Rows[gde]["prezime"].ToString();
                adresa.Text = podaci.Rows[gde]["adresa"].ToString();
                jmbg.Text = podaci.Rows[gde]["jmbg"].ToString();
                email.Text = podaci.Rows[gde]["email"].ToString();
                password.Text = podaci.Rows[gde]["pass"].ToString();
                uloga.Text = podaci.Rows[gde]["uloga"].ToString();


                levlje.Enabled = (gde != 0);
                levo.Enabled = (gde != 0);
                desnje.Enabled = (gde != podaci.Rows.Count-1);
                desno.Enabled = (gde != podaci.Rows.Count - 1);
            }
        }
        SqlConnection veza = Konekcija.zakonektuj();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter(naredba,Konekcija.zakonektuj());
            podaci = new DataTable();
            adapter.Fill(podaci);
            rennaisance();
        }

        private void desno_Click(object sender, EventArgs e)
        {
            gde++;
            rennaisance();
        }

        private void levo_Click(object sender, EventArgs e)
        {
            gde--;
            rennaisance();
        }

        private void desnje_Click(object sender, EventArgs e)
        {
            gde = podaci.Rows.Count - 1;
            rennaisance();
        }

        private void levlje_Click(object sender, EventArgs e)
        {
            gde = 0;
            rennaisance();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            flag = 0;
            foreach (Control c in Controls)
            {
                TextBox b = c as TextBox;
                if (b != null)
                {
                    if(b.Text == "")
                    {
                        flag++;
                    }
                }
            }
            if (flag > 0)
            {
                MessageBox.Show("Unesite sve podatke o osobi");
            }
            else
            {
                string unesi = $"insert into Osoba values('{ime.Text}', '{prezime.Text}', '{adresa.Text}', '{jmbg.Text}', '{email.Text}', '{password.Text}', {uloga.Text})";
                SqlCommand komanda = new SqlCommand(unesi,veza);
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                podaci.Clear();
                adapter = new SqlDataAdapter(naredba, Konekcija.zakonektuj());
                adapter.Fill(podaci);
                gde = podaci.Rows.Count - 1;
                rennaisance();
            }
            
        }

        private void update_Click(object sender, EventArgs e)
        {
            string izmeni = $"update Osoba set ime = '{ime.Text}', prezime = '{prezime.Text}', adresa = '{adresa.Text}', jmbg = '{jmbg.Text}', email = '{email.Text}', pass = '{password.Text}', uloga = {uloga.Text} where id={id.Text}";
            SqlCommand komanda = new SqlCommand(izmeni, veza);
            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter(naredba, Konekcija.zakonektuj());
            adapter.Fill(podaci);
            rennaisance();
        }

        private void obrisi_Click(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                TextBox b = c as TextBox;
                if (b != null)
                {
                    b.Text = "";
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string obrisi = $"delete from Osoba where id={id.Text}";
            SqlCommand komanda = new SqlCommand(obrisi, veza);
            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter(naredba,veza);
            adapter.Fill(podaci);
            gde = 0;
            rennaisance();

        }
    }
}
