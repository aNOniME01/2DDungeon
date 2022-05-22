using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFDungeon
{
    class Hallway
    {
        public Door D1 { get; private set; }
        public Door D2 { get; private set; }
        public IBody Body { get; private set; }
        public int Id { get; private set; }
        public int DoorId { get; private set; }
        public Hallway(Door d1,double hallwayLength, int hallwayId, int doorId)
        {
            D1 = new Door(d1);
            D2 = new Door(d1);

            Id = hallwayId;
            DoorId = doorId;

            D2.ModifyLocation(hallwayLength);

            Body = new HallwayBody(D1, D2);
            DoorId = doorId;
        }
        public void ChangeLocRot(Door d1, double hallwayLength, double[] location)
        {
            D1 = new Door(d1);
            D2 = new Door(d1);

            D2.ModifyLocation(hallwayLength);
            ToRoomLoc(location);
            (Body as HallwayBody).RefreshLocRot(D1,D2);
        }
        public void ToRoomLoc(double[] roomLoc)
        {
            D1.ToRoomLocation(roomLoc);
            D2.ToRoomLocation(roomLoc);
            Render.RefreshElement(Body.Mesh, D1.Location);
            (Body as HallwayBody).MoveHitbox();
        }
    }
}
