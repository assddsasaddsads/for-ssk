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
    public partial class FPosition : Form
    {
        public FPosition()
        {
            InitializeComponent();
            ShowPosition();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Position position = new Position();
            position.Name = textBoxName.Text;
            position.Salary = textBoxSalary.Text;
            Program.wfbe.Position.Add(position);
            Program.wfbe.SaveChanges();
            ShowPosition();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewPosition.SelectedItems.Count == 1)
            {
                Position position = listViewPosition.SelectedItems[0].Tag as Position;
                position.Name = textBoxName.Text;
                position.Salary = textBoxSalary.Text;
                Program.wfbe.SaveChanges();
                ShowPosition();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPosition.SelectedItems.Count == 1)
                {
                    Position position = listViewPosition.SelectedItems[0].Tag as Position;
                    Program.wfbe.Position.Remove(position);
                    Program.wfbe.SaveChanges();
                    ShowPosition();
                }
                textBoxName.Text = "";
                textBoxSalary.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPosition.SelectedItems.Count == 1)
            {
                Position position = listViewPosition.SelectedItems[0].Tag as Position;
                textBoxName.Text = position.Name;
                textBoxSalary.Text = position.Salary;
            }
            else
            {
                textBoxName.Text = "";
                textBoxSalary.Text = "";
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
        void ShowPosition()
        {
            listViewPosition.Items.Clear();
            foreach (Position position in Program.wfbe.Position)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    position.Id.ToString(), position.Name, position.Salary
                });
                item.Tag = position;
                listViewPosition.Items.Add(item);
            }
            listViewPosition.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
