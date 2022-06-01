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
        public int RoomId { get; private set; }
        public Portal(double yLoc,double xLoc,int roomId)
        {
            RoomId = roomId;

            Location = new double[2];
            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = 'T';

            Body = new PortalBody(Location);
        }
        public void ChangeLocationBy(double y, double x)
        {
            Location[0] += y;
            Location[1] += x;

            Render.RefreshEntity(this);

            (Body as PortalBody).MoveHitbox();
        }
        public void FaceTo(char faceing)
        {
            Faceing = faceing;
            (Body as PortalBody).FaceTo(Faceing);
        }

        public void ToRoomCenter(Room room)
        {
            Location[0] = room.Location[0] + room.Body.Mesh.Height / 2;
            Location[1] = room.Location[1] + room.Body.Mesh.Width / 2;

            Render.RefreshEntity(this);

            (Body as PortalBody).MoveHitbox();

        }
    }
}
