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
        public Direction Facing { get; private set; }
        public IBody Body { get; set; }

        public Bullet(string tag, double yLoc, double xLoc, Direction faceing)
        {
            Tag = tag;

            Location = new double[2];
            Location[0] = yLoc;
            Location[1] = xLoc;

            Facing = faceing;
            Body = new BulletBody(Facing,Location);
        }

        public void Navigate()
        {
            if (Facing == Direction.Top) Location[0] -= 5;
            else if (Facing == Direction.Bottom) Location[0] += 5;
            else if (Facing == Direction.Left) Location[1] -= 5;
            else if (Facing == Direction.Right) Location[1] += 5;
            Render.RefreshEntity(this);

            (Body as BulletBody).SetHitboxLocation();
        }

    }
}
