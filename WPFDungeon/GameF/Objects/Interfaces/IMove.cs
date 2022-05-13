using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDungeon
{
    internal interface IMove
    {
        public Rect MTop { get; }
        public Rect MBottom { get; }
        public Rect MLeft { get;}
        public Rect MRight { get;}
    }
}
