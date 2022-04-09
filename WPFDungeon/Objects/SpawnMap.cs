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
        //swifter list
        //point list
        //ammo list
        //ability list
        public SpawnMap()
        {
            Shooters = new List<Shooter>();
        }
        public void AddShooter(double yLoc,double xLoc,int turretNum, char faceing)
        {
            Shooters.Add(new Shooter(yLoc, xLoc, turretNum, faceing));
        }
        public void DeleteShooter(Shooter shooter)
        {
            Shooters.Remove(shooter);
        }
    }
}
