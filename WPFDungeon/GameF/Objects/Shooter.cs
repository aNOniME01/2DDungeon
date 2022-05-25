﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDungeon
{
    internal class Shooter : IEntity
    {
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }//it can have multiple turrets
        public double Height { get; private set; }
        public double Width { get; private set; }
        public int TurretNum { get; private set; }
        public IBody Body { get; private set; }
        public int RoomId { get; private set; }
        public int ShootTime { get; private set; }
        public int ShootTimer { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public Shooter(double yLoc,double xLoc, int turretNum,char faceing,int roomId)
        {
            RoomId = roomId;
            Location = new double[2];

            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;

            Height = 10;
            Width = 10;

            TurretNum = turretNum;
            ShootTime = 0;
            ShootTimer = Logic.rnd.Next(20,51);

            Body = new ShooterBody(Location,Faceing,TurretNum);

            Bullets = new List<Bullet>();
        }
        public void Shoot()
        {
            Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Faceing));
            if (TurretNum == 2)//T1 = top T2 = right
            {
                if (Faceing == 'T') Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'R'));
                else if (Faceing == 'B') Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'L'));
                else if (Faceing == 'L') Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'T'));
                else Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'B'));
            }
            if (TurretNum == 3)//T1 = top T2 = right T3 = left
            {
                if (Faceing == 'T' || Faceing == 'B')
                {
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'R'));
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'L'));
                }
                else
                {
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'B'));
                    Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'T'));
                }
            }
            if (TurretNum == 4)//all direction
            {
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'T'));
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'B'));
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'L'));
                Bullets.Add(new Bullet("eB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, 'R'));
            }

            ShootTimer = Logic.rnd.Next(20, 51);
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
        public void ToRoomLoc(double[] roomLocation)
        {
            Location[0] += roomLocation[0];
            Location[1] += roomLocation[1];

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
        public void FaceTo(char faceing)
        {
            Faceing = faceing;
            (Body as ShooterBody).FaceTo(Faceing);
        }
    }
}
