using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavmaxiaGame
{
    public class Battlefield
    {
        public Point initializationPoint { get; }
        public Boolean isEnemyBatt { get; }
        public BattlefieldPoint[,] battlefieldPoints = new BattlefieldPoint[10, 10];
        
        public List<Warship> remainingShips =new List<Warship>();
        public GameForm parentForm;
        public int rowSize { get; }
        public int colSize { get; }
        public Battlefield(Boolean isEnemyBattlefield,int rowSizeParameter,int colSizeParameter)
        {
            isEnemyBatt = isEnemyBattlefield;
            rowSize = rowSizeParameter;
            colSize = colSizeParameter;
        }

        public void AttemptRemoval(Warship removedWarship)
        {
            if (remainingShips.Any())   //If there are ships left , attempt to remove the parameter ship given
            {
                remainingShips.Remove(removedWarship);
            }

            if(!remainingShips.Any())   //If-else is not used because when we remove the last ship of remainingShips , we then have to end the game.
            {
                parentForm.GameEnd(this.isEnemyBatt);    //We get here only if all of a battlefield's ships have been sunk
            }   
        }

        public void setWarship(int shipSize)    //Warship is set to a random horizontal or vertical list of neighboring buttonpoints with the given size
        {
            Random rand = parentForm.rand;
            int initPositionRow;
            int initPositionCol;
            int isHorizontal = rand.Next(2);
            int[] shipCoordinates = new int[2];  //int array that holds the 2 random coordinates of the first point of a ship
            Boolean positionTaken = true;
            BattlefieldPoint tempBattlefieldPoint = new BattlefieldPoint();
            shipCoordinates = positionSetter(shipSize,isHorizontal);
            initPositionRow = shipCoordinates[0];
            initPositionCol = shipCoordinates[1];
            List<BattlefieldPoint> tempBattlefieldPointsList = new List<BattlefieldPoint>();  //List of the points a ship is going to occupy

            while (positionTaken)
            {
                positionTaken = false;
                if (isHorizontal == 1)
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        if (this.battlefieldPoints[initPositionRow + i, initPositionCol].isTaken)   //If there is already a ship point in the corresponding position
                        {
                            shipCoordinates = positionSetter(shipSize,isHorizontal);
                            initPositionRow = shipCoordinates[0];
                            initPositionCol = shipCoordinates[1];
                            positionTaken = true;
                            break;  //If any of the button-points we try to place the ship is taken,calculate a new init position for the ship
                        }           //and check again if all of the button points in the direction we want(horizontally or vertically) are available
                    }
                    if (!positionTaken) //If all the horizontal points from (row,col) up to (row+shipSize,col) are available,create a new ship on these coordinates
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            tempBattlefieldPoint = this.battlefieldPoints[initPositionRow + i, initPositionCol];
                            tempBattlefieldPoint.isTaken = true;
                            if (this.isEnemyBatt)
                            {
                                tempBattlefieldPointsList.Add(tempBattlefieldPoint);   //Enemy battlefield's ships must not be visible
                                continue;
                            }
                            tempBattlefieldPoint.BackColor = Color.Yellow;
                            tempBattlefieldPointsList.Add(tempBattlefieldPoint);
                        }
                        break;
                    }
                }
                else
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        if (this.battlefieldPoints[initPositionRow, initPositionCol + i].isTaken)   //If there is already a ship point in the corresponding position
                        {
                            shipCoordinates = positionSetter(shipSize,isHorizontal);
                            initPositionRow = shipCoordinates[0];
                            initPositionCol = shipCoordinates[1];
                            positionTaken = true;
                            break;  //The same applies if the ship's direction is horizontal
                        }
                    }
                    if (!positionTaken) //If all the horizontal points from (row,col) up to (row,col+shipSize) are available,create a new ship on these coordinates
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            tempBattlefieldPoint = this.battlefieldPoints[initPositionRow, initPositionCol + i];
                            tempBattlefieldPoint.isTaken = true;
                            if (this.isEnemyBatt)
                            {
                                tempBattlefieldPointsList.Add(tempBattlefieldPoint);   //Enemy battlefield's ships must not be visible
                                continue;
                            }
                            tempBattlefieldPoint.BackColor = Color.Yellow;       //Making our warships visible in our battlefield
                            tempBattlefieldPointsList.Add(tempBattlefieldPoint);
                        }
                        break;
                    }
                }
            }

            this.remainingShips.Add(new Warship(tempBattlefieldPointsList, this, shipSize));

        }

        public int[] positionSetter(int shipSize,int isHorizontal)  //Calculates 2 integers that correspond to a position in the battlefield
        {                                                       //The integers are calculated with respect to a ship's size and direction (horizontal,vertical),
            Random rand = parentForm.rand;                       //Because a ship's points must not surpass the boundaries of the battlefieldPoints array
            int[] array = new int[2];
            if (isHorizontal==1)
            {
                array[0] = rand.Next(rowSize - shipSize);   //calculated with respect to row size
                array[1] = rand.Next(colSize);
                return array;
            }
            else
            {
                array[0] = rand.Next(rowSize);
                array[1] = rand.Next(colSize-shipSize);     //calculate with respect to column size
                return array;
            }
        }

    }
}
