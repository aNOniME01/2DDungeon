using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace WPFDungeon
{
    class Room
    {
        public double[] Location { get; private set; }
        public Direction Facing { get; private set; }
        public IBody Body { get; private set; }
        public List<Door> Doors { get; private set; }
        public List<SpawnMap> SpawnMaps { get; private set; }
        public SpawnMap SelectedSpawnMap { get; private set; }
        public int SelectedSpawnMapIndex { get; private set; }
        public string Type { get; private set; }
        public int Id { get; private set; }
        public Room(string fileName,int roomId)
        {
            Location = new double[2] {0,0};
            Facing = Direction.Top;
            Doors = new List<Door>();
            SpawnMaps = new List<SpawnMap>();
            Type = fileName;
            Id = roomId;

            double height = 0;
            double width = 0;
            int doorId = 0;
            foreach (string line in File.ReadAllLines(Transfer.GetLocation() + "\\WPFDungeon\\Prefabs\\Rooms\\" + fileName + ".txt"))
            {
                if (line != "")
                {
                    if (line[0] == 'W') width = Convert.ToDouble(line.Trim('W').Trim());
                    else if (line[0] == 'H') height = Convert.ToDouble(line.Trim('H').Trim());
                    else if (line[0] == 'D')//Door
                    {
                        string[] sgd = line.Trim('D').Trim().Split(';');
                        Doors.Add(new Door(Convert.ToDouble(sgd[0]), (Direction)Enum.Parse(typeof(Direction), sgd[1]), height, width,Id,doorId));
                        doorId++;
                    }
                    else if (line[0] == 'V')
                    {
                        SpawnMaps.Add(new SpawnMap(Id));
                    }
                    else if (line[0] == 'S')//Shooter
                    {
                        //Shooter x;y;turretNum;faceing
                        if (Id != 0)
                        {
                            string[] sgd = line.Trim('S').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddShooter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToInt32(sgd[2]), (Direction)Enum.Parse(typeof(Direction),sgd[3]));
                        }
                    }
                    else if (line[0] == 'F')//Swifter
                    {
                        //Swifter x;y;faceing
                        if (Id != 0)
                        {
                            string[] sgd = line.Trim('F').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddSwifter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), (Direction)Enum.Parse(typeof(Direction),sgd[2]));
                        }
                    }
                    else if (line[0] == 'O')//Portal
                    {
                        //Portal x;y;faceing
                        string[] sgd = line.Trim('O').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count - 1].AddPortal(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), (Direction)Enum.Parse(typeof(Direction), sgd[2]));
                    }
                    else if (line[0] == 'P')//Point
                    {
                        if (Id != 0)
                        {
                            //Point x;y
                            string[] sgd = line.Trim('P').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddPoint(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]));
                        }
                    }
                }
            }
            Body = new RoomBody(height, width, Location, Type);

            SelectedSpawnMapIndex = Logic.rnd.Next(0, SpawnMaps.Count);
            SelectedSpawnMap = SpawnMaps[SelectedSpawnMapIndex];
        }
        private void ResetRoom(string fileName)
        {
            Doors.Clear();
            SpawnMaps.Clear();
            double height = 0;
            double width = 0;
            int doorId = 0;
            foreach (string line in File.ReadAllLines(Transfer.GetLocation() + "\\WPFDungeon\\Prefabs\\Rooms\\" + fileName + ".txt"))
            {
                if (line != "")
                {
                    if (line[0] == 'W') width = Convert.ToDouble(line.Trim('W').Trim());
                    else if (line[0] == 'H') height = Convert.ToDouble(line.Trim('H').Trim());
                    else if (line[0] == 'D')//Door
                    {
                        string[] sgd = line.Trim('D').Trim().Split(';');
                        Doors.Add(new Door(Convert.ToDouble(sgd[0]), (Direction)Enum.Parse(typeof(Direction), sgd[1]), height, width, Id, doorId));
                        doorId++;
                    }
                    else if (line[0] == 'V')
                    {
                        SpawnMaps.Add(new SpawnMap(Id));
                    }
                    else if (line[0] == 'S')//Shooter
                    {
                        //Shooter x;y;turretNum;faceing
                        if (Id != 0)
                        {
                            string[] sgd = line.Trim('S').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddShooter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToInt32(sgd[2]), (Direction)Enum.Parse(typeof(Direction), sgd[3]));
                        }
                    }
                    else if (line[0] == 'F')//Swifter
                    {
                        //Swifter x;y;faceing
                        if (Id != 0)
                        {
                            string[] sgd = line.Trim('F').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddSwifter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), (Direction)Enum.Parse(typeof(Direction), sgd[2]));
                        }
                    }
                    else if (line[0] == 'O')//Portal
                    {
                        //Portal x;y;faceing
                        string[] sgd = line.Trim('O').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count - 1].AddPortal(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), (Direction)Enum.Parse(typeof(Direction), sgd[2]));
                    }
                    else if (line[0] == 'P')//Point
                    {
                        if (Id != 0)
                        {
                            //Point x;y
                            string[] sgd = line.Trim('P').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddPoint(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]));
                        }
                    }
                }
            }
            (Body as RoomBody).Refresh(height,width,Location);

            SelectedSpawnMap = SpawnMaps[SelectedSpawnMapIndex];
        }
        public void ChangeFaceing(Direction newFaceing)
        {
            //Resets the room to defaoult (faceing, height, width)
            ResetRoom(Type);

            //Assign new faceing
            Facing = newFaceing;

            RotateEntityWithRoom(SelectedSpawnMap.Shooters);
            RotateEntityWithRoom(SelectedSpawnMap.Swifters);
            RotateEntityWithRoom(SelectedSpawnMap.Points);

            if (SelectedSpawnMap.Portal != null)
            {
                ChangeEntityFaceingWithRoom(SelectedSpawnMap.Portal);
            }

            //Set RoomFaceing
            Body.FaceTo(Facing);

            ChangeDoorFaceing();

        }
        private void ChangeDoorFaceing()
        {
            foreach (Door door in Doors)
            {
                if (Facing != Direction.Top)
                {
                    door.SetLocRot(door.X, Logic.RotateFaceingWithRoom(Facing, door.Facing), Body.Mesh.Height, Body.Mesh.Width);
                }
            }

        }
        private void RotateEntityWithRoom(List<IEntity> entities)
        {
            if (Facing != Direction.Top)
            {
                foreach (var entity in entities)
                {
                    ChangeEntityFaceingWithRoom(entity);
                }
            }
        }
        private void ChangeEntityFaceingWithRoom(IEntity entity)
        {
            double[] newLoc = new double[2];

            if (Facing == Direction.Bottom)
            {
                newLoc[0] = Body.Mesh.Height - entity.Body.Mesh.Height - (entity.Location[0] - Location[0]) - Location[0];
                newLoc[1] = Body.Mesh.Width - entity.Body.Mesh.Width - (entity.Location[1] - Location[1]) - Location[1];
            }
            else if (Facing == Direction.Left)
            {
                newLoc[0] = Body.Mesh.Width - (entity.Location[1] - Location[1]) - entity.Body.Mesh.Width + Location[0];
                newLoc[1] = Body.Mesh.Height - (entity.Location[0] - Location[0]) - entity.Body.Mesh.Height + Location[1];
            }
            else if (Facing == Direction.Right)
            {
                newLoc[0] = entity.Location[1] - Location[1] + Location[0];
                newLoc[1] = entity.Location[0] - Location[0] + Location[1];
            }

            entity.GoTo(newLoc);

            entity.FaceTo(Logic.RotateFaceingWithRoom(Facing, entity.Facing));
        }
        public void ChangeLocation(double y, double x)
        {
            Location[0] = y;
            Location[1] = x;

            Render.RefreshElement(Body.Mesh,Location);

            (Body as RoomBody).MoveHitbox();
            ChangeEntityLocation(y,x);
        }
        public void ToDoorLoc(Door targetDoor,Door door)
        {
            double disY = targetDoor.Location[0] - door.Location[0];
            double disX = targetDoor.Location[1] - door.Location[1];

            ChangeLocation(disY,disX);
        }
        private void ChangeEntityLocation(double y, double x)
        {
            foreach (Shooter entity in SelectedSpawnMap.Shooters)
            {
                entity.ChangeLocationBy(y,x);
            }

            foreach (Swifter entity in SelectedSpawnMap.Swifters)
            {
                entity.ChangeLocationBy(y, x);
            }
            foreach (Point point in SelectedSpawnMap.Points)
            {
                point.ChangeLocationBy(y, x);
            }

            if (SelectedSpawnMap.Portal != null)
            {
                SelectedSpawnMap.Portal.ChangeLocationBy(y,x);
            }
        }
        public Door SearchDoorFaceingOpposit(Direction faceing)
        {
            Direction SearchedFaceing;
            if (faceing == Direction.Top) SearchedFaceing = Direction.Bottom;
            else if (faceing == Direction.Bottom) SearchedFaceing = Direction.Top;
            else if (faceing == Direction.Left) SearchedFaceing = Direction.Right;
            else SearchedFaceing = Direction.Left;

            foreach (Door door in Doors)
            {
                if (door.Facing == SearchedFaceing) return door;
            }
            return null;
        }
    }
}
