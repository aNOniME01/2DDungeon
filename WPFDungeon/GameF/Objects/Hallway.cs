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
        public IBody Body { get; set; }
        public Hallway(Door d1,double hallwayLength)
        {
            D1 = new Door(d1);
            D2 = new Door(d1);

            D2.ModifyLocation(hallwayLength);

            Body = new HallwayBody(D1, D2);
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
