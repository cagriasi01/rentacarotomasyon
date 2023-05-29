using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentACar
{
    public partial class frmCustomerAdd : Form
    {
        public frmCustomerAdd()
        {
            InitializeComponent();
        }

        private string connectionSentence = @"Data Source=DESKTOP-HNDHJIN;Initial Catalog=RENTACAROTOMASYONUM;Integrated Security=True";
        //bağlantı ayarları veritabanı ismi gibi parametreler verildi.
        private void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerAdd_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//kaydetme butonu

        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            //daha önce oluşturulan bağlantı cümlesi kullanılarak bağlantı nesnesi oluşturuldu.

            connection.Open();
            //bağlantı açıldı.

            SqlCommand command = new SqlCommand("Insert Into Musteriler Values(@TcNo,@NameSurname,@PhoneNumber,@Mail,@Adress,@EhliyetNo,@EhliyetTarih)", connection);
            //connectin nesnesi kullanılarak örnek bir Insert sorgusu oluşturuldu. @ ile başlayan alanlar kullanıcıdan gelen parametrelerle doldurulacak.


            command.Parameters.AddWithValue("@TcNo", txtTc.Text);
            //command adlı sql komutuna @TcNo adlı alanına, formdakii txtTc adına sahip input elementinin içeriği geçildi.

            command.Parameters.AddWithValue("@NameSurname", txtNameSurname.Text);
            command.Parameters.AddWithValue("@PhoneNumber", maskedTextBox1.Text);
            command.Parameters.AddWithValue("@Mail", txtMail.Text);
            command.Parameters.AddWithValue("@Adress", txtAdress.Text);
            command.Parameters.AddWithValue("@EhliyetNo", txtEhliyetNo.Text);
            command.Parameters.AddWithValue("@EhliyetTarih", txtEhliyetTarih.Text);
            command.ExecuteNonQuery();
            //sorgu çalıştırıldı
            connection.Close();
            //bağlantı kapatıldı.

            MessageBox.Show("Kayıt Başarılı");
            //işlemin başarılı olduğuna dair kullanıcıya mesaj gönderildi.

        }// şarjım bitti : (anladım jdgjdgurdur takayım tmdır sahur yap sonra bakalım isiyorsan tmmdır 4 olmuş zatenhadi afiyet olsun sana da msj atarsın tmm

        private void txtAdress_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
