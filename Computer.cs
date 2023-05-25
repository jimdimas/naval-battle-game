using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavmaxiaGame
{
    class Computer
    {
        Dictionary<string, Warship> EnemyShipsLeft = new Dictionary<string, Warship>();
        public Computer(List<Warship> enemyWarships)
        {
            foreach (Warship enemyWarship in enemyWarships)
            {
                EnemyShipsLeft[enemyWarship.shipName] = enemyWarship;
            }
        }
    }

    class State
    {

    }
}
