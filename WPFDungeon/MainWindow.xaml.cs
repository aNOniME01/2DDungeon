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
        private static DispatcherTimer timer;
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

            GameLogic.GameLoop(game, mUp,mDown,mLeft,mRight,Width-25,Height-50,canvas);
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
                Canvas.SetTop(game.Player.Bullets[lastBullet].Mesh, game.Player.Bullets[lastBullet].Location[0]);
                Canvas.SetLeft(game.Player.Bullets[lastBullet].Mesh, game.Player.Bullets[lastBullet].Location[1]);
                canvas.Children.Add(game.Player.Bullets[lastBullet].Mesh);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += timer_Tick;
            timer.Start();

            game = new Game(Width, Height);
            canvas.Children.Add(game.Rooms[0].Area);
            Canvas.SetTop(game.Rooms[0].Area, game.Player.Location[0]);
            Canvas.SetLeft(game.Rooms[0].Area, game.Player.Location[1]);

            SetUpEnemy();

            SetUpPlayer();
        }
        private void SetUpPlayer()
        {
            canvas.Children.Add(game.Player.Body.Mesh);
            Canvas.SetTop(game.Player.Body.Mesh, game.Player.Location[0]);
            Canvas.SetLeft(game.Player.Body.Mesh, game.Player.Location[1]);

            Canvas.SetZIndex(game.Player.Body.Mesh, 1);
        }
        private void SetUpEnemy()
        {
            foreach (var enemy in game.Rooms[0].SpawnMaps[0].Swifters)
            {
                canvas.Children.Add(enemy.Body.Mesh);
                Canvas.SetTop(enemy.Body.Mesh, enemy.Location[0]);
                Canvas.SetLeft(enemy.Body.Mesh, enemy.Location[1]);
            }
            foreach (Shooter shooter in game.Rooms[0].SpawnMaps[0].Shooters)
            {
                canvas.Children.Add(shooter.Body.Mesh);
                Canvas.SetTop(shooter.Body.Mesh, shooter.Location[0]);
                Canvas.SetLeft(shooter.Body.Mesh, shooter.Location[1]);
            }
        }
        public void AddToCanvas(UIElement uiElement)
        {
            canvas.Children.Add(uiElement);
        }
    }
}
