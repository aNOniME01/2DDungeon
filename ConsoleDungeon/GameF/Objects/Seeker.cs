using ConsoleDungeon.GameF.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Seeker : IEntity
    {
        
        public double[] Location { get; private set; }
        public double[] PrevLocation { get; private set; }
        private Random rnd = new Random();
        public Seeker(double x, double y)
        {
            Location = new double[2];
            Location[0] = x;
            Location[1] = y;

            PrevLocation = new double[2];
        }
        public void SetEntityPrevPosTo(double[] ePrevPos)
        {
            PrevLocation[0] = ePrevPos[0];
            PrevLocation[1] = ePrevPos[1];
        }
        public void AddToEntityPos(int x, int y)
        {
            Location[0] += x;
            Location[1] += y;
        }
        public void DissapearEntity()
        {
            Location[0] = -1;
            Location[1] = -1;
        }

        public void Navigate(Map map)
        {

            bool jobbra = false;
            bool balra = false;
            bool fel = false;
            bool le = false;

            int irany = rnd.Next(0, 2);

            if (Location[0] - 1 > 0) if(map.GameArea[(int)Location[0] - 1, (int)Location[1]] != '2') balra = true;
            if (Location[0] + 1 < map.GameArea.GetLength(0)) if(map.GameArea[(int)Location[0] + 1, (int)Location[1]] != '2') jobbra = true;
            if (Location[1] - 1 > 0) if (map.GameArea[(int)Location[0], (int)Location[1] - 1] != '2') fel = true;
            if (Location[1] + 1 < map.GameArea.GetLength(1)) if(map.GameArea[(int)Location[0], (int)Location[1] + 1] != '2') le = true;

            SetEntityPrevPosTo(Location);
            if (irany == 0)
            {
                if (Location[1] < map.Player.Location[1] && le)//ha lefele van a cel a seekerhez kepest 
                {
                    AddToEntityPos(0, 1);
                }
                else if (fel)//ha felfele van a cel a seekerhez kepest
                {
                    AddToEntityPos(0, -1);
                }
                else if (Location[0] < map.Player.Location[0] && jobbra)//ha jobbra van a cel a seekerhez kepest
                {
                    AddToEntityPos(1, 0);
                }
                else if (balra)//ha balra van a cel a seekerhez kepest
                {
                    AddToEntityPos(-1, 0);
                }
            }
            else
            {
                if (Location[0] < map.Player.Location[0] && jobbra)//ha jobbra van a cel a seekerhez kepest
                {
                    AddToEntityPos(1, 0);
                }
                else if (balra)//ha balra van a cel a seekerhez kepest
                {
                    AddToEntityPos(-1, 0);
                }
                else if (Location[1] < map.Player.Location[1] && le)//ha lefele van a cel a seekerhez kepest 
                {
                    AddToEntityPos(0, 1);
                }
                else if (fel)//ha felfele van a cel a seekerhez kepest
                {
                    AddToEntityPos(0, -1);
                }
            }
            map.SetToEmpty((int)PrevLocation[0], (int)PrevLocation[1]);
            map.SetToEntity((int)Location[0], (int)Location[1]);
        }
    }
}
