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
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        public GameForm(String playerUsernameParam)
        {
            InitializeComponent();
            playerUsername = playerUsernameParam; 
        }

        public String playerUsername { get; }
        public static int rowSize = 10;
        public static int colSize = 10;
        public int playerTries=0;
        public Point startPoint = new Point(60, 90);
        public Point enemyStartPoint = new Point(480, 90);
        public Random rand = new Random();
        public Battlefield friendlyBattlefield = new Battlefield(false,10,10);
        public Battlefield enemyBattlefield = new Battlefield(true,10,10);
        public string[] letters = { "A", "B", "C", "D", "E", "F", "G" ,"H","I","J"};
        private int gameTimeSecs=0;
        DBHelper dbHelp = new DBHelper();
        List<BattlefieldPoint> pointsLeftToHit = new List<BattlefieldPoint>();      
        private void Form1_Load(object sender, EventArgs e)
        {
            friendlyBattlefield.parentForm = this;
            enemyBattlefield.parentForm = this;
            setBattlefield(startPoint,ref  friendlyBattlefield);
            setBattlefield(enemyStartPoint,ref enemyBattlefield);
            for (int i=0; i<rowSize; i++)     
            {
                for (int j=0; j<colSize; j++)
                {
                    pointsLeftToHit.Add(friendlyBattlefield.battlefieldPoints[i, j]);
                }
            }
            
            friendlyBattlefield.setWarship(5);  //Name is set based on size given
            friendlyBattlefield.setWarship(4);
            friendlyBattlefield.setWarship(3);
            friendlyBattlefield.setWarship(2);
            enemyBattlefield.setWarship(5);
            enemyBattlefield.setWarship(4);
            enemyBattlefield.setWarship(3);
            enemyBattlefield.setWarship(2);
            GameTimer.Enabled = true;

        }

        private void setBattlefield(Point startPoint,ref Battlefield newBattlefield)
        {
            Point paddingPoint = new Point(startPoint.X, startPoint.Y);
            BattlefieldPoint tempBattlefieldPoint = new BattlefieldPoint();
            
            for (int i=0; i<colSize; i++)
            {
                CustomLabel tempLabel = new CustomLabel(new Point(paddingPoint.X + i * 30 + 3, paddingPoint.Y - 30), (i + 1).ToString());
                this.Controls.Add(tempLabel);
            }
            for (int i = 0; i < newBattlefield.rowSize; i++)
            {
                CustomLabel tempLabel = new CustomLabel(new Point(paddingPoint.X - 30, paddingPoint.Y + 5), letters[i]);
                this.Controls.Add(tempLabel);
                for (int j = 0; j < newBattlefield.colSize; j++)
                {
                    tempBattlefieldPoint = new BattlefieldPoint();
                    tempBattlefieldPoint.Location = new Point(paddingPoint.X + 30 * j, paddingPoint.Y);
                    tempBattlefieldPoint.parentBattlefield = newBattlefield;
                    tempBattlefieldPoint.Enabled = newBattlefield.isEnemyBatt;    //Only the enemy battlefield's buttons can be clicked.
                    newBattlefield.battlefieldPoints[i, j] = tempBattlefieldPoint;
                    this.Controls.Add(newBattlefield.battlefieldPoints[i, j]);
                }
                paddingPoint.Y += 30;
            }
        }
        public void GameEnd(bool calledByEnemy)
        {
            GameTimer.Enabled = false;
            string gameTimeFormatted = TimeSpan.FromSeconds(gameTimeSecs).ToString(@"hh\:mm\:ss");
            
            if (calledByEnemy)  //If the function is called by the enemy battlefield class , that means all of it's ships were destroyed first
            {
                dbHelp.insertResult(playerUsername, playerTries.ToString(), gameTimeFormatted, "Won");
                MessageBox.Show($"You won!Moves: {playerTries.ToString()},Time: {gameTimeFormatted}");
                this.Close();
            }
            else 
            {
                dbHelp.insertResult(playerUsername, playerTries.ToString(), gameTimeFormatted, "Lost");
                MessageBox.Show($"You Lost!Moves: {playerTries.ToString()},Time: {gameTimeFormatted}");
                this.Close();
            }

        }

        public void ComputerMove()  //Picks one buttonpoint of the pointsLeftToHit list and executed the computer pick method on it 
        {                           //then its deleted so it can't be picked ever again
            int randomIndex = rand.Next(pointsLeftToHit.Count);           
            pointsLeftToHit[randomIndex].ComputerButtonPick();
            pointsLeftToHit.RemoveAt(randomIndex);
            this.Enabled = true;

        }

        public void SunkMessage(String message,bool isEnemyShip)
        {
            if (isEnemyShip)
            {
                MessageBox.Show($"Enemy {message} has been sunk!");
            }
            else
            {
                MessageBox.Show($"Your {message} has been sunk!");
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            gameTimeSecs++;
        }
    }
}
