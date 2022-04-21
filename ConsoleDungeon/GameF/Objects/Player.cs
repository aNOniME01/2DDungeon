using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Player : IEntity
    {
        public double[] Location { get; private set; }
        public double[] PrevLocation { get; private set; }
        public Player(char[,] map)
        {
            Location = new double[2];
            Location[0] = map.GetLength(0) / 2;
            Location[1] = map.GetLength(1) / 2;

            PrevLocation = new double[2];
        }
        public void SetEntityPrevPosTo(double[] ePrevPos)
        {
            PrevLocation[0] = ePrevPos[0];
            PrevLocation[1] = ePrevPos[1];
        }
        public void AddToEntityPos(int x, int y)
        {
            Location[0] += x;
            Location[1] += y;
        }
        public void DissapearEntity()
        {
            Location[0] = -1;
            Location[1] = -1;
        }
    }
}
