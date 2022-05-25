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

        public char Faceing { get; private set; }

        public IBody Body { get; private set; }
        public int RoomId { get; private set; }
        public Point(double y, double x,int roomId)
        {
            Faceing = 'T';
            Location = new double[] { y, x };
            RoomId = roomId;

            Body = new PointBody(Location);
        }
        public void ToRoomLoc(double[] roomLocation)
        {
            Location[0] += roomLocation[0];
            Location[1] += roomLocation[1];

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
        public void FaceTo(char faceing)
        {
            Faceing = faceing;
            (Body as PointBody).FaceTo(Faceing);
        }

    }
}
