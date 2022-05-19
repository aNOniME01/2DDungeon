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
        public void ToRoomLoc(double[] roomLocation)
        {
            Location[0] += roomLocation[0];
            Location[1] += roomLocation[1];
            Render.RefreshEntity(this);
        }

    }
}
