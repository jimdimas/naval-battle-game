using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavmaxiaGame
{
    public class BattlefieldPoint : Button   //This class extends the button control , because we need buttons as points in our battlefields
    {                                   //These buttons must have a certain behavior that suits the naval battle game , for instance knowing
                                        //if a button-point is taken from a warship , being able to be 'clicked' by the computer , having a click
                                        //event when we click the enemy battlefield's points etc.
        public Boolean isTaken { get; set; }        //Is set to true when a ButtonPoint belongs to a Warship
        public Boolean isHit = false;
        public Warship parentWarship { get; set; }   //If a button-point is taken by a ship , it must know which ship it belongs to
        public Battlefield parentBattlefield{get; set;}
        public BattlefieldPoint()
        {
            this.Click += new EventHandler(ButtonClick);
            this.Width = 30;
            this.Height = 30;
            this.BackColor = Color.LightGray;
            this.Font = new Font("Calibri", 16);
            this.ForeColor = Color.Black;
        }

        public void ButtonClick(object sender, EventArgs e)     //This method is an event of clicking an enemy battlefield button
        {                                                       //Because those buttonPoints are the ones we can click
            BattlefieldPoint currentPoint = ((BattlefieldPoint)sender);
            changePointAppearance(currentPoint);
            currentPoint.parentBattlefield.parentForm.playerTries++;
            if (this.isTaken)
            {
                parentWarship.AttemptRemoval(currentPoint);     //Since the ButtonPoint is taken by a ship, attempt to remove it from the ship it belongs to.
            }
            currentPoint.parentBattlefield.parentForm.Enabled = false;
            currentPoint.parentBattlefield.parentForm.ComputerMove();
        }

        public void ComputerButtonPick()    //The battlefieldPoints of our battlefield are disabled so we cant click them
        {                                   //The code will 'pick' a battlefieldPoint of ours by changing its isHit value
                                            //And its appearance.It will also remove it from the warship it belongs to.
            changePointAppearance(this);
            if (this.isTaken)               
            {
                parentWarship.AttemptRemoval(this);  //Since the BattlefieldPoint is taken by a ship, attempt to remove it from the ship it belongs to.
                return;
            }
        }

        private void changePointAppearance(BattlefieldPoint point)    //When a point is hit , this method changes it's appearance
        {
            if (point.isTaken)
            {
                point.Text = "X";
                point.ForeColor = Color.Red;
                point.isHit = true;
                point.Enabled = false;
            }
            else
            {
                point.Text = "-";
                point.ForeColor = Color.Green;
                point.isHit = true;
                point.Enabled = false;
            }
        }
    }
}
