using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentACar
{
    public partial class formCarList : Form
    {
        private string connectionSentence = @"Data Source=DESKTOP-HNDHJIN;Initial Catalog=RENTACAROTOMASYONUM;Integrated Security=True";

        public formCarList()
        {
            InitializeComponent();
        }
        public void Car_List()
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();

            String commandSentence = "Select * From Arabalar";
            SqlCommand command = new SqlCommand(commandSentence, connection);


            SqlDataAdapter da = new SqlDataAdapter(command);
            //command komutundan gelen veriler da adlı data adapter sınıfında tutulur.

            DataTable dt = new DataTable();
            //datatable nesnesi oluşturuldu.

            da.Fill(dt);
            //da tarafından tutulan veriler dt adlı datatable nesnesine aktarıldı.


            dataGridView1.DataSource = dt;
            //dataGridView1 form componentine dt içindeki veriler aktarılır.


            connection.Close();
            //bağlantı kapatıldı.
        }
        private void formCarList_Load(object sender, EventArgs e)
        {
            Car_List();
        }
        public void Car_Update()
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();

            string commandSentence = "Update Arabalar set MARKA=@Brand,SERI=@Series,MODELYIL=@Model,RENK=@Color,KILOMETRE=@Kilometer,YAKIT=@Fuel,KIRAUCRETI=@RentMoney where PLAKA=@NumberPlate";
            //bu sorgu cümlesinde arabaların güncelleme işlemi yapılıyor. eşitliğin sol tarafındaki ifadeler tablodaki sütun adları karşılık gelir.
            //eşitliğin sağ tarafındaki ifadeler ise kullanıcının girdiği verilerdir. sütundaki veriler ile kullanıcının girdiği veriyi değiştirir.

            SqlCommand command = new SqlCommand(commandSentence, connection);
            command.Parameters.AddWithValue("@NumberPlate", txtlicenseplate.Text);
            command.Parameters.AddWithValue("@Brand", cmbBrand.Text);
            command.Parameters.AddWithValue("@Series", cmbSeries.Text);
            command.Parameters.AddWithValue("@Model", txtModel.Text);
            command.Parameters.AddWithValue("@Color", txtColor.Text);
            command.Parameters.AddWithValue("@Kilometer", txtKilometer.Text);
            command.Parameters.AddWithValue("@Fuel", cmbFuil.Text);
            command.Parameters.AddWithValue("@RentMoney", txtRentAmount.Text);

            command.ExecuteNonQuery();
            connection.Close();
            Car_List();
            //Car_List(tüm arabaları görüntüler.) adlı fonksiyon çağrıldı.

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Car_Update();
            //Car_Update(araba güncelleme) fonksiyonu çağrıldı.

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtlicenseplate.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //dataGridView1 içinde o an tıklanılanılan satırın 1 hücresindeki değeri stringe çevirir.
            //daha sonra txtlicenseplate adlı input nesnesine aktarır.

            cmbBrand.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbSeries.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtColor.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKilometer.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cmbFuil.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtRentAmount.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            //bağlantı cümlesi kullanılarak bağlantı nesnesi oluşturuldu.
            connection.Open();
            //bağlantı nesnesi kullanılarak bağlantı açıldı.


            string commandSentence = "Delete from Arabalar where PLAKA='" + dataGridView1.CurrentRow.Cells["PLAKA"].Value.ToString() + "'";
            //bu sorgu parçalı bir sorgudur. "dataGridView1.CurrentRow.Cells["PLAKA"].Value.ToString() " ifadesinden gelen sonuç DELETE sorgusundaki 
            //where PLAKA = ifadesinde eşittirin sağ tarafına gelecek. yani kullanıcı bir satırda bir hücreye tıkladığında tıklanılan hücrenin id değeri DELETE sorgusuna gönderiir.
            //böyle bir komut cümlesi oluşturuldu. 



            SqlCommand command = new SqlCommand(commandSentence, connection);
            //bağlantı nesnesi ve komut cümlesi kullanılarak command nesnesi oluşturuldu.


            command.ExecuteNonQuery();
            //command nesnesi ile sorgu çalıştırıldı.

            connection.Close();
            //bağlantı kapatıldı. bağlantının kapatılmması kaynakların gereksiz yere kullanılmaması için önemlidir.

            Car_List();
            //arabaları listeleyen fonksiyon çağrıldı.
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //uygulamayı kapatır.
        }
    }
}
