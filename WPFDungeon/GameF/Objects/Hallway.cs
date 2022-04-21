using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    class Hallway
    {
        public Door D1 { get; private set; }
        public Door D2 { get; private set; }
        public IBody Body { get; set; }
        public Hallway(Door d1,double hallwayLength)
        {
            D1 = d1;
            D2 = new Door(d1);

            if (D2.Faceing == 'T') D2.L1[0] -= hallwayLength;
            else if (D2.Faceing == 'B') D2.L1[0] += hallwayLength;
            else if (D2.Faceing == 'L') D2.L1[1] -= hallwayLength;
            else D2.L1[1] += hallwayLength;

            Body = new HallwayBody(D1, D2);
        }
    }
}
