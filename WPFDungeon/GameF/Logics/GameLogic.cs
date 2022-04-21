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
        public static void GameLoop(Game game, bool mUp, bool mDown, bool mLeft, bool mRight, double wWidth, double wHeight)
        {
            #region PlayerLogic
            //player movement
            PlayerMovement(game,mUp, mDown, mLeft, mRight,wWidth,wHeight);

            //bullet navigation
            PBulletNavigation(game);

            //bullet deleting
            PBulletDeleteing(game, wWidth, wHeight);

            #endregion

            //Shooter logic
            ShooterLogic(game);

            //Swifter logic
            SwifterLogic(game,wWidth,wHeight);

        }
        private static void PlayerMovement(Game game, bool mUp, bool mDown, bool mLeft, bool mRight, double wWidth, double wHeight)
        {
            if (mUp && game.Player.Location[0] > 0)
            {
                game.Player.FaceTo('T');
                game.Player.AddToLocation(-2, 0);
                Render.RefreshElement(game.Player);
            }
            if (mDown && game.Player.Location[0] < wHeight)
            {
                game.Player.FaceTo('B');
                game.Player.AddToLocation(2, 0);
                Render.RefreshElement(game.Player);
            }
            if (mLeft && game.Player.Location[1] > 0)
            {
                game.Player.FaceTo('L');
                game.Player.AddToLocation(0, -2);
                Render.RefreshElement(game.Player);
            }
            if (mRight && game.Player.Location[1] < wWidth)
            {
                game.Player.FaceTo('R');
                game.Player.AddToLocation(0, 2);
                Render.RefreshElement(game.Player);
            }
        }
        /// <summary>
        /// Navigates the bullet
        /// </summary>
        /// <param name="game">Game object, contains all the game elements (rooms,hallways,player,enemies)</param>
        private static void PBulletNavigation(Game game)
        {
            List<Shooter> shDeleteNeeded = new List<Shooter>();
            List<Swifter> swDeleteNeeded = new List<Swifter>();
            Bullet bHit = null;
            foreach (Bullet bullet in game.Player.Bullets)
            {
                bullet.Navigate();
                Render.RefreshElement(bullet);
                foreach (Shooter shooter in game.Rooms[0].SpawnMaps[0].Shooters)
                {
                    if (shooter.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                    {
                        game.GCanvas.Children.Remove(shooter.Body.Mesh);
                        shDeleteNeeded.Add(shooter);
                        foreach (Bullet eBullet in shooter.Bullets)
                        {
                            game.GCanvas.Children.Remove(eBullet.Body.Mesh);
                        }
                        shooter.Bullets.Clear();
                        bHit = bullet;
                    }
                }
                foreach (Swifter swifter in game.Rooms[0].SpawnMaps[0].Swifters)
                {
                    if (swifter.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                    {
                        game.GCanvas.Children.Remove(swifter.Body.Mesh);
                        swDeleteNeeded.Add(swifter);
                        bHit = bullet;
                    }
                }
            }       

            foreach (Shooter shooter in shDeleteNeeded)//<------Deletes the bullets
            {
                game.Rooms[0].SpawnMaps[0].DeleteShooter(shooter);
            }
            foreach (Swifter swifter in swDeleteNeeded)//<------Deletes the bullets
            {
                game.Rooms[0].SpawnMaps[0].DeleteSwifter(swifter);
            }
            if (bHit != null)
            {
                game.GCanvas.Children.Remove(bHit.Body.Mesh);
                game.Player.DeleteBullet(bHit);
            }
        }
        private static void PBulletDeleteing(Game game, double wWidth, double wHeight)
        {
            List<Bullet> pbDeleteNeeded = new List<Bullet>();
            foreach (Bullet bullet in game.Player.Bullets)
            {
                if (bullet.Location[0] < 0 || bullet.Location[1] < 0 || bullet.Location[0] > wHeight || bullet.Location[1] > wWidth)
                {
                    game.GCanvas.Children.Remove(bullet.Body.Mesh);
                    pbDeleteNeeded.Add(bullet);
                }
            }
            foreach (Bullet bullet in pbDeleteNeeded)//<------Deletes the bullets
            {
                game.Player.DeleteBullet(bullet);
            }
        }
        private static void ShooterLogic(Game game)
        {
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
                            game.GCanvas.Children.Add(shooter.Bullets[i].Body.Mesh);
                        }
                    }
                    foreach (Bullet bullet in shooter.Bullets)
                    {
                        bullet.Navigate();
                        Canvas.SetTop(bullet.Body.Mesh, bullet.Location[0]);
                        Canvas.SetLeft(bullet.Body.Mesh, bullet.Location[1]);
                        if (bullet.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
                        {

                        }
                    }
                }
                //}

            }

            if (shootTimer > 50) shootTimer = 0;
            shootTimer++;
        }
        private static void SwifterLogic(Game game, double wWidth,double wHeight)
        {
            foreach (Swifter swifter in game.Rooms[0].SpawnMaps[0].Swifters)
            {
                swifter.Navigate(wWidth,wHeight);
                Canvas.SetTop(swifter.Body.Mesh,swifter.Location[0]);
                Canvas.SetLeft(swifter.Body.Mesh,swifter.Location[1]);
            }
        }

    }
}
