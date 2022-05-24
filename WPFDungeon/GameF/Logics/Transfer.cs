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
        public static void ReadInfoFromConsole()
        {
            if (Transfer.IsAvailable())
            {
                //put the player to the portal
            }
        }
    }
}
