using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class GameLogic
    {
        private static Process? ConsoleDungeonExe = null;
        private static Game? game;
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
            if (ConsoleDungeonExe == null && !game.gameOver)
            {
                #region PlayerLogic
                PlayerMovement(mUp, mDown, mLeft, mRight);

                PBulletNavigation();

                PlayerIntersectionCheck();

                #endregion

                ShooterLogic();

                SwifterLogic();
            }
            else if(!game.gameOver)
            {
                string[] info = Transfer.ReadInfoFromConsole();

                if (info[0] != "") 
                {
                    game.SetScore(Convert.ToInt32(info[0]));

                    if (info[1] == "F")
                    {
                        game.Over();
                    }

                }
            }
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
                            //AddsToScore
                            game.AddToScore(shooter.TurretNum);

                            //Removes entity and the bullets
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
                            //AddsToScore
                            game.AddToScore(2);

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

                    if (shooter.ShootTime > shooter.ShootTimer)
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

                        if (game.Player != null && bullet.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
                        {
                            game.Over();
                        }

                        if (bullet.Body.Hitbox.IntersectsWith(game.Rooms[0].Body.Hitbox))
                        {
                            Render.RemoveEntity(bullet);
                            bulletDeleteNeeded.Add(bullet);
                        }

                        if (!isBulletInside(bullet))
                        {
                            Render.RemoveEntity(bullet);
                            bulletDeleteNeeded.Add(bullet);
                        }

                        //Checks if an entity is hit by the bullet
                        foreach (Room room1 in game.Rooms)
                        {
                            foreach (Shooter shooter1 in room1.SelectedSpawnMap.Shooters)
                            {
                                if (shooter1 != shooter && shooter1.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                                {
                                    Render.RemoveEntity(bullet);
                                    bulletDeleteNeeded.Add(bullet);
                                }
                            }
                            foreach (Swifter swifter in room1.SelectedSpawnMap.Swifters)
                            {
                                if (swifter.Body.Hitbox.IntersectsWith(bullet.Body.Hitbox))
                                {
                                    Render.RemoveEntity(bullet);
                                    bulletDeleteNeeded.Add(bullet);
                                }
                            }

                        }
                    }

                    foreach (Bullet bullet in bulletDeleteNeeded)
                    {
                        shooter.DeleteBullet(bullet);
                    }

                    shooter.AddToShootTime();
                }
            }
        }
        private static void SwifterLogic()
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                {
                    swifter.Navigate(SwifterMoveCheck(swifter));
                }
            }
        }
        private static bool PlayerMoveCheck(char dir)
        {
            foreach (Room room in game.Rooms)
            {
                Rect hitbox = room.Body.Hitbox;

                if (dir == 'T' && game.Player.MoveChecks[0].CheckBoth(hitbox)) return true;
                else if (dir == 'B' && game.Player.MoveChecks[1].CheckBoth(hitbox)) return true;
                else if (dir == 'L' && game.Player.MoveChecks[2].CheckBoth(hitbox)) return true;
                else if (dir == 'R' && game.Player.MoveChecks[3].CheckBoth(hitbox)) return true;
            }

            foreach (Hallway hallway in game.Hallways)
            {
                Rect hitbox = hallway.Body.Hitbox;

                if (dir == 'T' && game.Player.MoveChecks[0].CheckBoth(hitbox)) return true;
                else if (dir == 'B' && game.Player.MoveChecks[1].CheckBoth(hitbox)) return true;
                else if (dir == 'L' && game.Player.MoveChecks[2].CheckBoth(hitbox)) return true;
                else if (dir == 'R' && game.Player.MoveChecks[3].CheckBoth(hitbox)) return true;
            }

            return false;
        }
        private static void PlayerIntersectionCheck()
        {
            //Portal Check
            if (game.PortalRoom.SelectedSpawnMap.Portal != null && game.PortalRoom.SelectedSpawnMap.Portal.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
            {
                if (ConsoleDungeonExe == null)
                {
                    game.PortalRoom.SelectedSpawnMap.DeletePortal();

                    Transfer.WriteInfoToConsole(game.Score,"T");
                    ConsoleDungeonExe = Process.Start(Transfer.GetLocation() + "\\ConsoleDungeon\\bin\\Debug\\net5.0\\ConsoleDungeon.exe");
                    Thread.Sleep(5000);
                    game.AddExitPortal();
                }
            }

            //Exit protal Check
            if (game.ExitPortal != null && game.ExitPortal.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
            {
                game.DeleteExitPortal();
                game.Exit();
            }

            List<Point> deletPoints = new List<Point>();
            foreach (Room room in game.Rooms)
            {
                //Point Check
                foreach (Point point in room.SelectedSpawnMap.Points)
                {
                    if (game.Player != null && point.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
                    {
                        game.AddToScore(1);
                        deletPoints.Add(point);
                        Render.RemoveEntity(point);
                    }
                }
                foreach (Point point in deletPoints)
                {
                    room.SelectedSpawnMap.Points.Remove(point);
                }


                //Swifter Check 
                foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                {
                    if (game.Player != null && swifter.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
                    {
                        game.Over();
                    }
                }

                //Shooter Check
                foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
                {
                    if (game.Player != null && shooter.Body.Hitbox.IntersectsWith(game.Player.Body.Hitbox))
                    {
                        game.Over();
                    }
                }

            }
        }
        public static void StopConsoleWindow()
        {
            if (ConsoleDungeonExe != null)
            {
                ConsoleDungeonExe.Kill();
                ConsoleDungeonExe = null;
            }
        }
        private static bool SwifterMoveCheck(Swifter swifter)
        {
            bool IsOutside = IsSwifterOutside(swifter);
            bool EntityInFront = IsEntityInfrontOfSwifter(swifter);
            if (!IsOutside && EntityInFront) return true;

            return false;
        }
        private static bool IsSwifterOutside(Swifter swifter)
        {
            foreach (Room room in game.Rooms)
            {
                if (room!= game.Rooms[0])
                {
                    Rect hitbox = room.Body.Hitbox;

                    if (swifter.Faceing == 'T' && swifter.MoveChecks[0].CheckBoth(hitbox)) return false;
                    else if (swifter.Faceing == 'B' && swifter.MoveChecks[1].CheckBoth(hitbox)) return false;
                    else if (swifter.Faceing == 'L' && swifter.MoveChecks[2].CheckBoth(hitbox)) return false;
                    else if (swifter.Faceing == 'R' && swifter.MoveChecks[3].CheckBoth(hitbox)) return false;
                }
            }
            foreach (Hallway hallway in game.Hallways)
            {
                Rect hitbox = hallway.Body.Hitbox;

                if (swifter.Faceing == 'T' && swifter.MoveChecks[0].CheckBoth(hitbox)) return false;
                else if (swifter.Faceing == 'B' && swifter.MoveChecks[1].CheckBoth(hitbox)) return false;
                else if (swifter.Faceing == 'L' && swifter.MoveChecks[2].CheckBoth(hitbox)) return false;
                else if (swifter.Faceing == 'R' && swifter.MoveChecks[3].CheckBoth(hitbox)) return false;
            }

            return true;
        }
        private static bool IsEntityInfrontOfSwifter(Swifter swifter)
        {
            foreach (Room room in game.Rooms)
            {
                foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
                {
                    Rect hitbox = shooter.Body.Hitbox;

                    if (swifter.CheckFront(hitbox)) return true;
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

            Render.AddEntityToCanvas(game.PortalRoom.SelectedSpawnMap.Portal);


            foreach (Room room in game.Rooms)
            {
                List<Shooter> shDelete = new List<Shooter>();
                foreach (Shooter shooter in room.SelectedSpawnMap.Shooters)
                {
                    if (!IsEntityOutside(shooter))
                    {
                        Render.RemoveEntity(shooter);
                        shDelete.Add(shooter);
                    }
                }
                foreach (Shooter shooter in shDelete)
                {
                    room.SelectedSpawnMap.Shooters.Remove(shooter);
                }

                List<Swifter> swDelete = new List<Swifter>();
                foreach (Swifter swifter in room.SelectedSpawnMap.Swifters)
                {
                    if (!IsEntityOutside(swifter))
                    {
                        Render.RemoveEntity(swifter);
                        swDelete.Add(swifter);
                    }
                }
                foreach (Swifter swifter in swDelete)
                {
                    room.SelectedSpawnMap.Shooters.Remove(swifter);
                }
                
            }

            if (!IsEntityOutside(game.PortalRoom.SelectedSpawnMap.Portal))
            {
                game.PortalRoom.SelectedSpawnMap.Portal.ToRoomCenter(game.PortalRoom);
            }

        }
        private static bool IsEntityOutside(IEntity entity)
        {
            foreach (Room room in game.Rooms)
            {
                if (entity.Body.Hitbox.IntersectsWith(room.Body.Hitbox))
                {
                    return true;
                }
            }
            foreach (Hallway hallway in game.Hallways)
            {
                if (entity.Body.Hitbox.IntersectsWith(hallway.Body.Hitbox))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
