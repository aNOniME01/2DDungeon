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
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public Bullet(string tag, double yLoc, double xLoc, char faceing)
        {
            Tag = tag;

            Location = new double[2];
            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;

            Mesh = new Rectangle();
            if (Faceing == 'T' || Faceing == 'B')
            {
                Mesh.Width = 2;
                Mesh.Height = 5;
            }
            else
            {
                Mesh.Width = 5;
                Mesh.Height = 2;
            }
            Mesh.Stroke = Brushes.Black;


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

            Hitbox = new Rect(Location[1], Location[0], Mesh.Width, Mesh.Height);
        }
    }
}
