using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Map
    {
        public Player Player { get; private set; }
        public List<Seeker> Seekers { get; private set; }
        public List<Point> Points { get; private set; }
        public double[] PortalPos { get; private set; }
        public char[,] GameArea { get; private set; }

        private static Random rnd = new Random();
        public Map()
        {
            //--------------------
            // 0 = emptySpace 
            // 1 = entity
            // 2 = obsticle
            // 3 = money
            //--------------------
            GameArea = new char[20,50];
            for (int i = 0; i < GameArea.GetLength(0); i++)
            {
                for (int j = 0; j < GameArea.GetLength(1); j++)
                {
                    GameArea[i, j] = '0';
                }
            }
            GenerateStarterWalls(20);//put the amaunt of walls you want to spawn here

            Points = new List<Point>();
            GenerateStarterPoint(6);//put the amaunt of money you want to spawn here

            this.Player = new Player(GameArea);
            GameArea[(int)this.Player.Location[0], (int)this.Player.Location[1]] = '1';

            Seekers = new List<Seeker>();
            GenerateSeekers(4);//put the amaunt of seekers you want to spawn here

            PortalPos = new double[2];
            PortalPos[0] = (double)rnd.Next(0, GameArea.GetLength(0));
            PortalPos[1] = 0;
            GameArea[(int)PortalPos[0], (int)PortalPos[1]] = '1';
        }

        #region GameArea manipulation
        public void SetToEmpty(int x, int y)
        {
            GameArea[x, y] = '0';
        }
        public void SetToEntity(int x, int y)
        {
            GameArea[x, y] = '1';
        }
        public void SetToObsticle(int x, int y)
        {
            GameArea[x, y] = '2';
        }
        #endregion
        private void GenerateStarterWalls(int num)
        {
            for (int i = 0; i < num; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(0, GameArea.GetLength(0));
                    y = rnd.Next(GameArea.GetLength(1) / 8, GameArea.GetLength(1));
                } while (GameArea[x, y] == '1' || GameArea[x, y] == '2' || GameArea[x, y] == '3');
                GameArea[x, y] = '2';
            }
        }
        private void GenerateStarterPoint(int num)
        {
            for (int i = 0; i < num; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(0, GameArea.GetLength(0));
                    y = rnd.Next(GameArea.GetLength(1) / 10, GameArea.GetLength(1));
                } while (GameArea[x, y] == '1' || GameArea[x, y] == '2'|| GameArea[x, y] == '3');
                Points.Add(new Point(x, y));
                GameArea[x, y] = '3';
            }
        }
        public void PointToNewLocation(Point point)
        {
            int x, y;
            do
            {
                x = rnd.Next(0, GameArea.GetLength(0));
                y = rnd.Next(GameArea.GetLength(1) / 10, GameArea.GetLength(1));
            } while (GameArea[x, y] == '1' || GameArea[x, y] == '2' || GameArea[x, y] == '3');
            point.SetLocation(x,y);
            GameArea[x, y] = '3';
        }
        private void GenerateSeekers(int num)
        {
            for (int i = 0; i < num; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(0, GameArea.GetLength(0));
                    y = rnd.Next(GameArea.GetLength(1) / 5, GameArea.GetLength(1));
                } while (GameArea[x, y] == '1' || GameArea[x, y] == '2' || GameArea[x, y] == '3');
                GameArea[x, y] = '1';
                Seekers.Add(new Seeker(x, y));
            }
        }

    }
}
