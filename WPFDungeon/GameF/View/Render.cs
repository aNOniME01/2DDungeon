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
        private static Game? game = null;
        public static void Load(Game gm)
        {
            game = gm;

            AddRoomToCanvas(game.Rooms[0]);

            //Draw player
            AddToCanvas(game.Player.Body.Mesh, game.Player.Location[0], game.Player.Location[1], 1);

            //Draw Enemies
            SetUpEnemy(game.Rooms[0]);
        }
        /// <summary>
        /// Draws all enemies onto the canvas
        /// </summary>
        private static void SetUpEnemy(Room room)
        {
            foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
            {
                try
                {
                    AddToCanvas(swifter.Body.Mesh, swifter.Location[0], swifter.Location[1]);
                }
                catch { }
            }
            foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
            {
                try
                {
                    AddToCanvas(shooter.Body.Mesh, shooter.Location[0], shooter.Location[1]);
                }
                catch { }
            }
            foreach (Point point in room.SelectedSpawnMap.Points)
            {
                try
                {
                    AddToCanvas(point.Body.Mesh, point.Location[0], point.Location[1]);
                }
                catch { }
            }
        }
        public static void AddRoomToCanvas(Room room)
        {
            AddToCanvas(room.Body.Mesh, room.Location[0], room.Location[1]);

            foreach (Hallway hallway in game.Hallways)
            {
                try
                {
                    AddToCanvas(hallway.Body.Mesh, hallway.D1.Location[0], hallway.D1.Location[1]);
                }
                catch { }
            }

            SetUpEnemy(room);
        }
        public static void RemoveRoomFromCanvas(Room room)
        {
            try
            {
                game.GCanvas.Children.Remove(room.Body.Mesh);

                game.GCanvas.Children.Remove(room.SelectedSpawnMap.Portal.Body.Mesh);
            }
            catch { }

            foreach (Hallway hallway in game.Hallways)
            {
                try
                {
                    game.GCanvas.Children.Remove(hallway.Body.Mesh);
                }
                catch { }
            }

            foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
            {
                try
                {
                    game.GCanvas.Children.Remove(swifter.Body.Mesh);
                }
                catch { }
            }

            foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
            {
                try
                {
                    game.GCanvas.Children.Remove(shooter.Body.Mesh);
                }
                catch { }
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
        public static void AddEntityToCanvas(IEntity entity)
        {
            Canvas.SetTop(entity.Body.Mesh, entity.Location[0]);
            Canvas.SetLeft(entity.Body.Mesh, entity.Location[1]);
            game.GCanvas.Children.Add(entity.Body.Mesh);
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
        public static void RemoveEntity(IEntity entity)
        {
            game.GCanvas.Children.Remove(entity.Body.Mesh);
        }
        public static void RemoveElement(UIElement uIElement)
        {
            game.GCanvas.Children.Remove(uIElement);
        }
    }
}
