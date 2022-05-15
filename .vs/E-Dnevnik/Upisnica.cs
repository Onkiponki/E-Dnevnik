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
    public partial class Upisnica : Form
    {
        SqlConnection veza = Konekcija.zakonektuj();
        SqlDataAdapter adapter;
        DataTable odeljenje;
        DataTable godina, nastavnik, smer;
        DataTable upisnica;

        private void FeelingTheCombosY()
        {
            string naredba;
            SqlConnection veza = Konekcija.zakonektuj();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Skolska_godina", veza);
            DataTable dtGodina = new DataTable();
            adapter.Fill(dtGodina);
            cmbGodina.DataSource = dtGodina;
            cmbGodina.DisplayMember = "naziv";
            cmbGodina.ValueMember = "id";
            cmbGodina.SelectedValue = 2;
            

        }
        private void FeelingTheCombosO()
        {
            string godina = cmbGodina.SelectedValue.ToString();
            SqlConnection veza = Konekcija.zakonektuj();
            SqlDataAdapter adapter = new SqlDataAdapter($"select id, str(razred) + indeks as Naziv from Odeljenje where godina_id ={godina}",veza);
            odeljenje = new DataTable();
            adapter.Fill(odeljenje);
            cmbOdeljenje.DataSource = odeljenje;
            cmbOdeljenje.ValueMember = "id";
            cmbOdeljenje.DisplayMember = "Naziv";
            cmbOdeljenje.SelectedIndex = -1;
        }
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null )
            {
                int br = dataGridView1.CurrentRow.Index;
                if (br >= 0)
                {
                    labelId.Text = dataGridView1.Rows[br].Cells["id"].Value.ToString();
                    cmbUcenik.SelectedValue = dataGridView1.Rows[br].Cells["Ucenik"].Value.ToString();
                }
                
               
            }
            
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbGodina_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbGodina.IsHandleCreated && cmbGodina.Focused)
            {
                FeelingTheCombosO();
                cmbOdeljenje.SelectedIndex = -1;
                cmbUcenik.Enabled = false;
                while (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
                }
                cmbUcenik.SelectedIndex = -1;
                labelId.Text = "";
            }
           
        }

        private void cmbOdeljenje_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbOdeljenje.IsHandleCreated && cmbOdeljenje.Focused)
            {
                cmbUcenik.Enabled = true;
                FeelingTheCombosU();
                gridPopulate();
                
            }
        }
        private void FeelingTheCombosU()
        {
            SqlConnection veza = Konekcija.zakonektuj();
            SqlDataAdapter adapter = new SqlDataAdapter($"select Osoba.id, ime + ' ' + prezime as Naziv from Osoba join Upisnica on Osoba.id = Upisnica.osoba_id where uloga = 1",veza);
            DataTable ucenik = new DataTable();
            adapter.Fill(ucenik);
            cmbUcenik.DataSource = ucenik;
            cmbUcenik.ValueMember = "id";
            cmbUcenik.DisplayMember = "Naziv";
            cmbUcenik.SelectedIndex = -1;
        }
        public Upisnica()
        {
            InitializeComponent();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            string naredba = $"insert into upisnica (odeljenje_id, osoba_id) values('{cmbOdeljenje.SelectedValue.ToString()}','{cmbUcenik.SelectedValue.ToString()}')";
            SqlConnection veza = Konekcija.zakonektuj();
            SqlCommand komanda = new SqlCommand(naredba, veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
                throw;
            }
            
            gridPopulate();

        }

        private void update_Click(object sender, EventArgs e)
        {
            string naredba = $"update Upisnica set osoba_id={cmbUcenik.SelectedValue.ToString()} where id={labelId.Text} ";
            SqlConnection veza = Konekcija.zakonektuj();
            SqlCommand komanda = new SqlCommand(naredba, veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
                throw;
            }
            gridPopulate();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string naredba = $"delete from Upisnica where id = {labelId.Text}";
            SqlConnection veza = Konekcija.zakonektuj();
            SqlCommand komanda = new SqlCommand(naredba, veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
                throw;
            }
            gridPopulate();
        }

        private void gridPopulate()
        {
            SqlConnection veza = Konekcija.zakonektuj();
            SqlDataAdapter adapter = new SqlDataAdapter($"select Upisnica.id as id, ime + ' ' + prezime as Naziv, Osoba.id as Ucenik from Upisnica join Osoba on Osoba.id = Upisnica.osoba_id where odeljenje_id={cmbOdeljenje.SelectedValue.ToString()}", veza);
            DataTable upisnica = new DataTable();
            adapter.Fill(upisnica);
            dataGridView1.DataSource = upisnica;
            dataGridView1.Columns["Ucenik"].Visible = false;
        }
        private void Upisnica_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            FeelingTheCombosY();
            FeelingTheCombosO();
            cmbUcenik.Enabled = false;

        }
    }
}
