using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentACar
{
    public partial class frnCustomerList : Form
    {
        private string connectionSentence = @"Data Source=DESKTOP-HNDHJIN;Initial Catalog=RENTACAROTOMASYONUM;Integrated Security=True";

        public frnCustomerList()
        {
            InitializeComponent();
        }

        private void frnCustomerList_Load(object sender, EventArgs e)
        {
            CustomerList();
        }
        public void CustomerList()
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From  Musteriler ", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();


        }

        private void button1_Click(object sender, EventArgs e)//güncelle butonu
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Update Musteriler set TCNO=@TcNo,ADSOYAD=@NameSurname,TELEFONNO=@PhoneNumber,EPOSTA=@Mail,ADRES=@Adress, EHLIYETNO=@EhliyetNo, EHLIYETTARIHI=@EhliyetTarih", connection);
            command.Parameters.AddWithValue("@TcNo", txtTc.Text);
            command.Parameters.AddWithValue("@NameSurname", txtNameSurname.Text);
            command.Parameters.AddWithValue("@PhoneNumber", maskedTextBox1.Text);
            command.Parameters.AddWithValue("@Mail", txtMail.Text);
            command.Parameters.AddWithValue("@Adress", txtAdress.Text);
            command.Parameters.AddWithValue("@EhliyetNo", txtEhliyetNo.Text);



            DateTime ehliyetTarih;
            //DateTime türünde ehliyetTarih adlı değişken oluşturuldu.

            if (DateTime.TryParse(txtEhliyetTarih.Text, out ehliyetTarih))
            {
                ehliyetTarih.ToShortDateString();
                //bu değişken kısa formatta tarihe çevrildi.
                //out parametresi ile de sonuç ehliyetTarih adlı değişkene aktarıldı.
            }


            command.Parameters.AddWithValue("@EhliyetTarih", ehliyetTarih);
            command.ExecuteNonQuery();
            CustomerList();
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)//delete butonu
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Delete from Musteriler where TCNO='" + dataGridView1.CurrentRow.Cells["TCNO"].Value.ToString() + "'", connection);
            command.ExecuteNonQuery();
            CustomerList();
            connection.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNameSurname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAdress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEhliyetNo.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtEhliyetTarih.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEhliyetTarih_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
