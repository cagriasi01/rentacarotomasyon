using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentACar
{
    public partial class frmAgreements : Form
    {
        public frmAgreements()
        {
            InitializeComponent();
        }
        private string connectionSentence = @"Data Source=DESKTOP-HNDHJIN;Initial Catalog=RENTACAROTOMASYONUM;Integrated Security=True";

        public void CarList()
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Arabalar where DURUMU='Bos'", connection);
            SqlDataReader dr = command.ExecuteReader();
            //command adlı sql komutunu çalıştırır. elde ettiği sonuçları hafızada tutar.

            while (dr.Read())  //dr adlı data reader nesnesinin her elemanını Read fonksiyonuyla okur. bu işlemi dr içindeki eleman bitene kadar yapar.
            {
                cbxCars.Items.Add(dr["PLAKA"]);
                //combobox elementine eleman olarak plaka ekler. 
                //her aracın plakasını ekler.
            }

        }
        private void frmAgreements_Load(object sender, EventArgs e)
        {
            CarList();
            //tüm arabaları listeleyen fonksiyon çağrıldı.

            AgreementList();
            //yapılan tüm kiralama işlemlerini listeleyen fonksiyon çağrıldı.
        }
        public void AgreementList()
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Anlasmalar", connection);
            //command nesnesi oluşturuldu. bu nesne üzerinden sorgu çalıştırıldığı zaman Anlasmalar tablosundaki tüm verieri çeker.

            SqlDataAdapter da = new SqlDataAdapter(command);
            //command nesnesi Data adapter nesnesine geçildi. 
            //SqlDataAdapter, verileri almak ve kaydetmek için ve SQL Server arasında bir DataSet köprü görevi görür. 


            DataTable dt = new DataTable();
            //data table nesnesi oluşturuldu. bu nesnesin refereansı dt'dir.

            da.Fill(dt);
            //data ataper nesnesini temsil eden da içeriğini data table nesnesi olan dt'ye aktarıldı.

            dataGridView1.DataSource = dt;
            //dt içindeki veriler datagridview nesnesine aktarıldı.

            connection.Close();
            //bağlantı kapatıldı..

        }


        private void cbxCars_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Arabalar where PLAKA like '" + cbxCars.SelectedItem + "'", connection);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                txtBrand.Text = dr["MARKA"].ToString();
                txtSeries.Text = dr["SERI"].ToString();
                txtModel.Text = dr["MODELYIL"].ToString();
                txtColor.Text = dr["RENK"].ToString();

            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            TimeSpan differenceDays = DateTime.Parse(datetimeCenter.Text) - DateTime.Parse(datetimeExit.Text);
            //DateTime.Parse() fonksiyonu datetimeCenter içindeki değeri DateTime nesnesine dönüştürür.
            //normalde datetimeCenter.Text değeri string bir ifadeye karşılık gelir.
            //Bu satırda ayrıca iki tarih arasındaki fark hesaplanır ve differenceDays adlı değişkene atanır..


            int CalculateDays = differenceDays.Days;
            //differenceDays adlı değişkenden iki tarih arasındaki gün farkı CalculateDays adlı değişkene atanır.


            txtDate.Text = CalculateDays.ToString();
            //CalculateDays adlı değişken stringe çevrilir. daha sonra txtDate adlı inputun değerine atanır.

            txtAmount.Text = (CalculateDays * int.Parse(txtRentMoney.Text)).ToString();
            //txtRentMoney.Text içindeki değer CalculateDays değişkeniyle çarpılır ve stringe çevrilir.
            //txtRentMoney.Text arabanın kiralama ücretini tutar.
            // txtAmount.Text ifadesi ile txtAmount inputuna toplam kiralama ücreti atanır.



        }

        private void cbxRentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Select KIRAUCRETI  from Arabalar", connection);
            SqlDataReader dr = command.ExecuteReader();
            //command nesnesi kullanılarak data reader nesnesi oluşturuldu. bu nesne sayesinde elemanları tek tek okuruz.

            while (dr.Read()) //dr ile tüm veriler tek tek okunur.
            {
                if (cbxRentType.SelectedIndex == 0) //renttype yani kiralama türü (kiralık,haftalık,aylık olabilir.) seçilen indekse göre farklılık gösterir.
                                                    //selected index(seçilen index) değiştiği zaman arabanın kiralama ücreti de değişir. bu değişim aşağıdaki katsayılarla olur. 
                {
                    txtAmount.Text = (int.Parse(dr["KIRAUCRETI"].ToString()) * 1).ToString();
                    //dr nesnesi içindeki KIRAUCRETI özelliği alındı.
                    //araba eğer günlük kiralanacaksa 1 ile çarpılır. mesela kiralama ücreti 1000 tl ise 1 ile çarpımı 1000 tl edecektir.
                    //günük kiralamada indirim yapılmayacaktır.
                }
                else if (cbxRentType.SelectedIndex == 1)
                {

                    txtAmount.Text = (int.Parse(dr["KIRAUCRETI"].ToString()) * 0.80).ToString();
                    //haftalık kiralamada ise katsayı 0.80 olacaktır.  yani kirası 10 bin tl olan araç haftalık olarak kiralandığı için 8 bin tl olacaktır.
                }
                else if (cbxRentType.SelectedIndex == 2)
                {
                    txtAmount.Text = (int.Parse(dr["KIRAUCRETI"].ToString()) * 0.50).ToString();
                    //aylık kiralamada ise %50 oranında bir indirim yapılır. (katsayı 0.50 olduğu için)
                }

            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();

            string commandSentence = "Insert Into Anlasmalar Values (@tcno,@AdSoyad,@Telefon,@ehliyetno,@ehliyettarih,@plaka,@Marka,@Seri,@Model,@Renk,@kirasekli,@kiraücreti,@kiralanangünsayisi,@tutar,@cikistarih,@dönüstarih)";
            SqlCommand command = new SqlCommand(commandSentence, connection);
            command.Parameters.AddWithValue("@tcno", txtTc.Text);
            command.Parameters.AddWithValue("@AdSoyad", txtNameSurname.Text);
            command.Parameters.AddWithValue("@Telefon", txtTel.Text);
            command.Parameters.AddWithValue("@ehliyetno", txtDrivingLicenceNo.Text);
            command.Parameters.AddWithValue("@ehliyettarih", txtDrivingDate.Text);
            command.Parameters.AddWithValue("@plaka", cbxCars.Text);
            command.Parameters.AddWithValue("@Marka", txtBrand.Text);
            command.Parameters.AddWithValue("@Seri", txtSeries.Text);
            command.Parameters.AddWithValue("@Model", txtModel.Text);
            command.Parameters.AddWithValue("@Renk", txtColor.Text);
            command.Parameters.AddWithValue("@kirasekli", cbxRentType.SelectedItem);
            command.Parameters.AddWithValue("@kiraücreti", txtRentMoney.Text);
            command.Parameters.AddWithValue("@kiralanangünsayisi", txtDate.Text);
            command.Parameters.AddWithValue("@tutar", txtAmount.Text);
            command.Parameters.AddWithValue("@cikistarih", datetimeExit.Value);
            command.Parameters.AddWithValue("@dönüstarih", datetimeCenter.Value);

            string commandSentenceUp = "update Arabalar set DURUMU= 'Dolu' where PLAKA ='" + cbxCars.SelectedItem + "'";
            SqlCommand commandUp = new SqlCommand(commandSentenceUp, connection);

            commandUp.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
            AgreementList();
            CarList();
            MessageBox.Show("Kayıt Başarılı");
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Musteriler where TCNO like '" + txtTcSearch.Text + "'", connection);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read()) //dr nesnesi ile dr içindeki veriler tek tek okunur.
            {
                txtTc.Text = dr["TCNO"].ToString(); //dr içindeki TCNO alanı stringe çevrilir ve txtTc adlı inputa aktarılır.
                txtNameSurname.Text = dr["ADSOYAD"].ToString();
                txtTel.Text = dr["TELEFONNO"].ToString();

            }
        }

        private void btnVehicleDelivery_Click(object sender, EventArgs e)//araç teslim
        {
            DataGridViewRow line = dataGridView1.CurrentRow;
            //dataGridView1 elementinde o an seçili olan satır line nesnesine aktarıldı.


            DateTime today = DateTime.Parse(DateTime.Now.ToShortDateString());
            //şu anki tarihi string olarak kısa tarihe çevirir. daha sonra DateTime.Parse() fonkyionuyla tekrar DateTime nesnesine dönüştürüldü.
            //yoday isimli değişkene aktarır.

            int pay = int.Parse(line.Cells["KIRAUCRETI"].Value.ToString());
            //satırdakü hücrelerden KIRAUCRETI adlı hücrenin değerini stringe çevirir ve pay adlı değişkene aktarır.


            int amount = int.Parse(line.Cells["Amount"].Value.ToString());
            //satırdaki hücrelerden Amount(miktar) temsil eden hücrenin değerini stringe çevirir. ve amount adlı değişkene aktarır.


            DateTime exit = DateTime.Parse(line.Cells["ExitDate"].Value.ToString());
            //satırdaki hücrelerden ExitDate(çıkış tarihi) denk gelen hücrenin değerini stringe çevirir. ve exit isimli değişkene aktarır.


            TimeSpan days = today - exit;
            //bugün  ile exit tarihi arasındaki farkı gunfark adlı değişkene atar.

            int Day = days.Days;
            //gunfark adlı nesne üzerinden gün sayısını Day adlı değişkene aktarır.

            int totalAmount = Day - pay;
            //toplam miktar hesabı yapıldı.

            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();
            string commandSentence = "Delete from Anlasmalar where LISANSPLAKA= '" + line.Cells["LicensePlate"].Value.ToString() + "'";
            SqlCommand command = new SqlCommand(commandSentence, connection);
            command.ExecuteNonQuery();


            string commandSentenceUp = "update Arabalar set DURUMU = 'Bos' where PLAKA = '" + line.Cells["NumberPlate"].Value.ToString() + "'";
            SqlCommand commandUp = new SqlCommand(commandSentence, connection);
            commandUp.ExecuteNonQuery();

            string commandSentenceSales = "Insert Into Satislar Values (@TcNo,@NameSurname,@LicensePlate,@RentType,@RentMoney,@Amount,@ExitDate,@ReturnDate)";
            SqlCommand commandSales = new SqlCommand(commandSentenceSales, connection);
            commandSales.Parameters.AddWithValue("@TcNo", line.Cells["TcNo"].Value.ToString());
            commandSales.Parameters.AddWithValue("@NameSurname", line.Cells["NameSurname"].Value.ToString());
            commandSales.Parameters.AddWithValue("@LicensePlate", line.Cells["LicensePlate"].Value.ToString());
            commandSales.Parameters.AddWithValue("@day", Day);
            commandSales.Parameters.AddWithValue("@RentType", line.Cells["RentType"].Value.ToString());
            commandSales.Parameters.AddWithValue("@RentMoney", pay);
            commandSales.Parameters.AddWithValue("@Amount", totalAmount);
            commandSales.Parameters.AddWithValue("@ExitDate", line.Cells["ExitDate"].Value.ToString());
            commandSales.Parameters.AddWithValue("@ReturnDate", line.Cells["ReturnDate"].Value.ToString());
            commandSales.ExecuteNonQuery();

            MessageBox.Show("Araç Teslim Edildi");
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
