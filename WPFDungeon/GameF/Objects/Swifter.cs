using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Swifter:IEntity
    {
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }//it can have multiple turrets
        public IBody Body { get; private set; }//it can have multiple turrets
        public Swifter(double yLoc,double xLoc,char faceing)
        {
            Location = new double[2];

            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;

            Body = new SwifterBody(20,10,Location[0],Location[1],Faceing);
        }
        public void Navigate(double width, double height)
        {
            double speed = 2.5;
            if (Faceing == 'T')
            {
                if (Location[0] - speed < 0) Faceing = 'B';
                else Location[0] -= speed;
            }
            else if (Faceing == 'B')
            {
                if (Location[0] + speed > height) Faceing = 'T';
                else Location[0] += speed;
            }
            else if (Faceing == 'L')
            {
                if (Location[1] - speed < 0) Faceing = 'R';
                else Location[1] -= speed;
            }
            else
            {
                if (Location[1] + speed > width) Faceing = 'L';
                else Location[1] += speed;
            }
            Body.FaceTo(Faceing);
            (Body as SwifterBody).MoveHitbox(Location);
        }
    }
}
