using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Logic
    {
        public static double ToPositive(double x) => Math.Sqrt(x*x);
        public static char FaceingRotate90(char faceing)
        {
            if (faceing == 'T') return 'R';
            else if (faceing == 'R') return 'B';
            else if (faceing == 'B') return 'L';
            else return 'T';
        }
    }
}
