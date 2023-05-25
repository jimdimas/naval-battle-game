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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        DBHelper dbHelp = new DBHelper();
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if(dbHelp.attemptLogin(UsernameTextbox.Text,PasswordTextbox.Text))
            {
                new UserForm(UsernameTextbox.Text).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Login!");
            }
        }
    }
}
