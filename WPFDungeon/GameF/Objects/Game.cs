using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFDungeon
{
    internal class Game
    {
        public List<Room> Rooms { get; private set; }
        public Player Player { get; private set; }
        public Canvas GCanvas { get; private set; }
        public Game(Canvas canvas)
        {
            Rooms = new List<Room>();
            Rooms.Add(new Room("R1"));

            Player = new Player();

            GCanvas = canvas;
        }
    }
}
