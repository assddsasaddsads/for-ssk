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
    public partial class FStorage : Form
    {
        public FStorage()
        {
            InitializeComponent();
            ShowStorage();
            ShowSuppliers();
            ShowProducts();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxSuppliers.SelectedItem !=null && comboBoxProducts.SelectedItem !=null)
            {
                Storage storage = new Storage();
                storage.Id_Suppliers = Convert.ToInt32(comboBoxSuppliers.SelectedItem.ToString().Split('.')[0]);
                storage.Id_Products = Convert.ToInt32(comboBoxProducts.SelectedItem.ToString().Split('.')[0]);
                Program.wfbe.Storage.Add(storage);
                Program.wfbe.SaveChanges();
                ShowStorage();
                ShowSuppliers();
                ShowProducts();
            }
            else
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Storage storage = listViewStorage.SelectedItems[0].Tag as Storage;
            storage.Id_Products = Convert.ToInt32(comboBoxProducts.Text);
            storage.Id_Suppliers = Convert.ToInt32(comboBoxSuppliers.Text);
            Program.wfbe.SaveChanges();
            ShowStorage();
            ShowSuppliers();
            ShowProducts();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewStorage.SelectedItems.Count == 1)
                {
                    Storage storage = listViewStorage.SelectedItems[0].Tag as Storage;
                    Program.wfbe.Storage.Remove(storage);
                    Program.wfbe.SaveChanges();
                    ShowStorage();
                    ShowSuppliers();
                    ShowProducts();
                }
                comboBoxProducts.Text = null;
                comboBoxSuppliers.Text = null;

            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStorage.SelectedItems.Count == 1)
            {
                Storage storage = listViewStorage.SelectedItems[0].Tag as Storage;
                comboBoxProducts.Text = storage.Id_Products.ToString();
                comboBoxSuppliers.Text = storage.Id_Suppliers.ToString();
            }
            else
            {
                comboBoxProducts.Text = null;
                comboBoxSuppliers.Text = null;
            }
        }
        void ShowStorage()
        {
            listViewStorage.Items.Clear();
            foreach (Storage storage in Program.wfbe.Storage)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    storage.Id.ToString(), storage.Id_Suppliers.ToString() + "." + storage.Suppliers.Name,
                    storage.Id_Products.ToString() + "." + storage.Products.Material.Tovar
                });
                item.Tag = storage;
                listViewStorage.Items.Add(item);
            }
            listViewStorage.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        void ShowSuppliers()
        {
            foreach (Suppliers suppliers in Program.wfbe.Suppliers)
            {
                string[] item = { suppliers.Id.ToString() + ".", suppliers.Name
                };
                comboBoxSuppliers.Items.Add(string.Join("", item));
            }
        }
        void ShowProducts()
        {
            foreach (Products products in Program.wfbe.Products)
            {
                string[] item = { products.Id.ToString() + ".", products.Id_Material.ToString() + "." + products.Material.Tovar
                };
                comboBoxProducts.Items.Add(string.Join("", item));
            }
        }
    }
}
