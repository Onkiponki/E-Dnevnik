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

            cmbGodina.DataSource = godina;
            cmbGodina.ValueMember = "id";
            cmbGodina.DisplayMember = "naziv";
            cmbGodina.SelectedValue = raspodela.Rows[br]["godina_id"];

            cmbNastavnik.DataSource = nastavnik;
            cmbNastavnik.ValueMember = "id";
            cmbNastavnik.DisplayMember = "naziv";
            cmbNastavnik.SelectedValue = raspodela.Rows[br]["nastavnik_id"];

            cmbPredmet.DataSource = predmet;
            cmbPredmet.ValueMember = "id";
            cmbPredmet.DisplayMember = "naziv";
            cmbPredmet.SelectedValue = raspodela.Rows[br]["predmet_id"];

            cmbOdeljenje.DataSource = odeljenje;
            cmbOdeljenje.ValueMember = "id";
            cmbOdeljenje.DisplayMember = "naziv";
            cmbOdeljenje.SelectedValue = raspodela.Rows[br]["odeljenje_id"];
        }
        private void Raspodela_Load(object sender, EventArgs e)
        {
            Zaucitaj();
            FeelingTheCombos();
        }
    }
}
