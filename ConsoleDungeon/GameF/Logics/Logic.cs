using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Logic
    {
        private static int Score;
        private static StreamWriter sw = File.CreateText(Transfer.GetLocation());
        public static void GameLogic(Map map, bool gameOver,int score)
        {
            Score = score;

            Render.FullGameRenderer(map, score);
            while (!gameOver)
            {
                PlayerController(map);
                SeekerController(map);
                gameOver = InteractionChecker(map);
                Render.SeekerStepRender(map);
                Render.PlayerStepRender(map);
            }
            sw.WriteLine(Score);
            sw.Close();
        }
        private static void PlayerController(Map map)
        {
            ConsoleKeyInfo keypress = new ConsoleKeyInfo();

            keypress = Console.ReadKey();

            if (map.Player.Location[0] != -1 && map.Player.Location[1] != -1)
            {
                if (keypress.Key == ConsoleKey.UpArrow && map.Player.Location[0] - 1 >= 0 && map.GameArea[(int)map.Player.Location[0] - 1, (int)map.Player.Location[1]] != '2')
                {
                    map.SetToEmpty((int)map.Player.Location[0], (int)map.Player.Location[1]);
                    map.Player.SetEntityPrevPosTo(map.Player.Location);
                    map.Player.AddToEntityPos(-1, 0);
                    map.SetToEntity((int)map.Player.Location[0], (int)map.Player.Location[1]);
                }
                if (keypress.Key == ConsoleKey.DownArrow && map.Player.Location[0] + 1 < map.GameArea.GetLength(0) && map.GameArea[(int)map.Player.Location[0] + 1, (int)map.Player.Location[1]] != '2')
                {
                    map.SetToEmpty((int)map.Player.Location[0], (int)map.Player.Location[1]);
                    map.Player.SetEntityPrevPosTo(map.Player.Location);
                    map.Player.AddToEntityPos(1,0);
                    map.SetToEntity((int)map.Player.Location[0], (int)map.Player.Location[1]);
                }
                if (keypress.Key == ConsoleKey.LeftArrow && map.Player.Location[1] - 1 >= 0 && map.GameArea[(int)map.Player.Location[0], (int)map.Player.Location[1] - 1] != '2') 
                {
                    map.SetToEmpty((int)map.Player.Location[0], (int)map.Player.Location[1]);
                    map.Player.SetEntityPrevPosTo(map.Player.Location);
                    map.Player.AddToEntityPos(0, -1);
                    map.SetToEntity((int)map.Player.Location[0], (int)map.Player.Location[1]);
                }
                if (keypress.Key == ConsoleKey.RightArrow && map.Player.Location[1] + 1 < map.GameArea.GetLength(1) && map.GameArea[(int)map.Player.Location[0], (int)map.Player.Location[1] + 1] != '2')
                {
                    map.SetToEmpty((int)map.Player.Location[0], (int)map.Player.Location[1]);
                    map.Player.SetEntityPrevPosTo(map.Player.Location);
                    map.Player.AddToEntityPos(0, 1);
                    map.SetToEntity((int)map.Player.Location[0], (int)map.Player.Location[1]);
                }
                if (keypress.Key == ConsoleKey.Enter)
                {
                    Render.FullGameRenderer(map,Score);
                }

            }
        }
        private static void SeekerController(Map map)
        {
            foreach (Seeker seeker in map.Seekers)
            {
                seeker.Navigate(map);
            }
        }
        private static bool InteractionChecker(Map map)
        {
            //Entered portal check
            if (map.Player.Location[0] == map.PortalPos[0]&& map.Player.Location[1] == map.PortalPos[1])
            {
                map.Player.DissapearEntity();
                Render.FullGameRenderer(map,Score);

                return true;
            }
            foreach (Point point in map.Points)
            {
                if (map.Player.Location[0] == point.Location[0] && map.Player.Location[1] == point.Location[1])
                {
                    Score++;
                    Render.WriteAt($"Score: {Score}", ConsoleColor.White, ConsoleColor.Black, 0, 0);
                    map.GenerateWall();
                    map.PointToNewLocation(point);
                    Render.WriteAt("C", ConsoleColor.Green, ConsoleColor.Black, (int)point.Location[0]+2, (int)point.Location[1]+1);
                }
            }
            //Enemy collision checking
            foreach (Seeker seeker in map.Seekers)
            {
                if (map.Player.Location[0] == seeker.Location[0] && map.Player.Location[1] == seeker.Location[1])
                {
                    Render.FullGameRenderer(map,Score);
                    map.Player.DissapearEntity();
                    Render.GameOver();
                    return true;
                }

                foreach (Point point in map.Points)
                {
                    if (seeker.Location[0] == point.Location[0] && seeker.Location[1] == point.Location[1])
                    {
                        map.PointToNewLocation(point);
                        Render.WriteAt("C", ConsoleColor.Green, ConsoleColor.Black, (int)point.Location[0] + 2, (int)point.Location[1] + 1);
                    }
                }

            }
            return false;
        }
    }
}
