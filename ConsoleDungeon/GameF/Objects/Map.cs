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
        public List<Wall> Walls { get; private set; }
        public double[] PortalPos { get; private set; }
        public char[,] GameArea { get; private set; }

        private static Random rnd = new Random();
        public Map()
        {
            //--------------------
            // 0 = emptySpace 
            // 1 = entity
            // 2 = obsticle
            // 3 = point
            //--------------------
            GameArea = new char[20, 50];
            for (int i = 0; i < GameArea.GetLength(0); i++)
            {
                for (int j = 0; j < GameArea.GetLength(1); j++)
                {
                    GameArea[i, j] = '0';
                }
            }

            this.Player = new Player(GameArea);
            GameArea[(int)this.Player.Location[0], (int)this.Player.Location[1]] = '1';

            Walls = new List<Wall>();
            //Put the amaunt of walls you want to spawn here
            GenerateStarterWalls(20);

            Points = new List<Point>();
            //Put the amaunt of money you want to spawn here
            GeneratePoints(6);

            Seekers = new List<Seeker>();
            //Put the amaunt of seekers you want to spawn here
            GenerateSeekers(4);

            //PutDownPortal
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
                Walls.Add(new Wall(x, y));
                GameArea[x, y] = '2';
            }
        }
        public void GenerateWall()
        {
            int wallOrientation = rnd.Next(1, 3);
            int hole;

            if (wallOrientation == 1)
            {
                for (int y = 1; y < GameArea.GetLength(1) - 1; y++)
                {
                    hole = rnd.Next(0, 2);
                    if (hole == 1)
                    {
                        if (GameArea[(int)Player.Location[0], y] == '0') 
                        { 
                            GameArea[(int)Player.Location[0], y] = '2';
                            Walls.Add(new Wall((int)Player.Location[0], y));
                            Render.WriteAt("#", ConsoleColor.White, ConsoleColor.Black, (int)Player.Location[0]+2, y+1);
                        }
                    }
                }
            }
            else if (wallOrientation == 2)
            {
                for (int x = 1; x < GameArea.GetLength(0) - 1; x++)
                {
                    hole = rnd.Next(0, 2);
                    if (hole == 1)
                    {
                        if (GameArea[x, (int)Player.Location[1]] == '0')
                        {
                            GameArea[x, (int)Player.Location[1]] = '2';
                            Walls.Add(new Wall(x, (int)Player.Location[1]));
                            Render.WriteAt("#", ConsoleColor.White, ConsoleColor.Black, x + 2, (int)Player.Location[1] + 1);
                        }
                    }
                }
            }

        }
        private void GeneratePoints(int num)
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
