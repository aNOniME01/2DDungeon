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
        public List<Door> DoorList { get; private set; }
        public List<SpawnMap> SpawnMaps { get; private set; }
        
        public Room(string fileName)
        {
            Area = new Rectangle();
            DoorList = new List<Door>();

            foreach (string line in File.ReadAllLines(Transfer.GetLocation()+"\\WPFDungeon\\Prefabs\\Rooms\\"+fileName+".txt"))
            {
                if (line != "")
                {
                    if (line[0] == 'W') Area.Width = Convert.ToDouble(line.Trim('W').Trim());
                    else if (line[0] == 'H') Area.Height = Convert.ToDouble(line.Trim('H').Trim());
                    else if (line[0] == 'D')
                    {
                        string[] sgd = line.Trim('D').Trim().Split(';');
                        DoorList.Add(new Door(Convert.ToDouble(sgd[0]), Convert.ToDouble(sgd[1]), Convert.ToDouble(sgd[2]), Convert.ToDouble(sgd[3])));
                    }
                    else if (line[0] == 'S')
                    {
                        /*Shooter x;y;faceing*/
                    }
                    else if (line[0] == 'P')
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
