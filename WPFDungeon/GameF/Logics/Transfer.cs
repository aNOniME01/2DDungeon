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
        public static void WriteInfoToConsole(int score)
        {
            try
            {
                StreamWriter? sw = File.CreateText(GetLocation() + "transfer.txt");
                sw.WriteLine(score);
                sw.Close();
            }
            catch { }
        }
        public static string ReadInfoFromConsole()
        {
            string info = "";

            if (Transfer.IsAvailable())
            {
                GameLogic.StopConsoleWindow();

                StreamReader sr = File.OpenText(GetLocation() + "transfer.txt");
                if (sr.ReadLine() != "")
                {
                    info = sr.ReadLine();
                }
                sr.Close();
            }

            return info;
        }
    }
}
