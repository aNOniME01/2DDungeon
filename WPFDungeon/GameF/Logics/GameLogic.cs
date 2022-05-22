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
        public static void GameLoad(Game gm)
        {
            game = gm;
            Render.Load(game);


            game.ChangeRoomLocation(game.Rooms[0], 200, 50);

            game.ChangeRoomFaceing(game.Rooms[0], 'L');

            GenerateDungeon();
        }
        public static void GameLoop(bool mUp, bool mDown, bool mLeft, bool mRight)
        {
            #region PlayerLogic
            //player movement
            PlayerMovement(mUp, mDown, mLeft, mRight);

            //bullet navigation
            PBulletNavigation();

            #endregion

            //Shooter logic
            ShooterLogic();

            //Swifter logic
            SwifterLogic();
        }
        /// <summary>
        /// cheks if a player can move into a direction and if yes moves it
        /// </summary>
        /// <param name="mUp">is W pressed</param>
        /// <param name="mDown">is S pressed</param>
        /// <param name="mLeft">is A pressed</param>
        /// <param name="mRight">is D pressed</param>
        private static void PlayerMovement( bool mUp, bool mDown, bool mLeft, bool mRight)
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
                //Bullet navigation
                bullet.Navigate();
                Render.RefreshEntity(bullet);

                //checks if the bullet is colllideing with any enemy
                foreach (Room room in game.Rooms)
                {
                    foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
                    {
                        if (shooter.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                        {
                            Render.RemoveEntity(shooter);
                            foreach (Bullet eBullet in shooter.Bullets)
                            {
                                Render.RemoveEntity(eBullet);
                            }
                            shooter.Bullets.Clear();
                            shDeleteNeeded.Add(shooter);
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
                }
            }

            //deletes entities if they don't needed
            foreach (Room room in game.Rooms)
            {
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

            //checks if the bullet is inside the play area
            List<Bullet> pbDeleteNeeded = new List<Bullet>();
            foreach (Bullet bullet in game.Player.Bullets)
            {
                if (!isBulletInside(bullet))
                {
                    Render.RemoveEntity(bullet);
                    pbDeleteNeeded.Add(bullet);
                }
            }

            //deletes bullets wich are not needed
            foreach (Bullet bullet in pbDeleteNeeded)
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
                            Render.RemoveEntity(bullet);
                            bulletDeleteNeeded.Add(bullet);
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
        private static void SwifterLogic()
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                {
                    swifter.Navigate(SwifterMoveCheck(swifter, swifter.Faceing));
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
                else if (dir == 'R' && game.Player.MoveChecks[3].Check(hitbox)) return true;
            }

            foreach (Hallway hallway in game.Hallways)
            {
                Rect hitbox = hallway.Body.Hitbox;

                if (dir == 'T' && game.Player.MoveChecks[0].Check(hitbox)) return true;
                else if (dir == 'B' && game.Player.MoveChecks[1].Check(hitbox)) return true;
                else if (dir == 'L' && game.Player.MoveChecks[2].Check(hitbox)) return true;
                else if (dir == 'R' && game.Player.MoveChecks[3].Check(hitbox)) return true;
            }

            return false;
        }
        private static bool SwifterMoveCheck(Swifter swifter,char dir)
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Room CheckRoom in game.Rooms)
                {
                    Rect hitbox = CheckRoom.Body.Hitbox;

                    if (dir == 'T' && swifter.MoveChecks[0].Check(hitbox)) return true;
                    else if (dir == 'B' && swifter.MoveChecks[1].Check(hitbox)) return true;
                    else if (dir == 'L' && swifter.MoveChecks[2].Check(hitbox)) return true;
                    else if (dir == 'R' && swifter.MoveChecks[3].Check(hitbox)) return true;
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
        private static void GenerateDungeon()
        {
            int count = 0;
            while (count < 20 && count < game.Hallways.Count)
            {
                int roomTry = 0;
                while (!game.AddRoom($"R{Logic.rnd.Next(1, 6)}", game.Hallways[count]) && roomTry < 4) roomTry++;
                if (roomTry <= 4) game.AddRoom($"R6", game.Hallways[count]);
                count++;
            }

            List<Hallway> hallwayDeleteNeeded = new List<Hallway>();
            foreach (Hallway hallway in game.Hallways)
            {
                if (!hallway.IsConnected) hallwayDeleteNeeded.Add(hallway);
            }

            //Deletes unconnectedRooms
            foreach (Hallway hallway in hallwayDeleteNeeded)
            {
                game.Hallways.Remove(hallway);
                Render.RemoveElement(hallway.Body.Mesh);
            }
        }
    }
}
