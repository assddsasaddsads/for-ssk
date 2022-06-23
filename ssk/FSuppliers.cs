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
    public partial class FSuppliers : Form
    {
        public FSuppliers()
        {
            InitializeComponent();
            ShowSuppliers();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Suppliers suppliers = new Suppliers();
            suppliers.Name = textBoxName.Text;
            suppliers.Phone = Int64.Parse(textBoxPhone.Text);
            suppliers.Email = textBoxMail.Text;
            suppliers.PTovar = textBoxPTovar.Text;
            Program.wfbe.Suppliers.Add(suppliers);
            Program.wfbe.SaveChanges();
            ShowSuppliers();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSuppliers.SelectedItems.Count == 1)
            {
                Suppliers suppliers = listViewSuppliers.SelectedItems[0].Tag as Suppliers;
                suppliers.Name = textBoxName.Text;
                suppliers.Phone = Int64.Parse(textBoxPhone.Text);
                suppliers.Email = textBoxMail.Text;
                suppliers.PTovar = textBoxPTovar.Text;
                Program.wfbe.SaveChanges();
                ShowSuppliers();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSuppliers.SelectedItems.Count == 1)
                {
                    Suppliers suppliers = listViewSuppliers.SelectedItems[0].Tag as Suppliers;
                    Program.wfbe.Suppliers.Remove(suppliers);
                    Program.wfbe.SaveChanges();
                }
                textBoxName.Text = "";
                textBoxPhone.Text = "";
                textBoxMail.Text = "";
                textBoxPTovar.Text = "";
                ShowSuppliers();
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSuppliers.SelectedItems.Count == 1)
            {
                Suppliers suppliers = listViewSuppliers.SelectedItems[0].Tag as Suppliers;
                textBoxName.Text = suppliers.Name;
                textBoxPhone.Text = suppliers.Phone.ToString();
                textBoxMail.Text = suppliers.Email;
                textBoxPTovar.Text = suppliers.PTovar;
            }
            else
            {
                textBoxName.Text = "";
                textBoxPhone.Text = "";
                textBoxMail.Text = "";
                textBoxPTovar.Text = "";
            }
        }
        void ShowSuppliers()
        {
            listViewSuppliers.Items.Clear();
            foreach (Suppliers suppliers in Program.wfbe.Suppliers)
            {
                ListViewItem item = new ListViewItem(new string[]
                { suppliers.Id.ToString(), suppliers.Name, suppliers.Phone.ToString(), suppliers.Email,
                suppliers.PTovar
                });
                item.Tag = suppliers;
                listViewSuppliers.Items.Add(item);
            }
            listViewSuppliers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }

}
