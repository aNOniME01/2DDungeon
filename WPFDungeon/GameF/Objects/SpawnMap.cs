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
        public Portal Portal { get; private set; }
        //point list
        //ammo list
        //ability list
        public SpawnMap(int roomId)
        {

            RoomId = roomId;
            Shooters = new List<IEntity>();
            Swifters = new List<IEntity>();
            this.Portal = null;
        }
        public void AddShooter(double yLoc,double xLoc,int turretNum, char faceing)
        {
            Shooters.Add(new Shooter(yLoc, xLoc, turretNum, faceing, RoomId));
        }
        public void DeleteShooter(Shooter shooter)
        {
            Shooters.Remove(shooter);
        }
        public void AddSwifter(double yLoc,double xLoc, char faceing)
        {
            Swifters.Add(new Swifter(yLoc, xLoc, faceing, RoomId));
        }
        public void DeleteSwifter(Swifter swifter)
        {
            Swifters.Remove(swifter);
        }
        public void AddPortal(double yLoc, double xLoc, char faceing)
        {
            this.Portal = new Portal(yLoc, xLoc, RoomId);
        }
    }
}
