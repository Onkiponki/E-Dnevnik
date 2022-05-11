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
    public partial class Raspodela : Form
    {
        SqlConnection veza = Konekcija.zakonektuj();
        DataTable raspodela;
        int br = 0;
        public Raspodela()
        {
            InitializeComponent();
        }
        private void Zaucitaj()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from raspodela", veza);
            raspodela = new DataTable();
            adapter.Fill(raspodela);
        }
        private void FeelingTheCombos()
        {
            SqlDataAdapter adapter;
            DataTable godina, nastavnik, predmet, odeljenje;
            
            godina = new DataTable();
            nastavnik = new DataTable();
            predmet = new DataTable();
            odeljenje = new DataTable();

            adapter = new SqlDataAdapter("select * from Skolska_godina", veza);
            adapter.Fill(godina);

            adapter = new SqlDataAdapter("select id, ime + prezime as naziv from Osoba where uloga = 2", veza);
            adapter.Fill(nastavnik);

            adapter = new SqlDataAdapter("select id,str(razred) + '-' + indeks as naziv from Odeljenje", veza);
            adapter.Fill(odeljenje);

            adapter = new SqlDataAdapter("select id, naziv from Predmet", veza);
            adapter.Fill(predmet);

            labelID.Text = raspodela.Rows[br]["id"].ToString();

            cmbGodina.DataSource = godina;
            cmbGodina.ValueMember = "id";
            cmbGodina.DisplayMember = "naziv";
            

            cmbNastavnik.DataSource = nastavnik;
            cmbNastavnik.ValueMember = "id";
            cmbNastavnik.DisplayMember = "naziv";

            cmbPredmet.DataSource = predmet;
            cmbPredmet.ValueMember = "id";
            cmbPredmet.DisplayMember = "naziv";

            cmbOdeljenje.DataSource = odeljenje;
            cmbOdeljenje.ValueMember = "id";
            cmbOdeljenje.DisplayMember = "naziv";

            if (raspodela.Rows.Count == 0)
            {
                cmbGodina.SelectedValue = -1;
                cmbNastavnik.SelectedValue = -1;
                cmbPredmet.SelectedValue = -1;
                cmbOdeljenje.SelectedValue = -1;

                foreach (Control c in Controls)
                {
                    Button b = c as Button;
                    if (b != null)
                    {
                        b.Enabled = false;
                    }
                }
                Insert.Enabled = true;
            }
            else
            {

                cmbGodina.SelectedValue = raspodela.Rows[br]["godina_id"];
                cmbNastavnik.SelectedValue = raspodela.Rows[br]["nastavnik_id"];
                cmbPredmet.SelectedValue = raspodela.Rows[br]["predmet_id"];
                cmbOdeljenje.SelectedValue = raspodela.Rows[br]["odeljenje_id"];

                levlje.Enabled = (br != 0);
                levo.Enabled = (br != 0);
                desnje.Enabled = (br != raspodela.Rows.Count - 1);
                desno.Enabled = (br != raspodela.Rows.Count - 1);
            }

           


        }
        private void Raspodela_Load(object sender, EventArgs e)
        {
            Zaucitaj();
            FeelingTheCombos();
        }

        private void levlje_Click(object sender, EventArgs e)
        {
            br = 0;
            FeelingTheCombos();
        }

        private void levo_Click(object sender, EventArgs e)
        {
            br--;
            FeelingTheCombos();
        }

        private void desnje_Click(object sender, EventArgs e)
        {
            br = raspodela.Rows.Count - 1;
            FeelingTheCombos();
        }

        private void desno_Click(object sender, EventArgs e)
        {
            br++;
            FeelingTheCombos();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string obrisi = $"delete from Raspodela where id={labelID.Text}";
            SqlCommand komanda = new SqlCommand(obrisi, veza);
            Boolean brisano = false;
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                brisano = true;
            }
            catch(Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            if (brisano)
            {
                Zaucitaj();
                if (br > 0) br--;
                FeelingTheCombos();
            }
            
        }

        private void Insert_Click(object sender, EventArgs e)
        {
                //insert into raspodela (godina_id, nastavnik_id, predmet_id, odeljenje_id)
                //values(7,2,2,1)
                string unesi = $"insert into raspodela (godina_id, nastavnik_id, predmet_id, odeljenje_id) values('{cmbGodina.SelectedValue}', '{cmbNastavnik.SelectedValue}', '{cmbPredmet.SelectedValue}', '{cmbPredmet.SelectedValue}')";
                SqlCommand komanda = new SqlCommand(unesi, veza);
                try
                {
                    veza.Open();
                    komanda.ExecuteNonQuery();
                    veza.Close();
                }
                catch(Exception Greska)
                {
                    MessageBox.Show(Greska.Message);
                }
                Zaucitaj();
                FeelingTheCombos();
            }

        private void update_Click(object sender, EventArgs e)
        {
            string izmeni = $"update Raspodela set godina_id = {cmbGodina.SelectedValue}, nastavnik_id = {cmbNastavnik.SelectedValue}, predmet_id = {cmbPredmet.SelectedValue}, odeljenje_id={cmbOdeljenje.SelectedValue}";
            SqlCommand komanda = new SqlCommand(izmeni, veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            Zaucitaj();
            FeelingTheCombos();
        }
    }
}
