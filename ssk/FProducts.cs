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
    public partial class FProducts : Form
    {
        public FProducts()
        {
            InitializeComponent();
            ShowMaterial();
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
            if (comboBoxMaterial.SelectedItem != null && textBoxAmount.Text !="" )
            {
                Products products = new Products();
                products.Id_Material = Convert.ToInt32(comboBoxMaterial.SelectedItem.ToString().Split('.')[0]);
                products.Amount = textBoxAmount.Text;
                Program.wfbe.Products.Add(products);
                Program.wfbe.SaveChanges();
                ShowMaterial();
                ShowProducts();

            }
            else 
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count==1)
            {
                Products products = listViewProducts.SelectedItems[0].Tag as Products;
                products.Id_Material = Convert.ToInt32(comboBoxMaterial.SelectedItem.ToString().Split('.')[0]);
                products.Amount = textBoxAmount.Text;
                Program.wfbe.SaveChanges();
                ShowMaterial();
                ShowProducts();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if(listViewProducts.SelectedItems.Count==1)
                {
                    Products products = listViewProducts.SelectedItems[0].Tag as Products;
                    Program.wfbe.Products.Remove(products);
                    Program.wfbe.SaveChanges();
                }
                comboBoxMaterial.Text = null;
                textBoxAmount.Text = "";
                ShowMaterial();
                ShowProducts();
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count == 1)
            {
                Products products = listViewProducts.SelectedItems[0].Tag as Products;
                comboBoxMaterial.SelectedIndex = comboBoxMaterial.FindString(products.Id_Material.ToString());
                textBoxAmount.Text =  products.Amount;
                Program.wfbe.SaveChanges();
            }
            else
            {
                comboBoxMaterial.Text = null;
                textBoxAmount.Text = "";
            }
        }
        void ShowMaterial()
        {
            listViewProducts.Items.Clear();
            foreach(Material material in Program.wfbe.Material)
            {
                string[] item = { material.Id.ToString() + ".", material.Tovar};
                comboBoxMaterial.Items.Add(string.Join("", item));
            }
            listViewProducts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        void ShowProducts()
        {
            listViewProducts.Items.Clear();
            foreach (Products products in Program.wfbe.Products)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    products.Id.ToString(), products.Id_Material.ToString() + "." + products.Material.Tovar, products.Amount
                });
                item.Tag = products;
                listViewProducts.Items.Add(item);
            }
            listViewProducts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
