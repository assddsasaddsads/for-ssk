using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FClients fClients = new FClients();
            fClients.Show();
            this.Hide();
        }

        private void buttonEmployees_Click_1(object sender, EventArgs e)
        {
            FEmployees fEmployees = new FEmployees();
            fEmployees.Show();
            this.Hide();
        }

        private void buttonSuppliers_Click_1(object sender, EventArgs e)
        {
            FSuppliers fSuppliers = new FSuppliers();
            fSuppliers.Show();
            this.Hide();
        }

        private void buttonProducts_Click_1(object sender, EventArgs e)
        {
            FProducts fProducts = new FProducts();
            fProducts.Show();
            this.Hide();
        }

        private void buttonOrders_Click_1(object sender, EventArgs e)
        {
            FOrders fOrders = new FOrders();
            fOrders.Show();
            this.Hide();
        }

        private void buttonDelivery_Click_1(object sender, EventArgs e)
        {
            FDelivery fDelivery = new FDelivery();
            fDelivery.Show();
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPosition_Click(object sender, EventArgs e)
        {
            FPosition fPosition = new FPosition();
            fPosition.Show();
        }

        private void buttonStorage_Click(object sender, EventArgs e)
        {
            FStorage fstorage = new FStorage();
            fstorage.Show();
        }

        private void buttonMaterial_Click(object sender, EventArgs e)
        {
            FMaterial fMaterial = new FMaterial();
            fMaterial.Show();
        }
    }
}
