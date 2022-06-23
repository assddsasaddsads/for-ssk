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
    public partial class FMaterial : Form
    {
        public FMaterial()
        {
            InitializeComponent();
            ShowMaterial();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxMaterial.Text != "" && textBoxTovar.Text != "" && textBoxPrice.Text != ""
                && textBoxAmount.Text != "" && textBoxColour.Text != "" && textBoxSize.Text != "")
            {
                Material material = new Material();
                material.Material1 = textBoxMaterial.Text;
                material.Tovar = textBoxTovar.Text;
                material.Price = textBoxPrice.Text;
                material.AmountSHT = textBoxAmount.Text;
                material.Colour = textBoxColour.Text;
                material.Size = textBoxSize.Text;
                Program.wfbe.Material.Add(material);
                Program.wfbe.SaveChanges();
                ShowMaterial();

            }
            else
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewMaterial.SelectedItems.Count == 1)
            {
                Material material = listViewMaterial.SelectedItems[0].Tag as Material;
                material.Material1 = textBoxMaterial.Text;
                material.Tovar = textBoxTovar.Text;
                material.Price = textBoxPrice.Text;
                material.AmountSHT = textBoxAmount.Text;
                material.Colour = textBoxColour.Text;
                material.Size = textBoxSize.Text;
                Program.wfbe.SaveChanges();
                ShowMaterial();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewMaterial.SelectedItems.Count == 1)
                {
                    Material material = listViewMaterial.SelectedItems[0].Tag as Material;
                    Program.wfbe.Material.Remove(material);
                    Program.wfbe.SaveChanges();
                    ShowMaterial();
                }
                textBoxMaterial.Text = "";
                textBoxTovar.Text = "";
                textBoxPrice.Text = "";
                textBoxAmount.Text = "";
                textBoxColour.Text = "";
                textBoxSize.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMaterial.SelectedItems.Count == 1)
                {
                Material material = listViewMaterial.SelectedItems[0].Tag as Material;
                textBoxMaterial.Text = material.Material1;
                textBoxTovar.Text = material.Tovar;
                textBoxPrice.Text = material.Price;
                textBoxAmount.Text = material.AmountSHT;
                textBoxColour.Text = material.Colour;
                textBoxSize.Text = material.Size;
                Program.wfbe.SaveChanges();
            }
            else
            {
                textBoxMaterial.Text = "";
                textBoxTovar.Text = "";
                textBoxPrice.Text = "";
                textBoxAmount.Text = "";
                textBoxColour.Text = "";
                textBoxSize.Text = "";
            }
        }
        void ShowMaterial()
        {
            listViewMaterial.Items.Clear();
            foreach (Material material in Program.wfbe.Material)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    material.Id.ToString(), material.Material1, material.Tovar, material.Price, material.AmountSHT,
                    material.Colour, material.Size
                });
                item.Tag = material;
                listViewMaterial.Items.Add(item);
            }
            listViewMaterial.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}