using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFDungeon
{
    internal class Game
    {
        public bool gameOver { get; private set; }
        public List<Room> Rooms { get; private set; }
        public List<Hallway> Hallways { get; private set; }
        public Player? Player { get; private set; }
        public TextBlock GScore { get; private set; }
        public Canvas GCanvas { get; private set; }
        public Grid GGrid { get; private set; }
        public List<Rect> BarrierList { get; private set; }
        public int Score { get; private set; }
        public Room PortalRoom { get; private set; }
        public Portal? ExitPortal { get; private set; }
        public Game(Grid gGrid)
        {
            gameOver = false;

            Score = 0;

            Rooms = new List<Room>();
            Rooms.Add(new Room("R1", Rooms.Count));
            ExitPortal = null;

            Hallways = new List<Hallway>();

            foreach (Door door in Rooms[0].Doors)
            {
                Hallways.Add(new Hallway(door, 40, Rooms.Count - 1, door.Id));
            }

            PortalRoom = Rooms[0];

            Player = new Player();

            GScore = new TextBlock();
            GScore.Foreground = Brushes.Gray;
            GScore.Text = "Score: 0";
            GCanvas = new Canvas();
            GGrid = gGrid;
            gGrid.Children.Add(GCanvas);
            gGrid.Children.Add(GScore);

            BarrierList = new List<Rect>();

            BarrierList.Add(new Rect(0, 0, 465, 20));
            BarrierList.Add(new Rect(0, 442, 465, 100));
            BarrierList.Add(new Rect(0, 20, 20, 422));
            BarrierList.Add(new Rect(460, 0, 100, 462));

        }
        public bool AddRoom(string roomName, Hallway entHallway)
        {
            Room newRoom = new Room(roomName, Rooms.Count);
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
                    ChangeRoomFaceing(newRoom, Logic.RotateFaceing90(newRoom.Faceing));
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
                        bool hallwayOutside = false;
                        Hallway hallway = new Hallway(door, 40, Rooms.Count, door.Id);

                        foreach (Rect barrier in BarrierList)
                        {
                            if (hallway.Body.Hitbox.IntersectsWith(barrier))
                            {
                                hallwayOutside = true;
                                Render.RemoveElement(hallway.Body.Mesh);
                            }
                        }

                        if (!hallwayOutside) Hallways.Add(hallway);

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

                PortalRoom = newRoom;

                Render.AddRoomToCanvas(newRoom);

                //Sets the hallway status to connected
                entHallway.Connected();

                return true;
            }
            return false;
        }
        public void ChangeRoomFaceing(Room room, char faceing)
        {
            room.ChangeFaceing(faceing);

            foreach (Door door in room.Doors)
            {
                foreach (Hallway hallway in Hallways)
                {
                    if (hallway.Id == room.Id && hallway.DoorId == door.Id)
                    {
                        hallway.ChangeLocRot(door, 40, room.Location);
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
        public void AddToScore(int amaunt)
        {
            Score += amaunt;
            GScore.Text = $"Score: {Score}";
        }
        public void SetScore(int amaunt)
        {
            Score = amaunt;
            GScore.Text = $"Score: {Score}";
        }
        public void AddExitPortal()
        {
            ExitPortal = new Portal(Rooms[0].Location[0] + Rooms[0].Body.Mesh.Height /  2, Rooms[0].Location[1] + Rooms[0].Body.Mesh.Width / 2, 0);
            Render.AddEntityToCanvas(ExitPortal);
        }
        public void Over()
        {
            this.Player = null; 
            gameOver = true;
            Transfer.WriteInfoToConsole(Score);
        }
        public void Exit()
        {
            gameOver = true;
            Transfer.WriteInfoToConsole(Score);
        }
        public void DeleteExitPortal()
        {
            Render.RemoveEntity(ExitPortal);
            ExitPortal = null;
        }

    }
}
