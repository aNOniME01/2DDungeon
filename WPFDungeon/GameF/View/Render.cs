using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    class Render
    {
        private static Game game = null;
        public static void Load(Game gm)
        {
            game = gm;

            AddToCanvas(game.Rooms[0].Body.Mesh, game.Rooms[0].Location[0], game.Rooms[0].Location[1]);

            AddToCanvas(game.Rooms[0].SpawnMaps[0].Portal.Body.Mesh, game.Rooms[0].SpawnMaps[0].Portal.Location[0],game.Rooms[0].SpawnMaps[0].Portal.Location[1]);

            foreach (Hallway hallway in game.Hallways)
            {
                AddToCanvas(hallway.Body.Mesh, hallway.D1.Location[0], hallway.D1.Location[1]);
            }

            //Draw player
            AddToCanvas(game.Player.Body.Mesh, game.Player.Location[0], game.Player.Location[1], 1);

            //Draw Enemies
            SetUpEnemy();
        }
        /// <summary>
        /// Draws all enemies onto the canvas
        /// </summary>
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
        /// <summary>
        /// Adds a UIElement to the canvas (Layer = 0)
        /// </summary>
        /// <param name="uiElement">Element</param>
        /// <param name="y">Y cordinate</param>
        /// <param name="x">X cordinate</param>
        public static void AddToCanvas(UIElement uiElement,double y,double x)
        {
            Canvas.SetTop(uiElement, y);
            Canvas.SetLeft(uiElement, x);
            game.GCanvas.Children.Add(uiElement);
        }
        /// <summary>
        /// Adds a UIElement to the canvas with a layer
        /// </summary>
        /// <param name="uiElement">Element</param>
        /// <param name="y">Y cordinate</param>
        /// <param name="x">X cordinate</param>
        /// <param name="z">Layer</param>
        public static void AddToCanvas(UIElement uiElement,double y,double x,int z)
        {
            Canvas.SetTop(uiElement, y);
            Canvas.SetLeft(uiElement, x);
            game.GCanvas.Children.Add(uiElement);

            Canvas.SetZIndex(uiElement, z);
        }
        /// <summary>
        /// Refreshes the entity location on the canvas
        /// </summary>
        /// <param name="entity">Entity</param>
        public static void RefreshEntity(IEntity entity)
        {
            Canvas.SetTop(entity.Body.Mesh, entity.Location[0]);
            Canvas.SetLeft(entity.Body.Mesh, entity.Location[1]);
        }
        public static void RefreshElement(UIElement element,double[] Loc)
        {
            Canvas.SetTop(element, Loc[0]);
            Canvas.SetLeft(element, Loc[1]);
        }
    }
}
