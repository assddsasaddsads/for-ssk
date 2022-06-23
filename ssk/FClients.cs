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
    public partial class FClients : Form
    {
        public FClients()
        {
            InitializeComponent();
            ShowClient();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients();
            clients.FirstName = textBoxFirstName.Text;
            clients.MiddleName = textBoxMiddleName.Text;
            clients.LastName = textBoxLastName.Text;
            clients.Phone = textBoxPhone.Text;
            Program.wfbe.Clients.Add(clients);
            Program.wfbe.SaveChanges();
            ShowClient();
        }
        void ShowClient()
        {
            listViewClients.Items.Clear();
            foreach(Clients clients in Program.wfbe.Clients)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    clients.Id.ToString(), clients.FirstName, clients.MiddleName, clients.LastName,
                    clients.Phone
                });
                item.Tag = clients;
                listViewClients.Items.Add(item);
            }
            listViewClients.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count == 1)
            {
                Clients clients = listViewClients.SelectedItems[0].Tag as Clients;
                clients.FirstName = textBoxFirstName.Text;
                clients.MiddleName = textBoxMiddleName.Text;
                clients.LastName = textBoxLastName.Text;
                clients.Phone = textBoxPhone.Text;
                Program.wfbe.SaveChanges();
                ShowClient();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewClients.SelectedItems.Count==1)
                {
                    Clients clients = listViewClients.SelectedItems[0].Tag as Clients;
                    Program.wfbe.Clients.Remove(clients);
                    Program.wfbe.SaveChanges();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                ShowClient();
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count==1)
            {
                Clients clients = listViewClients.SelectedItems[0].Tag as Clients;
                textBoxFirstName.Text = clients.FirstName;
                textBoxMiddleName.Text = clients.MiddleName;
                textBoxLastName.Text = clients.LastName;
                textBoxPhone.Text = clients.Phone;
            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
            }
        }
    }
}
