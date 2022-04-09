using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class GameLogic
    {
        private static int shootTimer = 0;
        public static void GameLoop(Game game, bool mUp, bool mDown, bool mLeft, bool mRight, double wWidth, double wHeight, Canvas canvas)
        {
            Rectangle body = game.Player.Body.Mesh;
            Rect playerHitBox = new Rect(game.Player.Location[0], game.Player.Location[0],game.Player.Width,game.Player.Height);//x,y,w,h

            #region PlayerLogic
            //player movement
            if (mUp && game.Player.Location[0] > 0)
            {
                game.Player.FaceTo('T');
                game.Player.AddToLocation(-2, 0);
                Canvas.SetTop(body, game.Player.Location[0]);
            }
            if (mDown && game.Player.Location[0] < wHeight)
            {
                game.Player.FaceTo('B');
                game.Player.AddToLocation(2, 0);
                Canvas.SetTop(body, game.Player.Location[0]);
            }
            if (mLeft && game.Player.Location[1] > 0)
            {
                game.Player.FaceTo('L');
                game.Player.AddToLocation(0, -2);
                Canvas.SetLeft(body, game.Player.Location[1]);
            }
            if (mRight && game.Player.Location[1] < wWidth)
            {
                game.Player.FaceTo('R');
                game.Player.AddToLocation(0, 2);
                Canvas.SetLeft(body, game.Player.Location[1]);
            }

            //bullet navigation
            List<Shooter> shDeleteNeeded = new List<Shooter>();
            foreach (Bullet bullet in game.Player.Bullets)
            {
                bullet.Navigate();
                Canvas.SetTop(bullet.Mesh, bullet.Location[0]);
                Canvas.SetLeft(bullet.Mesh, bullet.Location[1]);
                foreach (Shooter shooter in game.Rooms[0].SpawnMaps[0].Shooters)
                {
                    if (shooter.Body.Hitbox.IntersectsWith(bullet.Hitbox))
                    {
                        canvas.Children.Remove(shooter.Body.Mesh);
                        shDeleteNeeded.Add(shooter);
                    }
                }
            }
            foreach (Shooter shooter in shDeleteNeeded)//<------Deletes the bullets
            {
                game.Rooms[0].SpawnMaps[0].DeleteShooter(shooter);
            }

            //bullet deleting
            List<Bullet> pbDeleteNeeded = new List<Bullet>();
            foreach (Bullet bullet in game.Player.Bullets)
            {
                if (bullet.Location[0] < 0 || bullet.Location[1] < 0 || bullet.Location[0] > wHeight || bullet.Location[1] > wWidth)
                {
                    canvas.Children.Remove(bullet.Mesh);
                    pbDeleteNeeded.Add(bullet);
                }
            }
            foreach (Bullet bullet in pbDeleteNeeded)//<------Deletes the bullets
            {
                game.Player.DeleteBullet(bullet);
            }
            #endregion

            //Shooter Bullets
            foreach (Room room in game.Rooms)
            {
                //foreach (SpawnMap spawnMap in room.SpawnMaps)
                //{
                foreach (Shooter shooter in room.SpawnMaps[0].Shooters)
                {
                    if (shootTimer > 50)
                    {
                        shooter.Shoot();
                        for (int i = shooter.Bullets.Count - 1; i >= shooter.Bullets.Count - shooter.TurretNum; i--)
                        {
                            canvas.Children.Add(shooter.Bullets[i].Mesh);
                        }
                    }
                    foreach (Bullet bullet in shooter.Bullets)
                    {
                        bullet.Navigate();
                        Canvas.SetTop(bullet.Mesh, bullet.Location[0]);
                        Canvas.SetLeft(bullet.Mesh, bullet.Location[1]);
                    }
                }
                //}
            }

            if (shootTimer > 50) shootTimer = 0;
            shootTimer++;
        }

    }
}
