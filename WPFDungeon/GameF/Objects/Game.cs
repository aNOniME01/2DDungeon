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
            Rooms.Add(new Room("R1", Rooms.Count));

            Hallways = new List<Hallway>();

            foreach (Door door in Rooms[0].Doors)
            {
                Hallways.Add(new Hallway(door, 40, Rooms.Count - 1));
            }



            Player = new Player();

            GCanvas = canvas;
        }
        public void AddRoom(string roomName, Door hallwayEnd)
        {
            Room newRoom = new Room(roomName,Rooms.Count);
            Door enterance = newRoom.SearchDoorFaceingOpposit(hallwayEnd.Faceing);
            
            //Add halway to door locations except the door where it connects to the mother Room
            foreach (Door door in newRoom.Doors)
            {
                if (door != enterance)
                {
                    Hallways.Add(new Hallway(door, 40,Rooms.Count));
                }
            }
            newRoom.ToDoorLoc(hallwayEnd,enterance);

            //Sets hallway location
            foreach (Hallway hallway in Hallways)
            {
                if (hallway.Id == newRoom.Id)
                {
                    hallway.ToRoomLoc(newRoom.Location);
                }
            }

            Rooms.Add(newRoom);
            Render.AddRoomToCanvas(newRoom);
        }
    }
}
