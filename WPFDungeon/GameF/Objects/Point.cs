using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Point : IEntity
    {
        public double[] Location { get; private set; }

        public Direction Facing { get; private set; }

        public IBody Body { get; private set; }
        public int RoomId { get; private set; }
        public Point(double y, double x,int roomId)
        {
            Facing = Direction.Top;
            Location = new double[] { y, x };
            RoomId = roomId;

            Body = new PointBody(Location);
        }
        public void ChangeLocationBy(double y, double x)
        {
            Location[0] += y;
            Location[1] += x;

            Render.RefreshEntity(this);

            (Body as PointBody).MoveHitbox();
        }
        public void GoTo(double[] location)
        {
            Location[0] = location[0];
            Location[1] = location[1];

            Render.RefreshEntity(this);

            (Body as PointBody).MoveHitbox();

        }
        public void FaceTo(Direction faceing)
        {
            Facing = faceing;
            (Body as PointBody).FaceTo(Facing);
        }

    }
}
