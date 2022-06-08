using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    public class Transfer
    {
        public static string GetLocation()
        {
            string transferFileLoc;
            transferFileLoc = ".";

            transferFileLoc = Path.GetFullPath(transferFileLoc);

            string[] hlpr = transferFileLoc.Split('\\');
            transferFileLoc = "";
            for (int i = 0; i < hlpr.Length - 4; i++)
            {
                transferFileLoc += hlpr[i];
                transferFileLoc += "\\";
            }
            return transferFileLoc;

        }
        public static bool IsAvailable()
        {
            string transferFileLoc = GetLocation();
            try
            {
                StreamReader sr = File.OpenText(transferFileLoc + "transfer.txt");
                sr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void WriteInfoToConsole(int score,string alive)
        {
            try
            {
                StreamWriter sw = File.CreateText(GetLocation() + "transfer.txt");
                sw.WriteLine(score+ $";{alive}");
                sw.Close();
            }
            catch { }
        }
        public static string[] ReadInfoFromConsole()
        {
            string[] info = {"","T"};

            if (IsAvailable())
            {

                StreamReader sr = File.OpenText(GetLocation() + "transfer.txt");
                info = sr.ReadLine().Trim().Split(';');
                sr.Close();
                GameLogic.StopConsoleWindow();
            }

            return info;
        }
    }
}
