using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Wall
    {
        public double[] Location { get; private set; }
        public Wall(double[] location)
        {
            Location = new double[2] { location[0], location[1] };
        }
        public Wall(double x,double y)
        {
            Location = new double[2] {x,y};
        }
    }
}
