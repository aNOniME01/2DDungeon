using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon.GameF.Objects.Interfaces
{
    internal interface IEntity
    {
        public double[] Location { get; }
        public double[] PrevLocation { get; }

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
