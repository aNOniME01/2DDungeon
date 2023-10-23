using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class SpawnMap
    {
        public int RoomId { get; private set; }
        public List<IEntity> Shooters { get; private set; }
        public List<IEntity> Swifters { get; private set; }
        public List<IEntity> Points { get; private set; }
        public Portal? Portal { get; private set; }
        //point list
        //ammo list
        //ability list
        public SpawnMap(int roomId)
        {

            RoomId = roomId;
            Shooters = new List<IEntity>();
            Swifters = new List<IEntity>();
            Points = new List<IEntity>();
            this.Portal = null;
        }
        public void AddShooter(double yLoc,double xLoc,int turretNum, Direction facing)
        {
            Shooters.Add(new Shooter(yLoc, xLoc, turretNum, facing, RoomId));
        }
        public void DeleteShooter(Shooter shooter)
        {
            Shooters.Remove(shooter);
        }
        public void AddSwifter(double yLoc,double xLoc, Direction facing)
        {
            Swifters.Add(new Swifter(yLoc, xLoc, facing, RoomId));
        }
        public void AddPoint(double yLoc,double xLoc)
        {
            Points.Add(new Point(yLoc, xLoc, RoomId));
        }
        public void DeleteSwifter(Swifter swifter)
        {
            Swifters.Remove(swifter);
        }
        public void AddPortal(double yLoc, double xLoc, Direction facing)
        {
            this.Portal = new Portal(yLoc, xLoc, RoomId);
        }
        public void DeletePortal()
        {
            Render.RemoveEntity(Portal);
            Portal = null;
        }
    }
}
