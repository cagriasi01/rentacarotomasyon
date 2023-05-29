using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RentACar
{
    public partial class frmSales : Form
    {
        public frmSales()
        {
            InitializeComponent();
        }
        private string connectionSentence = @"Data Source=DESKTOP-HNDHJIN;Initial Catalog=RENTACAROTOMASYONUM;Integrated Security=True";

        private void frmSales_Load(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionSentence);
            connection.Open();

            String commandSentence = "Select * From Arabalar";
            SqlCommand command = new SqlCommand(commandSentence, connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
