using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class Bullet
    {
        public string Tag { get; private set; }
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }
        public Rectangle Hitbox { get; private set; }
        public Bullet(string tag,double xLoc,double yLoc,char faceing)
        {
            Tag = tag;

            Location = new double[2];
            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;

            Hitbox = new Rectangle();
            Hitbox.Width = 2;
            Hitbox.Height = 5;
            Hitbox.Stroke = Brushes.Black;
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
        }
    }
}
