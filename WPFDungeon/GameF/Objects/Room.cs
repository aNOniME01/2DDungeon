using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media;

namespace WPFDungeon
{
    class Room
    {
        public Rectangle Area { get; private set; }
        public List<Door> Doors { get; private set; }
        public List<SpawnMap> SpawnMaps { get; private set; }
        
        public Room(string fileName)
        {
            Area = new Rectangle();
            Doors = new List<Door>();
            SpawnMaps = new List<SpawnMap>();

            foreach (string line in File.ReadAllLines(Transfer.GetLocation()+"\\WPFDungeon\\Prefabs\\Rooms\\"+fileName+".txt"))
            {
                if (line != "")
                {
                    if (line[0] == 'W') Area.Width = Convert.ToDouble(line.Trim('W').Trim());
                    else if (line[0] == 'H') Area.Height = Convert.ToDouble(line.Trim('H').Trim());
                    else if (line[0] == 'D')//Door
                    {
                        string[] sgd = line.Trim('D').Trim().Split(';');
                        Doors.Add(new Door(Convert.ToDouble(sgd[0]), Convert.ToChar(sgd[1]),Area.Height,Area.Width));
                    }
                    else if(line[0] == 'V')
                    {
                        SpawnMaps.Add(new SpawnMap());
                    }
                    else if (line[0] == 'S')//Shooter
                    {
                        //Shooter x;y;turretNum;faceing
                        string[] sgd = line.Trim('S').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count-1].AddShooter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]),Convert.ToInt32(sgd[2]),Convert.ToChar(sgd[3]));
                    }
                    else if (line[0] == 'F')//Swifter
                    {
                        //Swifter x;y;faceing
                        string[] sgd = line.Trim('F').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count-1].AddSwifter(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]),Convert.ToChar(sgd[2]));
                    }
                    else if (line[0] == 'O')//Portal
                    {
                        //Portal x;y;faceing
                        string[] sgd = line.Trim('O').Trim().Split(';');
                        SpawnMaps[SpawnMaps.Count-1].AddPortal(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]),Convert.ToChar(sgd[2]));
                    }
                    else if (line[0] == 'P')//Point
                    {
                        //Point x;y
                    }
                }
            }

            Area.Stroke = Brushes.Black;
            Area.Fill = Brushes.Green;
        }
    }
}
