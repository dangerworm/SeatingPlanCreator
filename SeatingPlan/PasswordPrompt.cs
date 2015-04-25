using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SeatingPlanCreator
{
    public partial class PasswordPrompt : Form
    {
        private bool newUser;

        public PasswordPrompt(ClassRoomSetup parent, List<User> users, bool newUser)
        {
            InitializeComponent();
            Tag = parent;

            this.newUser = newUser;

            if (newUser)
            {
                Text = "Create User Account";
                cmbUsernames.Items.Add(Environment.UserName);
            }
            else
            {
                foreach (var user in users)
                {
                    cmbUsernames.Items.Add(user.Username);
                }
            }

            cmbUsernames.SelectedIndex = 0;
        }

        public void ClearPassword()
        {
            txtPassword.Text = "";
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ((ClassRoomSetup)Tag).DoLogin(newUser, (string)cmbUsernames.SelectedItem, txtPassword.Text);
                e.Handled = true;
            }
        }
    }
}
