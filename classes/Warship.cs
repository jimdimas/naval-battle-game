using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavmaxiaGame
{
    public class Warship
    {
        public List<BattlefieldPoint> warshipPoints { get; set; }
        public String shipName { get; set; }
        public Boolean isHorizontal { get; }
        public int rowPos { get; }
        public int columnPos { get; }
        public int size { get; set; }

        public Battlefield battlefieldUpper { get; set; }
        public Warship(List<BattlefieldPoint> warshipPointsParam,Battlefield battlefieldUpperParameter,int sizeParam)
        {
            warshipPoints = warshipPointsParam;
            battlefieldUpper = battlefieldUpperParameter;
            switch(sizeParam)
            {
                case 2:
                    this.shipName = "Submarine";
                    break;
                case 3:
                    this.shipName = "Fighter";
                    break;
                case 4:
                    this.shipName = "Antitorpedo";
                    break;
                case 5:
                    this.shipName = "Aircraft Carrier";
                    break;
            }
            size = sizeParam;
            foreach (BattlefieldPoint tempPoint in warshipPointsParam)
            {
                tempPoint.parentWarship = this;      //Setting this so a buttonPoint knows to which warship it belongs to.
            }
        }

        public void AttemptRemoval(BattlefieldPoint removedButton)
        {
            if (warshipPoints.Any())
            {
                warshipPoints.Remove(removedButton);
            }

            if (!warshipPoints.Any())   //Checking the reverse statement here because if warshipPoints has only one buttonPoint left , 
            {                           //Then this if command will remove the ship from the battlefield after we remove the last point in the first if.
                battlefieldUpper.AttemptRemoval(this);
                if (battlefieldUpper.remainingShips.Any())
                {
                    battlefieldUpper.parentForm.SunkMessage(this.shipName, this.battlefieldUpper.isEnemyBatt);   //We only get here if every point of a ship has been attacked                                                                                            
                }
            }
        }

        public Warship()
        { }
    }
}
