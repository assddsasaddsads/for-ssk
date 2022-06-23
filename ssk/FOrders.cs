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
    public partial class FOrders : Form
    {
        public FOrders()
        {
            InitializeComponent();
            ShowEmployees();
            ShowClients();
            ShowProducts();
            ShowOrders();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxClients.SelectedItem != null && comboBoxProduct.SelectedItem != null && 
                comboBoxEmployees.SelectedItem !=null && textBoxPrice.Text !="" && textBoxDate.Text !="")
            {
                Orders orders = new Orders();
                orders.Id_Clients = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                orders.Id_Products = Convert.ToInt32(comboBoxProduct.SelectedItem.ToString().Split('.')[0]);
                orders.Id_Employees = Convert.ToInt32(comboBoxEmployees.SelectedItem.ToString().Split('.')[0]);
                orders.Price = textBoxPrice.Text;
                orders.Date = Convert.ToDateTime(textBoxDate.Text);
                Program.wfbe.Orders.Add(orders);
                Program.wfbe.SaveChanges();
                ShowEmployees();
                ShowClients();
                ShowProducts();
                ShowOrders();
            }
            else
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedItems.Count==1)
            {
                Orders orders = listViewOrders.SelectedItems[0].Tag as Orders;
                orders.Id_Clients = Convert.ToInt32(comboBoxClients.Text);
                orders.Id_Products = Convert.ToInt32(comboBoxProduct.Text);
                orders.Id_Employees = Convert.ToInt32(comboBoxEmployees.Text);
                orders.Price = textBoxPrice.Text;
                orders.Date = Convert.ToDateTime(textBoxDate.Text);
                Program.wfbe.SaveChanges();
                ShowEmployees();
                ShowClients();
                ShowProducts();
                ShowOrders();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewOrders.SelectedItems.Count == 1)
                {
                    Orders orders = listViewOrders.SelectedItems[0].Tag as Orders;
                    Program.wfbe.Orders.Remove(orders);
                    Program.wfbe.SaveChanges();
                }
                comboBoxClients.Text = null;
                comboBoxProduct.Text = null;
                comboBoxEmployees.Text = null;
                textBoxPrice.Text = "";
                textBoxDate.Text = "";
                ShowEmployees();
                ShowClients();
                ShowProducts();
                ShowOrders();
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedItems.Count==1)
            {
                Orders orders = listViewOrders.SelectedItems[0].Tag as Orders;
                comboBoxClients.Text = orders.Id_Clients.ToString();
                comboBoxProduct.Text = orders.Id_Products.ToString();
                comboBoxEmployees.Text = orders.Id_Employees.ToString();
                textBoxPrice.Text = orders.Price;
                textBoxDate.Text = orders.Date.ToString();
            }
            else
            {
                comboBoxClients.Text = null;
                comboBoxProduct.Text = null;
                comboBoxEmployees.Text = null;
                textBoxPrice.Text = "";
                textBoxDate.Text = "";
            }
        }
        void ShowClients()
        {
            listViewOrders.Items.Clear();
            foreach(Clients clients in Program.wfbe.Clients)
            {
                string[] item = { clients.Id.ToString() + ".", clients.FirstName + " " + clients.MiddleName
                + " " + clients.LastName};
                comboBoxClients.Items.Add(string.Join("", item));
            }
        }
        void ShowProducts()
        {
            listViewOrders.Items.Clear();
            foreach (Products products in Program.wfbe.Products)
            {
                string[] item = { products.Id.ToString() + "." + products.Material.Tovar
                };
                comboBoxProduct.Items.Add(string.Join("", item));
            }
        }
        void ShowEmployees()
        {
            listViewOrders.Items.Clear();
            foreach (Employees employees in Program.wfbe.Employees)
            {
                string[] item = { employees.Id.ToString() + ".", employees.FirstName + " " + employees.MiddleName
                + " " + employees.LastName};
                comboBoxEmployees.Items.Add(string.Join("", item));
            }
        }
        void ShowOrders()
        {
            listViewOrders.Items.Clear();
            foreach (Orders orders in Program.wfbe.Orders)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    orders.Id.ToString(), orders.Clients.FirstName+" " + orders.Clients.MiddleName + " " +
                    orders.Clients.LastName, orders.Id_Products.ToString()+"." + orders.Products.Material.Tovar, orders.Employees.FirstName + " " + orders.Employees.MiddleName + " " + 
                    orders.Employees.LastName,orders.Price, orders.Date.ToString()
                });
                item.Tag = orders;
                listViewOrders.Items.Add(item);
            }
            listViewOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
