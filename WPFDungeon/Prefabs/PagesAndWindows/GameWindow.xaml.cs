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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private static DispatcherTimer? timer;
        private static Game game;
        private static bool mUp;
        private static bool mDown;
        private static bool mLeft;
        private static bool mRight;
        public GameWindow()
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
                game.Player.Shoot();
                Bullet shotBullet = game.Player.Bullets[game.Player.Bullets.Count - 1];
                Render.AddToCanvas(shotBullet.Body.Mesh, shotBullet.Location[0], shotBullet.Location[1]);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game = new Game(grid);

            GameLogic.GameLoad(game);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            GameLogic.GameLoop(mUp, mDown, mLeft, mRight);
            if (game.gameOver)
            {
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (game.gameOver)
            {
                RestartWindow restartWindow = new RestartWindow();
                restartWindow.Show();
            }
            timer.Stop();
            GameLogic.StopConsoleWindow();
        }
    }
}
