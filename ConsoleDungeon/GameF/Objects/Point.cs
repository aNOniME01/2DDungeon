using ConsoleDungeon.GameF.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Point : IEntity
    {
        public double[] Location { get; private set; }

        public double[] PrevLocation { get; private set; }
        public Point(double x, double y)
        {
            Location = new double[2];
            SetLocation(x, y);
            PrevLocation = new double[2] {x,y};
        }
        public void SetLocation(double x,double y)
        {
            Location[0] = x;
            Location[1] = y;
        }
    }
}
