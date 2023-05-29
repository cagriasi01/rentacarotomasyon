using System;
using System.Windows.Forms;

namespace RentACar
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frnCustomerList customerList = new frnCustomerList();
            customerList.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCustomerAdd frmCustomer = new frmCustomerAdd();
            frmCustomer.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCarAdd_Click(object sender, EventArgs e)
        {
            frmCarAdd carAdd = new frmCarAdd();
            carAdd.ShowDialog();
        }

        private void btnCarLıst_Click(object sender, EventArgs e)
        {
            formCarList carList = new formCarList();
            carList.ShowDialog();

        }

        private void btnAgreement_Click(object sender, EventArgs e)
        {
            frmAgreements agreements = new frmAgreements();
            agreements.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmSales sales = new frmSales();
            sales.ShowDialog();
        }
    }
}
