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
    public partial class FEmployees : Form
    {
        public FEmployees()
        {
            InitializeComponent();
            ShowEmployees();
            ShowPosition();
        }



        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Id_Position = Convert.ToInt32(comboBoxPosition.SelectedItem.ToString().Split('.')[0]);
            employees.FirstName = textBoxFirstName.Text;
            employees.MiddleName = textBoxMiddleName.Text;
            employees.LastName = textBoxLastName.Text;
            employees.Phone = Int64.Parse(textBoxPhone.Text);
            employees.Address = textBoxAddress.Text;
            Program.wfbe.Employees.Add(employees);
            Program.wfbe.SaveChanges();
            ShowEmployees();
            ShowPosition();
        }

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {
            if (listViewEmployees.SelectedItems.Count == 1)
            {
                Employees employees = listViewEmployees.SelectedItems[0].Tag as Employees;
                employees.Id_Position = Convert.ToInt32(comboBoxPosition.Text);
                employees.FirstName = textBoxFirstName.Text;
                employees.MiddleName = textBoxMiddleName.Text;
                employees.LastName = textBoxLastName.Text;
                employees.Phone = Int64.Parse(textBoxPhone.Text);
                employees.Address = textBoxAddress.Text;
                Program.wfbe.SaveChanges();
                ShowEmployees();
                ShowPosition();
            }
        }

        private void buttonDel_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (listViewEmployees.SelectedItems.Count == 1)
                {
                    Employees employees = listViewEmployees.SelectedItems[0].Tag as Employees;
                    Program.wfbe.Employees.Remove(employees);
                    Program.wfbe.SaveChanges();
                    ShowPosition();
                    ShowEmployees();
                }
                comboBoxPosition.Text = null;
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxAddress.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
        void ShowPosition()
        {
            comboBoxPosition.Items.Clear();
            foreach (Position position in Program.wfbe.Position)
            {
                string[] item = { position .Id.ToString() + ".", position.Name};
                comboBoxPosition.Items.Add(string.Join("", item));
            }
            listViewEmployees.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listViewEmployees_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listViewEmployees.SelectedItems.Count == 1)
            {
                Employees employees = listViewEmployees.SelectedItems[0].Tag as Employees;
                comboBoxPosition.Text = employees.Id_Position.ToString();
                textBoxFirstName.Text = employees.FirstName;
                textBoxMiddleName.Text = employees.MiddleName;
                textBoxLastName.Text = employees.LastName;
                textBoxPhone.Text = employees.Phone.ToString();
                textBoxAddress.Text = employees.Address;
            }
            else
            {
                comboBoxPosition.Text = null;
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxAddress.Text = "";
            }
        }
        void ShowEmployees()
        {
            listViewEmployees.Items.Clear();
            foreach (Employees employees in Program.wfbe.Employees)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                   employees.Id.ToString(), employees.FirstName, employees.MiddleName, employees.LastName,
                   employees.Phone.ToString(), employees.Address, employees.Position.Id.ToString() + ". " + employees.Position.Name
                });
                item.Tag = employees;
                listViewEmployees.Items.Add(item);
            }
            listViewEmployees.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
