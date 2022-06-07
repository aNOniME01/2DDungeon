using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeon
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
            return transferFileLoc += "transfer.txt";

        }
        public static string ReadInData()
        {
            string info = "";
            StreamReader sr = File.OpenText(GetLocation());
            string[] hlpr = sr.ReadLine().Trim().Split(';'); 
            info = hlpr[0];
            sr.Close();

            return info;
        }

    }
}
