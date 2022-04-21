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
    class BulletBody : IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public BulletBody(char faceing,double yLoc,double xLoc)
        {

            Mesh = new Rectangle();
            if (faceing == 'T' || faceing == 'B')
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
            SetHitboxLocation(yLoc, xLoc);
        }
        public void SetHitboxLocation(double yLoc, double xLoc)
        {
            Hitbox = new Rect(xLoc, yLoc, Mesh.Width, Mesh.Height);
        }
    }
}
