using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Logic
    {
        public static void GameLogic(Map map, bool gameOver)
        {
            Render.FullGameRenderer(map);
            do
            {
                while (!gameOver)
                {
                    PlayerController(map);
                    SeekerController(map);
                    gameOver = InteractionChecker(map);
                    Render.SeekerStepRender(map);
                    Render.PlayerStepRender(map);
                }
                if (Transfer.IsAvailable())
                {
                    gameOver = false;
                }
                
            } while (true);
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
                    Render.FullGameRenderer(map);
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
                Render.FullGameRenderer(map);
            }
            //Enemy collision checking
            foreach (Seeker seeker in map.Seekers)
            {
                if (map.Player.Location[0] == seeker.Location[0] && map.Player.Location[1] == seeker.Location[1])
                {
                    Render.FullGameRenderer(map);
                    map.Player.DissapearEntity();
                    Render.GameOver();
                    return true;
                }
            }
            return false;
        }
    }
}
