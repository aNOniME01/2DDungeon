using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDungeon
{
    internal class MoveHitboxes
    {
        public Rect Up { get; private set; }
        public Rect Down { get; private set; }
        public Rect Right { get; private set; }
        public Rect Left { get; private set; }
        public MoveHitboxes(double width,double height)
        {
            Up = new Rect(new Point(0,height),new Point(width,0));
            Down = new Rect(new Point(0,height),new Point(width,0));
            Right = new Rect(new Point(0,height),new Point(width,0));
            Left = new Rect(new Point(0,height),new Point(width,0));
        }
    }
}
