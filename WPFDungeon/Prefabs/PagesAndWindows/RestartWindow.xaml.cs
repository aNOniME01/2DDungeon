using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for RestartWindow.xaml
    /// </summary>
    public partial class RestartWindow : Window
    {
        private static int Score;
        private static string IsAlive;
        public RestartWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader sr = File.OpenText(Transfer.GetLocation() + "transfer.txt");
            string[] hlpr = sr.ReadLine().Trim().Split(';');
            Score = Convert.ToInt32(hlpr[0]);
            IsAlive = hlpr[1];
            sr.Close();

            score.Text = $"Score: {Score}";

            if (LoggedData.UserId == null)
            {
                saveAndExit.Visibility = Visibility.Hidden;
            }
            else
            {
                userText.Text = $"@{SQLOperations.GetUserById(LoggedData.UserId)}";
                userText.Visibility = Visibility.Visible;
            }

        }

        private void SaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            SQLOperations.UploadScore(LoggedData.UserId,Score);
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
