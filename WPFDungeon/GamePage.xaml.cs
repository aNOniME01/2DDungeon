using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private static DispatcherTimer? timer;
        private static Game game;
        private static bool mUp;
        private static bool mDown;
        private static bool mLeft;
        private static bool mRight;
        public GamePage()
        {
            InitializeComponent();

            mUp = false;
            mDown = false;
            mLeft = false;
            mRight = false;

            Process.Start(Transfer.GetLocation() + "\\ConsoleDungeon\\bin\\Debug\\net5.0\\ConsoleDungeon.exe");
        }
        public static void Page_KeyDown(Key key)
        {
            if (key == Key.W)
            {
                mUp = true;
            }
            else if (key == Key.S)
            {
                mDown = true;
            }
            else if (key == Key.A)
            {
                mLeft = true;
            }
            else if (key == Key.D)
            {
                mRight = true;
            }
        }

        public static void KeyPressed(Key key )
        {
            if (key == Key.W)
            {
                mUp = false;
            }
            else if (key == Key.S)
            {
                mDown = false;
            }
            else if (key == Key.A)
            {
                mLeft = false;
            }
            else if (key == Key.D)
            {
                mRight = false;
            }
            else if (key == Key.Space)
            {
                game.Player.Shoot();
                Bullet shotBullet = game.Player.Bullets[game.Player.Bullets.Count - 1];
                Render.AddToCanvas(shotBullet.Body.Mesh, shotBullet.Location[0], shotBullet.Location[1]);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            game = new Game(canvas);

            GameLogic.GameLoad(game);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (Transfer.IsAvailable())
            {
                //put the player to the portal
            }


            GameLogic.GameLoop(mUp, mDown, mLeft, mRight);
        }

    }
}
