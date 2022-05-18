using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class Bullet:IEntity
    {
        public string Tag { get; private set; }
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }
        public IBody Body { get; set; }
        public Bullet(string tag, double yLoc, double xLoc, char faceing)
        {
            Tag = tag;

            Location = new double[2];
            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;
            Body = new BulletBody(Faceing,Location);
        }
        public void Navigate()
        {
            if (Faceing == 'T')
            {
                Location[0] -= 5;
            }
            else if (Faceing == 'B')
            {
                Location[0] += 5;
            }
            else if (Faceing == 'L')
            {
                Location[1] -= 5;
            }
            else if (Faceing == 'R')
            {
                Location[1] += 5;
            }
            (Body as BulletBody).SetHitboxLocation();
        }
    }
}
