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
        public Rectangle Hitbox { get; private set; }
        public Bullet(string tag,double xLoc,double yLoc)
        {
            Tag = tag;

            Location = new double[2];
            Location[0] = xLoc;
            Location[1] = yLoc;

            Hitbox = new Rectangle();
            Hitbox.Width = 2;
            Hitbox.Height = 5;
            Hitbox.Stroke = Brushes.Black;
        }
        public void Navigate(double x, double y)
        {
            Location[0] += x;
            Location[1] += y;
        }
    }
}
