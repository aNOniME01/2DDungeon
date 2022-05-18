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
        private static Game game;
        private static List<Rect> barrierList = new List<Rect>();
        public static void GameLoad(Game gm)
        {
            game = gm;

            barrierList.Add(new Rect(0,0,465,20));
            barrierList.Add(new Rect(0,442,465,20));
            barrierList.Add(new Rect(0,20,20,422));
            barrierList.Add(new Rect(465,0,20,462));

            game.Rooms[0].ChangeLocation(100, 150);
            foreach (Hallway hallway in game.Hallways)
            {
                hallway.ToRoomLoc(game.Rooms[0].Location);
            }
        }
        public static void GameLoop(bool mUp, bool mDown, bool mLeft, bool mRight, double wWidth, double wHeight)
        {
            #region PlayerLogic
            //player movement
            PlayerMovement(mUp, mDown, mLeft, mRight,wWidth,wHeight);

            //bullet navigation
            PBulletNavigation();

            //bullet deleting
            PBulletDeleteing();

            #endregion

            //Shooter logic
            ShooterLogic();

            //Swifter logic
            SwifterLogic(wWidth,wHeight);

        }
        private static void PlayerMovement( bool mUp, bool mDown, bool mLeft, bool mRight, double wWidth, double wHeight)
        {
            if (mUp && PlayerMoveCheck('T'))
            {
                game.Player.FaceTo('T');
                game.Player.AddToLocation(-2, 0);
            }
            if (mDown && PlayerMoveCheck('B'))
            {
                game.Player.FaceTo('B');
                game.Player.AddToLocation(2, 0);
            }
            if (mLeft && PlayerMoveCheck('L'))
            {
                game.Player.FaceTo('L');
                game.Player.AddToLocation(0, -2);
            }
            if (mRight && PlayerMoveCheck('R'))
            {
                game.Player.FaceTo('R');
                game.Player.AddToLocation(0, 2);
            }
        }
        /// <summary>
        /// Navigates the bullet
        /// </summary>
        /// <param name="game">Game object, contains all the game elements (rooms,hallways,player,enemies)</param>
        private static void PBulletNavigation()
        {
            List<Shooter> shDeleteNeeded = new List<Shooter>();
            List<Swifter> swDeleteNeeded = new List<Swifter>();
            Bullet? bHit = null;
            foreach (Bullet bullet in game.Player.Bullets)
            {
                bullet.Navigate();
                Render.RefreshEntity(bullet);
                foreach (Room room in game.Rooms)
                {
                    foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
                    {
                        if (shooter.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                        {
                            Render.RemoveEntity(shooter);
                            shDeleteNeeded.Add(shooter);
                            foreach (Bullet eBullet in shooter.Bullets)
                            {
                                Render.RemoveEntity(bullet);
                            }
                            shooter.Bullets.Clear();
                            bHit = bullet;
                        }
                    }
                    foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                    {
                        if (swifter.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                        {
                            Render.RemoveEntity(swifter);
                            swDeleteNeeded.Add(swifter);
                            bHit = bullet;
                        }
                    }

                    foreach (Shooter shooter in shDeleteNeeded)
                    {
                        room.SelectedSpawnMap.DeleteShooter(shooter);
                    }
                    foreach (Swifter swifter in swDeleteNeeded)
                    {
                        room.SelectedSpawnMap.DeleteSwifter(swifter);
                    }
                    if (bHit != null)
                    {
                        Render.RemoveEntity(bHit);
                        game.Player.DeleteBullet(bHit);
                    }

                }
            }       

        }
        private static void PBulletDeleteing()
        {
            List<Bullet> pbDeleteNeeded = new List<Bullet>();
            foreach (Bullet bullet in game.Player.Bullets)
            {
                if (!isBulletInside(bullet))
                {
                    Render.RemoveEntity(bullet);
                    pbDeleteNeeded.Add(bullet);
                }
            }
            foreach (Bullet bullet in pbDeleteNeeded)//<------Deletes the bullets
            {
                game.Player.DeleteBullet(bullet);
            }
        }
        private static void ShooterLogic()
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
                {
                    List<Bullet> bulletDeleteNeeded = new List<Bullet>();

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
                        //navigates the bullet and refreshes the bullet loc
                        bullet.Navigate();

                        if (bullet.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
                        {
                            //game over
                        }

                        if (!isBulletInside(bullet))
                        {
                            bulletDeleteNeeded.Add(bullet);
                            Render.RemoveEntity(bullet);
                        }
                    }

                    foreach (Bullet bullet in bulletDeleteNeeded)
                    {
                        shooter.DeleteBullet(bullet);
                    }

                }
            }

            if (shootTimer > 50) shootTimer = 0;
            shootTimer++;
        }
        private static void SwifterLogic( double wWidth,double wHeight)
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                {
                    swifter.Navigate(SwifterMoveCheck(swifter.Faceing));
                }
            }
        }
        private static bool PlayerMoveCheck(char dir)
        {
            foreach (Room room in game.Rooms)
            {
                Rect hitbox = room.Body.Hitbox;

                if (dir == 'T' && game.Player.MoveChecks[0].Check(hitbox)) return true;
                else if (dir == 'B' && game.Player.MoveChecks[1].Check(hitbox)) return true;
                else if (dir == 'L' && game.Player.MoveChecks[2].Check(hitbox)) return true;
                else if(dir == 'R' && game.Player.MoveChecks[3].Check(hitbox)) return true;
            }

            foreach (Hallway hallway in game.Hallways)
            {
                Rect hitbox = hallway.Body.Hitbox;

                if (dir == 'T' && game.Player.MoveChecks[0].Check(hitbox)) return true;
                else if (dir == 'B' && game.Player.MoveChecks[1].Check(hitbox)) return true;
                else if (dir == 'L' && game.Player.MoveChecks[2].Check(hitbox)) return true;
                else if(dir == 'R' && game.Player.MoveChecks[3].Check(hitbox)) return true;
            }

            return false;
        }
        private static bool SwifterMoveCheck(char dir)
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                {
                    foreach (Room CheckRoom in game.Rooms)
                    {
                        Rect hitbox = CheckRoom.Body.Hitbox;

                        if (dir == 'T' && game.Player.MoveChecks[0].Check(hitbox)) return true;
                        else if (dir == 'B' && swifter.MoveChecks[1].Check(hitbox)) return true;
                        else if (dir == 'L' && swifter.MoveChecks[2].Check(hitbox)) return true;
                        else if (dir == 'R' && swifter.MoveChecks[3].Check(hitbox)) return true;
                    }
                }
            }
            return false;
        }
        private static bool isBulletInside(Bullet bullet)
        {
            foreach (Room room in game.Rooms)
            {
                if (bullet.Body.Hitbox.IntersectsWith(room.Body.Hitbox)) return true;
            }
            foreach (Hallway hallway in game.Hallways)
            {
                if (bullet.Body.Hitbox.IntersectsWith(hallway.Body.Hitbox)) return true;
            }

            return false;
        }
    }
}
