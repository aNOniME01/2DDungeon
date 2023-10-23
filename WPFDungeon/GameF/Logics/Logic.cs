using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Logic
    {
        public static Random rnd = new Random();
        public static double ToPositive(double x) => Math.Sqrt(x*x);
        public static char RotateFaceing90(char facing)
        {
            if (facing == 'T') return 'R';
            else if (facing == 'R') return 'B';
            else if (facing == 'B') return 'L';
            else return 'T';
        }
        public static char RandomFaceing()
        {
            char dir;
            int rand = rnd.Next(0,5);
            if (rand == 1) dir = 'T';
            else if (rand == 2) dir = 'B';
            else if (rand == 3) dir = 'L';
            else dir = 'R';

            return dir;
        }
        public static char RotateFaceingWithRoom(char roomFaceing,char elementFaceing)
        {
            char newFaceing = elementFaceing;
            if (roomFaceing == 'B')
            {
                if (elementFaceing == 'T') newFaceing = 'B';
                else if (elementFaceing == 'B') newFaceing = 'T';
                else if (elementFaceing == 'L') newFaceing = 'R';
                else newFaceing = 'L';
            }
            else if (roomFaceing == 'L')
            {
                if (elementFaceing == 'T') newFaceing = 'L';
                else if (elementFaceing == 'B') newFaceing = 'R';
                else if (elementFaceing == 'L') newFaceing = 'B';
                else newFaceing = 'T';
            }
            else if (roomFaceing == 'R')
            {
                if (elementFaceing == 'T') newFaceing = 'R';
                else if (elementFaceing == 'B') newFaceing = 'L';
                else if (elementFaceing == 'L') newFaceing = 'T';
                else newFaceing = 'B';
            }
            return newFaceing;
        }
    }
}
