using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Portal : IEntity
    {
        public double[] Location { get; private set; }

        public char Faceing { get; private set; }

        public IBody Body { get; private set; }

        public Portal(double yLoc,double xLoc,char faceing)
        {
            Location = new double[2];
            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;

            Body = new PortalBody(15,10,Location[0],Location[1],Faceing);
        }
    }
}
