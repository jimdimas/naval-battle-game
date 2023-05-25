using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavmaxiaGame
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        public UserForm(String playerNameParam)
        {
            playerName = playerNameParam;
            InitializeComponent();
        }

        public String playerName { get; }
        DBHelper dbHelp = new DBHelper();
        private void UserForm_Load(object sender, EventArgs e)
        {
            WelcomeLabel.Text = "Welcome " + playerName;
        }

        private void PersonalResultsButton_Click(object sender, EventArgs e)
        {
            ResultTextbox.Text = "";
            List<String> results = dbHelp.playerResults(playerName);
            if(results.Count!=0)
            {
                foreach(String result in results)
                {
                    ResultTextbox.Text += result;
                }
            }
            else
            {
                MessageBox.Show("No games played yet!");
            }
        }

        private void SeeResultsSearch_Click(object sender, EventArgs e)
        {
            ResultTextbox.Text = "";
            List<String> results = dbHelp.playerResults(UsernameTextbox.Text);
            if (results.Count != 0)
            {
                foreach (String result in results)
                {
                    ResultTextbox.Text += result;
                }
            }
            else
            {
                MessageBox.Show("No games played yet or user doesn't exist!");
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            new GameForm(playerName).Show();
        }
    }
}
