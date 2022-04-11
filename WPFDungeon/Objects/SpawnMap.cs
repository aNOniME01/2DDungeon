using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class SpawnMap
    {
        public List<Shooter> Shooters { get; private set; }
        public List<Swifter> Swifters { get; private set; }
        //point list
        //ammo list
        //ability list
        public SpawnMap()
        {
            Shooters = new List<Shooter>();
            Swifters = new List<Swifter>();
        }
        public void AddShooter(double yLoc,double xLoc,int turretNum, char faceing)
        {
            Shooters.Add(new Shooter(yLoc, xLoc, turretNum, faceing));
        }
        public void DeleteShooter(Shooter shooter)
        {
            Shooters.Remove(shooter);
        }
        public void AddSwifter(double yLoc,double xLoc, char faceing)
        {
            Swifters.Add(new Swifter(yLoc, xLoc, faceing));
        }
        public void DeleteSwifter(Swifter swifter)
        {
            Swifters.Remove(swifter);
        }
    }
}
