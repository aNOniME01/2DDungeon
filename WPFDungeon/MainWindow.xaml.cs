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
using System.Windows.Threading;

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DispatcherTimer? timer;
        private static Game game;
        private static bool mUp;
        private static bool mDown;
        private static bool mLeft;
        private static bool mRight;
        public MainWindow()
        {
            InitializeComponent();

            mUp = false;
            mDown = false;
            mLeft = false;
            mRight = false;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                mUp = true;
            }
            else if (e.Key == Key.S)
            {
                mDown = true;
            }
            else if (e.Key == Key.A)
            {
                mLeft = true;
            }
            else if (e.Key == Key.D)
            {
                mRight = true;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                mUp = false;
            }
            else if (e.Key == Key.S)
            {
                mDown = false;
            }
            else if (e.Key == Key.A)
            {
                mLeft = false;
            }
            else if (e.Key == Key.D)
            {
                mRight = false;
            }
            else if (e.Key == Key.Space)
            {
                int lastBullet = game.Player.Bullets.Count;
                game.Player.Shoot();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game = new Game(canvas);

            GameLogic.GameLoad(game);
            Render.Load(game);

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

            foreach (Bullet bullet in game.Player.Bullets)
            {
                lable.Content = $"y:{bullet.Location[0]}x:{bullet.Location[1]}";
            }

            GameLogic.GameLoop(mUp, mDown, mLeft, mRight, Width - 25, Height - 50);
        }
    }
}
