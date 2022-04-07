using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    class Game
    {
        public List<Room> Rooms { get; private set; }
        public Player Player { get; private set; }
        public Game(double height,double width)
        {
            Rooms = new List<Room>();
            Rooms.Add(new Room("R1"));

            Player = new Player(height,width);
        }
    }
}
