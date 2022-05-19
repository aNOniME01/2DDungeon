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
        public List<Hallway> Hallways { get; private set; }
        public Player Player { get; private set; }
        public Canvas GCanvas { get; private set; }
        public Game(Canvas canvas)
        {
            Rooms = new List<Room>();
            Rooms.Add(new Room("R1"));

            Hallways = new List<Hallway>();
            foreach (Room room in Rooms)
            {
                foreach (Door door in room.Doors)
                {
                    Hallways.Add(new Hallway(door,40));
                }
            }

            Player = new Player();

            GCanvas = canvas;
        }
        public void AddRoom(string roomName, Door hallwayEnd)
        {
            Room newRoom = new Room(roomName);
            Door enterance = newRoom.SearchDoorFaceingOpposit(hallwayEnd.Faceing);
            foreach (Door door in newRoom.Doors)
            {
                if (door != enterance)
                {
                    Hallways.Add(new Hallway(door, 40));
                }
            }
            newRoom.ToDoorLoc(hallwayEnd,enterance);
            Rooms.Add(newRoom);
        }
    }
}
