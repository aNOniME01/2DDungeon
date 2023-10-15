using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Shooter : IEntity
    {
        public double[] Location { get; private set; }
        public Direction Facing { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public int TurretNum { get; private set; }
        public IBody Body { get; private set; }
        public int RoomId { get; private set; }
        public int ShootTime { get; private set; }
        public int ShootTimer { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public Shooter(double yLoc,double xLoc, int turretNum,Direction facing,int roomId)
        {
            RoomId = roomId;
            Location = new double[2];

            Location[0] = yLoc;
            Location[1] = xLoc;

            Facing = facing;

            Height = 10;
            Width = 10;

            TurretNum = turretNum;
            ShootTime = 0;
            ShootTimer = Logic.rnd.Next(20,51);

            Body = new ShooterBody(Location,Facing,TurretNum);

            Bullets = new List<Bullet>();
        }
        public void Shoot()
        {
            Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Facing));
            if (TurretNum == 2)//T1 = top T2 = right
            {
                if (Facing == Direction.Top) Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Right));
                else if (Facing == Direction.Bottom) Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Left));
                else if (Facing == Direction.Left) Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Top));
                else Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Bottom));
            }
            if (TurretNum == 3)//T1 = top T2 = right T3 = left
            {
                if (Facing == Direction.Top || Facing == Direction.Bottom)
                {
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Right));
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Left));
                }
                else
                {
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Bottom));
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Top));
                }
            }
            if (TurretNum == 4)//all direction
            {
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Top));
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Bottom));
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Left));
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Direction.Right));
            }

            ShootTime = 0;
        }
        public void AddToShootTime()
        {
            ShootTime++;
        }
        public void DeleteBullet(Bullet bullet)
        {
            Bullets.Remove(bullet);
        }
        public void ChangeLocationBy(double y,double x)
        {
            Location[0] += y;
            Location[1] += x;

            Render.RefreshEntity(this);

            (Body as ShooterBody).MoveHitbox();
        }
        public void GoTo(double[] location)
        {
            Location[0] = location[0];
            Location[1] = location[1];

            Render.RefreshEntity(this);

            (Body as ShooterBody).MoveHitbox();

        }
        public void FaceTo(Direction facing)
        {
            Facing = facing;
            (Body as ShooterBody).FaceTo(Facing);
        }
    }
}
