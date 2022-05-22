﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal interface IEntity
    {
        public double[] Location { get; }
        public char Faceing { get; }
        public IBody Body { get; }
        public void ToRoomLoc(double[] roomLocation){}
        public void GoTo(double[] location){}
        public void FaceTo(char faceing){}

    }
}
