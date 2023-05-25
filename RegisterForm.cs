using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavmaxiaGame
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        DBHelper dbHelp = new DBHelper();
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (dbHelp.attemptRegister(UsernameTextbox.Text,PasswordTextbox.Text))
            {
                new UserForm(UsernameTextbox.Text).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username already in use!");
            }
        }
    }
}
