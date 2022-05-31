using System.Collections.Generic;

namespace WPFDungeon
{
    class LoggedData
    {
        public static List<string[]> dataList = new List<string[]>();
        public static char[] charac = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '@', '#', '$', '?', '!', '.', ' ' };
        public static string Username;//The name is for testing
        public static void Log(string IncomeingUsername)
        {
            Username = IncomeingUsername;
        }
    }
}
