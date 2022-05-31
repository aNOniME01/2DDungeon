using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private GameWindow gameWindow;
        private ScoreboardPage scorePage;
        public MainMenu()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameWindow != null)
            {
                gameWindow.Close();
            }


            gameWindow = new GameWindow();
            gameWindow.Show();

            scorePage = new ScoreboardPage();
        }
        public void Closeing()
        {
            if (gameWindow != null)
            {
                gameWindow.Close();
            }
        }
    }
}
