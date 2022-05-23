using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace WPFDungeon
{
    class Room
    {
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }
        public IBody Body { get; private set; }
        public List<Door> Doors { get; private set; }
        public List<SpawnMap> SpawnMaps { get; private set; }
        public SpawnMap SelectedSpawnMap { get; private set; }
        public string Type { get; private set; }
        public int Id { get; private set; }
        public Room(string fileName,int roomId)
        {
            Location = new double[2] {0,0};
            Faceing = 'T';
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
                        Doors.Add(new Door(Convert.ToDouble(sgd[0]), Convert.ToChar(sgd[1]), height, width,Id,doorId));
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
                            SpawnMaps[SpawnMaps.Count - 1].AddShooter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToInt32(sgd[2]), Convert.ToChar(sgd[3]));
                        }
                    }
                    else if (line[0] == 'F')//Swifter
                    {
                        //Swifter x;y;faceing
                        if (Id != 0)
                        {
                            string[] sgd = line.Trim('F').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddSwifter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToChar(sgd[2]));
                        }
                    }
                    else if (line[0] == 'O')//Portal
                    {
                        //Portal x;y;faceing
                        string[] sgd = line.Trim('O').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count - 1].AddPortal(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToChar(sgd[2]));
                    }
                    else if (line[0] == 'P')//Point
                    {
                        //Point x;y
                    }
                }
            }
            Body = new RoomBody(height, width, Location, Type);

            SelectedSpawnMap = SpawnMaps[Logic.rnd.Next(0, SpawnMaps.Count)];
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
                        Doors.Add(new Door(Convert.ToDouble(sgd[0]), Convert.ToChar(sgd[1]), height, width, Id, doorId));
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
                            SpawnMaps[SpawnMaps.Count - 1].AddShooter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToInt32(sgd[2]), Convert.ToChar(sgd[3]));
                        }
                    }
                    else if (line[0] == 'F')//Swifter
                    {
                        //Swifter x;y;faceing
                        if (Id != 0)
                        {
                            string[] sgd = line.Trim('F').Trim().Split(';');
                            SpawnMaps[SpawnMaps.Count - 1].AddSwifter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToChar(sgd[2]));
                        }
                    }
                    else if (line[0] == 'O')//Portal
                    {
                        //Portal x;y;faceing
                        string[] sgd = line.Trim('O').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count - 1].AddPortal(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToChar(sgd[2]));
                    }
                    else if (line[0] == 'P')//Point
                    {
                        //Point x;y
                    }
                }
            }
            (Body as RoomBody).Refresh(height,width,Location);
        }
        public void ChangeFaceing(char newFaceing)
        {
            //Resets the room to defaoult (faceing, height, width)
            ResetRoom(Type);

            //Assign new faceing
            Faceing = newFaceing;

            RotateEntityWithRoom(SelectedSpawnMap.Shooters);
            RotateEntityWithRoom(SelectedSpawnMap.Swifters);

            if (SelectedSpawnMap.Portal != null)
            {
                ChangeEntityFaceingWithRoom(SelectedSpawnMap.Portal);
            }

            //Set RoomFaceing
            Body.FaceTo(newFaceing);

            ChangeDoorFaceing();

        }
        private void ChangeDoorFaceing()
        {
            foreach (Door door in Doors)
            {
                if (Faceing != 'T')
                {
                    door.SetLocRot(door.X, Logic.RotateFaceingWithRoom(Faceing, door.Faceing), Body.Mesh.Height, Body.Mesh.Width);
                }
            }

        }
        private void RotateEntityWithRoom(List<IEntity> entities)
        {
            if (Faceing != 'T')
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

            if (Faceing == 'B')
            {
                newLoc[0] = Body.Mesh.Height - (entity.Location[0] - Location[0]) - entity.Body.Mesh.Height;
                newLoc[1] = Body.Mesh.Width - (entity.Location[1] - Location[1]) - entity.Body.Mesh.Width;
            }
            else if (Faceing == 'L')
            {
                newLoc[0] = entity.Location[1] - Location[1] + Location[0];
                newLoc[1] = entity.Location[0] - Location[0] + Location[1];
            }
            else if (Faceing == 'R')
            {
                newLoc[0] =Body.Mesh.Width - (entity.Location[1] - Location[1]) - entity.Body.Mesh.Width;
                newLoc[1] =Body.Mesh.Height - (entity.Location[0] - Location[0]) - entity.Body.Mesh.Height;
            }

            entity.GoTo(newLoc);

            entity.FaceTo(Logic.RotateFaceingWithRoom(Faceing, entity.Faceing));
        }
        public void ChangeLocation(double y, double x)
        {
            Location[0] = y;
            Location[1] = x;

            Render.RefreshElement(Body.Mesh,Location);

            (Body as RoomBody).MoveHitbox();
            ChangeEntityLocation();
        }
        public void ToDoorLoc(Door targetDoor,Door door)
        {
            double disY = targetDoor.Location[0] - door.Location[0];
            double disX = targetDoor.Location[1] - door.Location[1];

            ChangeLocation(disY,disX);
        }
        private void ChangeEntityLocation()
        {
            foreach (Shooter entity in SelectedSpawnMap.Shooters)
            {
                entity.ToRoomLoc(Location);
            }

            foreach (Swifter entity in SelectedSpawnMap.Swifters)
            {
                entity.ToRoomLoc(Location);
            }

            if (SelectedSpawnMap.Portal != null)
            {
                SelectedSpawnMap.Portal.ToRoomLoc(Location);
            }
        }
        public Door SearchDoorFaceingOpposit(char faceing)
        {
            char SearchedFaceing;
            if (faceing == 'T') SearchedFaceing = 'B';
            else if (faceing == 'B') SearchedFaceing = 'T';
            else if (faceing == 'L') SearchedFaceing = 'R';
            else SearchedFaceing = 'L';

            foreach (Door door in Doors)
            {
                if (door.Faceing == SearchedFaceing) return door;
            }
            return null;
        }
    }
}
