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
        DataTable ocene;
        public Ocena()
        {
            InitializeComponent();
        }

        private void Ocena_Load(object sender, EventArgs e)
        {
            FeelingTheCombosY();
            cmbPredmet.Enabled = false;
            cmbOdeljenje.Enabled = false;
            cmbUcenik.Enabled = false;
            cmbOcena.Enabled = false;
            for (int i = 0; i < 5; i++)
            {
                cmbOcena.Items.Add(i+1);
            }
            FeelingTheCombosP();
            
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
        private void cmbGodina_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbGodina.IsHandleCreated && cmbGodina.Focused)
            {
                FeelingTheCombosP();
            }
        }
        private void FeelingTheCombosP()
        {
            naredba = $"select distinct osoba.id as id, Osoba.Ime + Osoba.Prezime AS naziv from Osoba join Raspodela on Osoba.id = Raspodela.nastavnik_id where godina_id={cmbGodina.SelectedValue.ToString()}";
            SqlDataAdapter adapter = new SqlDataAdapter(naredba, veza);
            DataTable profesor = new DataTable();

            adapter.Fill(profesor);
            cmbProfesor.DataSource = profesor;
            cmbProfesor.ValueMember = "id";
            cmbProfesor.DisplayMember = "naziv";
            cmbProfesor.SelectedValue = -1;


        }

        private void cmbProfesor_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbProfesor.IsHandleCreated && cmbProfesor.Focused)
            {
                FeelingTheCombosPredmet();
                cmbPredmet.Enabled = true;

                cmbOdeljenje.Enabled = false;
                cmbOdeljenje.SelectedIndex = -1;

                cmbUcenik.SelectedIndex = -1;
                cmbUcenik.Enabled = false;

                cmbOcena.SelectedIndex = -1;
                cmbOcena.Enabled = false;

                ocene = new DataTable();
                dataGridView1.DataSource = ocene;



            }
        }

        private void FeelingTheCombosPredmet()
        {
            naredba = $"select distinct Predmet.id as id, naziv from Predmet join Raspodela on Predmet.id=Raspodela.predmet_id where godina_id ={cmbGodina.SelectedValue.ToString()} and nastavnik_id={cmbProfesor.SelectedValue.ToString()}";
            SqlDataAdapter adapter = new SqlDataAdapter(naredba, veza);
            DataTable predmet = new DataTable();
            adapter.Fill(predmet);
            cmbPredmet.DataSource = predmet;
            cmbPredmet.DataSource = predmet;
            cmbPredmet.ValueMember = "id";
            cmbPredmet.DisplayMember = "naziv";
            cmbPredmet.SelectedValue = -1;

        }

        private void cmbPredmet_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbPredmet.IsHandleCreated && cmbPredmet.Focused)
            {
                cmbOdeljenje.Enabled = true;
                FeelingTheCombosO();

                cmbUcenik.SelectedIndex = -1;
                cmbUcenik.Enabled = false;

                cmbOcena.SelectedIndex = -1;
                cmbOcena.Enabled = false;

                ocene = new DataTable();
                dataGridView1.DataSource = ocene;

            }
        }
        private void FeelingTheCombosO()
        {
            naredba = $"select distinct Odeljenje.id as id, str(razred) + '-' + indeks as naziv from Odeljenje join Raspodela on Odeljenje.id = Raspodela.odeljenje_id where raspodela.godina_id ={cmbGodina.SelectedValue.ToString()} and nastavnik_id={cmbProfesor.SelectedValue.ToString()} and predmet_id = {cmbPredmet.SelectedValue.ToString()}";
            SqlDataAdapter adapter = new SqlDataAdapter(naredba,veza);
            DataTable odeljenje = new DataTable();
            adapter.Fill(odeljenje);
            cmbOdeljenje.DataSource = odeljenje;
            cmbOdeljenje.ValueMember = "id";
            cmbOdeljenje.DisplayMember = "naziv";
            cmbOdeljenje.SelectedIndex = -1;
        }

        private void cmbOdeljenje_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbOdeljenje.IsHandleCreated && cmbOdeljenje.Focused)
            {
                FeelingTheCombosU();
                cmbUcenik.Enabled = true;
                FeelingTheGrid();
                UcenikOcenaIdSet(0);
                cmbOcena.Enabled = true;
            }
            
        }
        private void FeelingTheCombosU()
        {
            naredba = $"select Osoba.id as id, ime + prezime as naziv from Osoba join Upisnica on Osoba.id = Upisnica.osoba_id where Upisnica.odeljenje_id={cmbOdeljenje.SelectedValue.ToString()}";
            SqlDataAdapter adapter = new SqlDataAdapter(naredba, veza);
            DataTable ucenik = new DataTable();
            adapter.Fill(ucenik);
            cmbUcenik.DataSource = ucenik;
            cmbUcenik.ValueMember = "id";
            cmbUcenik.DisplayMember = "naziv";
            cmbUcenik.SelectedIndex = -1;
        }
        private void FeelingTheGrid()
        {
            naredba = $"select Ocena.id as id, ime+' '+prezime as naziv, Ocena.ocena, ucenik_id, Ocena.datum from Osoba join Ocena on Osoba.id = Ocena.ucenik_id join Raspodela on Raspodela.id = Ocena.raspodela_id where raspodela_id in (select id from Raspodela where godina_id ={cmbGodina.SelectedValue.ToString()} and nastavnik_id ={cmbProfesor.SelectedValue.ToString()} and predmet_id = {cmbPredmet.SelectedValue.ToString()} and odeljenje_id={cmbOdeljenje.SelectedValue.ToString()})";
            textBox1.Text = naredba;
            SqlDataAdapter adapter = new SqlDataAdapter(naredba,veza);
            ocene = new DataTable();
            adapter.Fill(ocene);
            dataGridView1.DataSource = ocene;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns["ucenik_id"].Visible = false;
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UcenikOcenaIdSet(e.RowIndex);

            }
        }
        private void UcenikOcenaIdSet(int br)
        {
            cmbUcenik.SelectedValue = ocene.Rows[br]["ucenik_id"];
            cmbOcena.SelectedItem = ocene.Rows[br]["ocena"];
            label1.Text = ocene.Rows[br]["id"].ToString();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            naredba = $"select id from raspodela select id from Raspodela where godina_id ={cmbGodina.SelectedValue.ToString()} and nastavnik_id ={cmbProfesor.SelectedValue.ToString()} and predmet_id = {cmbPredmet.SelectedValue.ToString()} and odeljenje_id={cmbOdeljenje.SelectedValue.ToString()}";
            SqlCommand komanda = new SqlCommand(naredba, veza);
            int idRaspodele = 0;
            try
            {
                veza.Open();
                idRaspodele = Convert.ToInt32(komanda.ExecuteScalar());
                veza.Close();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
                throw;
            }
            if (idRaspodele > 0)
            {
                naredba = $"insert into Ocena(datum, raspodela_id, ucenik_id, ocena) values ('{datum.Value.ToString("yyyy-MM-dd")}',{idRaspodele.ToString()},{cmbUcenik.SelectedValue.ToString()},{cmbOcena.SelectedItem.ToString()})";
            }
            komanda = new SqlCommand(naredba, veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
                throw;
            }
            FeelingTheGrid();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label1.Text) > 0)
            {
                DateTime date = datum.Value;
                naredba = $"update Ocena set ucenik_id={cmbUcenik.SelectedValue.ToString()}, ocena = {cmbOcena.SelectedItem.ToString()}, datum = '{date.ToString("yyyy-MM-dd")}' where id = {label1.Text} ";
                SqlCommand komand = new SqlCommand(naredba,veza);
                try
                {
                    veza.Open();
                    komand.ExecuteNonQuery();
                    veza.Close();
                }
                catch (Exception greska)
                {
                    MessageBox.Show(greska.Message);
                    throw;
                }
                FeelingTheGrid();
            }
            
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label1.Text) > 0)
            {
                naredba = $"delete from Ocena where id = {label1.Text}";
                SqlCommand komand = new SqlCommand(naredba, veza);
                try
                {
                    veza.Open();
                    komand.ExecuteNonQuery();
                    veza.Close();
                    UcenikOcenaIdSet(0);
                }
                catch (Exception greska)
                {
                    MessageBox.Show(greska.Message);
                    throw;
                }
                FeelingTheGrid();
            }
        }
        
    }
}
