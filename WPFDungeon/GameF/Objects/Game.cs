using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFDungeon
{
    internal class Game
    {
        public List<Room> Rooms { get; private set; }
        public List<Hallway> Hallways { get; private set; }
        public Player Player { get; private set; }
        public Canvas GCanvas { get; private set; }
        public List<Rect> BarrierList { get; private set; }
        public Game(Canvas canvas)
        {
            Rooms = new List<Room>();
            Rooms.Add(new Room("R1", Rooms.Count));

            Hallways = new List<Hallway>();

            foreach (Door door in Rooms[0].Doors)
            {
                Hallways.Add(new Hallway(door, 40, Rooms.Count - 1, door.Id));
            }



            Player = new Player();

            GCanvas = canvas;

            BarrierList = new List<Rect>();

            BarrierList.Add(new Rect(0, 0, 465, 20));
            BarrierList.Add(new Rect(0, 442, 465, 20));
            BarrierList.Add(new Rect(0, 20, 20, 422));
            BarrierList.Add(new Rect(465, 0, 20, 462));

        }
        public bool AddRoom(string roomName, Hallway entHallway)
        {
            Room newRoom = new Room(roomName,Rooms.Count);
            Door enterance = null;
            int count = 0;

            while (count < 4 && enterance == null)
            {
                enterance = newRoom.SearchDoorFaceingOpposit(entHallway.D2.Faceing);
                if (enterance != null)
                {
                    newRoom.ToDoorLoc(entHallway.D2, enterance);
                    bool outside = false;

                    //Checks if the room intersect with the edge of the window
                    foreach (Rect barrier in BarrierList)
                    {
                        if (newRoom.Body.Hitbox.IntersectsWith(barrier)) outside = true;
                    }
                    foreach (Room room in Rooms)
                    {
                        if (newRoom.Body.Hitbox.IntersectsWith(room.Body.Hitbox)) outside = true;
                    }
                    if (outside) enterance = null;
                }

                if (enterance == null)
                {
                    ChangeRoomFaceing(newRoom,Logic.RotateFaceing90(newRoom.Faceing)); 
                }
                count++;
            }

            if (enterance != null)
            {

                //Add halway to door locations except the door where it connects to the mother Room
                foreach (Door door in newRoom.Doors)
                {
                    if (door != enterance)
                    {
                        Hallways.Add(new Hallway(door, 40, Rooms.Count, door.Id));
                    }
                }

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

                //Sets the hallway status to connected
                entHallway.Connected();

                return true;
            }
            return false;
        }
        public void ChangeRoomFaceing(Room room,char faceing) 
        {
            room.ChangeFaceing(faceing);

            foreach (Door door in room.Doors)
            {
                foreach (Hallway hallway in Hallways)
                {
                    if (hallway.Id == room.Id && hallway.DoorId == door.Id)
                    {
                        hallway.ChangeLocRot(door, 40,room.Location);
                    }
                }
            }
        }
        public void ChangeRoomLocation(Room room, double y, double x)
        {
            room.ChangeLocation(y, x);

            foreach (Hallway hallway in Hallways)
            {
                hallway.ToRoomLoc(room.Location);
            }
        }
    }
}
