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
    public partial class FDelivery : Form
    {
        public FDelivery()
        {
            InitializeComponent();
            ShowDelivery();
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
            if (comboBoxOrders.SelectedItem != null && textBoxAddress.Text != "" && textBoxPrice.Text != "")
            {
                Delivery delivery = new Delivery();
                delivery.Id_Orders = Convert.ToInt32(comboBoxOrders.SelectedItem.ToString().Split('.')[0]);
                delivery.Address = textBoxAddress.Text;
                delivery.Price = textBoxPrice.Text;
                Program.wfbe.Delivery.Add(delivery);
                Program.wfbe.SaveChanges();
                ShowDelivery();
                ShowOrders();
            }
            else
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewDelivery.SelectedItems.Count == 1)
            {
                Delivery delivery = listViewDelivery.SelectedItems[0].Tag as Delivery;
                delivery.Id_Orders = Convert.ToInt32(comboBoxOrders.Text);
                delivery.Address = textBoxAddress.Text;
                delivery.Price = textBoxPrice.Text;
                Program.wfbe.SaveChanges();
                ShowDelivery();
                ShowOrders();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDelivery.SelectedItems.Count == 1)
                {
                    Delivery delivery = listViewDelivery.SelectedItems[0].Tag as Delivery;
                    Program.wfbe.Delivery.Remove(delivery);
                    Program.wfbe.SaveChanges();
                }
                comboBoxOrders.Text = null;
                textBoxAddress.Text = "";
                textBoxPrice.Text = "";
                ShowDelivery();
                ShowOrders();
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDelivery.SelectedItems.Count == 1)
            {
                Delivery delivery = listViewDelivery.SelectedItems[0].Tag as Delivery;
                comboBoxOrders.Text = delivery.Id_Orders.ToString();
                textBoxAddress.Text = delivery.Address;
                textBoxPrice.Text = delivery.Price;
                Program.wfbe.SaveChanges();
            }
            else
            {
                comboBoxOrders.Text = null;
                textBoxAddress.Text = "";
                textBoxPrice.Text = "";
            }
        }
        void ShowDelivery()
        {
            listViewDelivery.Items.Clear();
            foreach (Delivery delivery in Program.wfbe.Delivery)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    delivery.Id.ToString(), delivery.Orders.Id.ToString(), delivery.Address,
                    delivery.Price
                });
                item.Tag = delivery;
                listViewDelivery.Items.Add(item);
            }
            listViewDelivery.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        void ShowOrders()
        {
            comboBoxOrders.Text = null;
            foreach (Orders orders in Program.wfbe.Orders)
            {
                string[] item = { orders.Id.ToString() };
                comboBoxOrders.Items.Add(string.Join("", item));
            }
        }
    }
}