using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class IEntity
    {
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }//it can have multiple turrets
        public ITextures Body { get; private set; }//it can have multiple turrets
    }
}
