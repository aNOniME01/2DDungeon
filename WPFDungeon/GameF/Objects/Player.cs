using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDungeon
{
    internal class Player:IEntity
    {
        public double[] Location { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public char Faceing { get; private set; }
        public IBody Body { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public Player()
        {
            Location = new double[2];
            Location[0] = 100;
            Location[1] = 200;

            Width = 10;
            Height = 10;

            Faceing = 'T';//T,B,L,F

            Bullets = new List<Bullet>();

            Body = new PlayerBody(Location[0], Location[1]);
        }
        public void AddToLocation(double x, double y)
        {
            Location[0] += x;
            Location[1] += y;

        }
        public void FaceTo(char direction)
        {
            Faceing = direction;
            Body.FaceTo(direction);
        }
        public void Shoot()
        {
            Bullets.Add(new Bullet("pB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Faceing));
        }
        public void DeleteBullet(Bullet bullet)
        {
            Bullets.Remove(bullet);
        }
    }
}
