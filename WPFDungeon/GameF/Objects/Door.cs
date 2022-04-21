using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Door
    {
        public double[] L1 { get; private set; }
        public double[] L2 { get; private set; }
        public char Faceing { get; private set; }
        public Door(double x1,double y1,double x2,double y2,char faceing)
        {
            L1 = new double[2];
            L2 = new double[2];

            L1[0] = x1;
            L1[1] = y1;

            L2[0] = x2;
            L2[1] = y2;

            Faceing = faceing;
        }
    }
}
