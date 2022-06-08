using System.Collections.Generic;

namespace WPFDungeon
{
    class LoggedData
    {
        public static char[] charac = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '@', '#', '$', '?', '!', '.', ' ' };
        public static int? UserId = null;
        public static int? Score = null;
        public static GameWindow? gameWindow;

        public static void Log(int userId)
        {
            UserId = userId;
        }
        public static void CreateGameWindow( int? score)
        {
            Score = score;
            if (gameWindow != null)
            {
                gameWindow.Close();
            }



            gameWindow = new GameWindow();
            gameWindow.Show();
        }
        public static void CloseGameWindow()
        {
            if (gameWindow != null)
            {
                gameWindow.Close();
                gameWindow = null;
            }
        }
    }
}
