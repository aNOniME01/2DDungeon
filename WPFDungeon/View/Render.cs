using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFDungeon
{
    class Render
    {
        private static Canvas canvas = null;
        private static Game game = null;
        public static void Load(Canvas cvs,Game gm)
        {
            canvas = cvs;
            game = gm;

            canvas.Children.Add(game.Rooms[0].Area);
            Canvas.SetTop(game.Rooms[0].Area, game.Player.Location[0]);
            Canvas.SetLeft(game.Rooms[0].Area, game.Player.Location[1]);

            canvas.Children.Add(game.Rooms[0].SpawnMaps[0].Portal.Body.Mesh);
            Canvas.SetTop(game.Rooms[0].SpawnMaps[0].Portal.Body.Mesh, game.Rooms[0].SpawnMaps[0].Portal.Location[0]);
            Canvas.SetLeft(game.Rooms[0].SpawnMaps[0].Portal.Body.Mesh, game.Rooms[0].SpawnMaps[0].Portal.Location[1]);

            SetUpPlayer();
            SetUpEnemy();
        }
        private static void SetUpPlayer()
        {
            AddToCanvas(game.Player.Body.Mesh,game.Player.Location[0],game.Player.Location[1],1);
        }
        private static void SetUpEnemy()
        {
            foreach (var enemy in game.Rooms[0].SpawnMaps[0].Swifters)
            {
                AddToCanvas(enemy.Body.Mesh, enemy.Location[0], enemy.Location[1]);
            }
            foreach (Shooter shooter in game.Rooms[0].SpawnMaps[0].Shooters)
            {
                AddToCanvas(shooter.Body.Mesh, shooter.Location[0], shooter.Location[1]);
            }
        }

        public static void AddToCanvas(UIElement uiElement,double y,double x)
        {
            Canvas.SetTop(uiElement, y);
            Canvas.SetLeft(uiElement, x);
            canvas.Children.Add(uiElement);
        }
        public static void AddToCanvas(UIElement uiElement,double y,double x,int z)
        {
            Canvas.SetTop(uiElement, y);
            Canvas.SetLeft(uiElement, x);
            canvas.Children.Add(uiElement);

            Canvas.SetZIndex(uiElement, z);
        }
    }
}
