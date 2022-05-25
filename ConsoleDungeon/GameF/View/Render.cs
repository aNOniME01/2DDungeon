using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDungeon
{
    internal class Render
    {
        public static void FullGameRenderer(Map map,int score)//Palya, Jatekos, Falak, Penz megjelenitese
        {
            Console.Clear();
            Console.WriteLine($"Score: {score}");
            RenderMapSide(map);
            for (int i = 0; i < map.GameArea.GetLength(0); i++)
            {
                for (int j = 0; j < map.GameArea.GetLength(1); j++)
                {
                    if (map.GameArea[i, j] == '2')
                    {
                        WriteAt("#", ConsoleColor.Gray, ConsoleColor.Black, i + 2, j + 1);
                    }
                    if (map.GameArea[i, j] == '3')
                    {
                        WriteAt("C", ConsoleColor.Green, ConsoleColor.Black, i + 2, j + 1);
                    }

                    if (map.Player.Location[0] == i && map.Player.Location[1] == j)
                    {
                        WriteAt("O", ConsoleColor.Yellow, ConsoleColor.Black, i + 2, j + 1);
                    }
                    else if (map.PortalPos[0] == i && map.PortalPos[1] == j)
                    {
                        WriteAt("$", ConsoleColor.DarkMagenta, ConsoleColor.Black, i + 2, j + 1);
                    }

                    foreach (Seeker seeker in map.Seekers)
                    {
                        if (seeker.Location[0] == i && seeker.Location[1] == j)
                        {
                            WriteAt("x", ConsoleColor.DarkRed, ConsoleColor.Black, i + 2, j + 1);
                        }
                    }
                }
            }
        }
        public static void PlayerStepRender(Map map)
        {
            if (map.Player.Location[0] != -1 && map.Player.Location[1] != -1)
            {
                WriteAt("O", ConsoleColor.Yellow, ConsoleColor.Black, (int)map.Player.Location[0] + 2, (int)map.Player.Location[1] + 1);
                if (map.GameArea[(int)map.Player.PrevLocation[0], (int)map.Player.PrevLocation[1]] == '0')
                {
                    WriteAt(" ", ConsoleColor.Yellow, ConsoleColor.Black, (int)map.Player.PrevLocation[0] + 2, (int)map.Player.PrevLocation[1] + 1);
                }
            }
        }
        public static void SeekerStepRender(Map map)
        {
            foreach (Seeker seeker in map.Seekers)
            {
                WriteAt("x", ConsoleColor.DarkRed, ConsoleColor.Black, (int)seeker.Location[0] + 2, (int)seeker.Location[1] + 1);
                if (map.GameArea[(int)seeker.PrevLocation[0], (int)seeker.PrevLocation[1]] == '0')
                {
                    WriteAt(" ", ConsoleColor.Yellow, ConsoleColor.Black, (int)seeker.PrevLocation[0] + 2, (int)seeker.PrevLocation[1] + 1);
                }
            }
        }
        public static void GameOver()//GameOver kiirasa
        {
            Thread.Sleep(400);
            Console.Clear();


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game Over");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press ESC to quit");
            Console.ResetColor();
        }
        private static void RenderMapSide(Map map)
        {
            for (int i = 1; i < map.GameArea.GetLength(0)+3; i++)
            {
                for (int j = 0; j < map.GameArea.GetLength(1)+2; j++)
                {
                    if (i == 1 || j == 0 || i == map.GameArea.GetLength(0) + 2|| j == map.GameArea.GetLength(1) + 1)
                    {
                        WriteAt("#", ConsoleColor.White,ConsoleColor.Black, i, j);
                    }
                }
            }

        }
        public static void WriteAt(string s, ConsoleColor fColor,ConsoleColor bColor, int x, int y)
        {
            Console.SetCursorPosition(y, x);

            Console.ForegroundColor = fColor;
            Console.BackgroundColor = bColor;

            Console.Write(s);
            Console.ResetColor();
        }

    }
}
