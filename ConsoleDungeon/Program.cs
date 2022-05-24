using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleDungeon
{
    class Program
    {
        private static Map map;
        private static bool gameOver;
        private static string transferFileLoc;
        static void Main(string[] args)
        {
            transferFileLoc = Transfer.GetLocation();
            gameOver = false;
            map = new Map();
            Thread.Sleep(4000);
            Logic.GameLogic(map, gameOver);
        }
    }
}
