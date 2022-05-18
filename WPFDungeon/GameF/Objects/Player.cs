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
    internal class Player:IEntity
    {
        public double[] Location { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public char Faceing { get; private set; }
        public IBody Body { get; private set; }
        public List<MoveCheck> MoveChecks { get; private set; }
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

            Body = new PlayerBody(Location);

            MoveChecks = new List<MoveCheck>();
            RefreshMoveCheck();
        }
        private void RefreshMoveCheck()
        {
            MoveChecks.Clear();
            MoveChecks.Add(new MoveCheck('T',Body));
            MoveChecks.Add(new MoveCheck('B',Body));
            MoveChecks.Add(new MoveCheck('L',Body));
            MoveChecks.Add(new MoveCheck('R',Body));
        }
        public void AddToLocation(double x, double y)
        {
            Location[0] += x;
            Location[1] += y;

            Canvas.SetTop(Body.Mesh, Location[0]);
            Canvas.SetLeft(Body.Mesh, Location[1]);

            (Body as PlayerBody).MoveHitbox();
            RefreshMoveCheck();
        }
        public void FaceTo(char direction)
        {
            Faceing = direction;
            Body.FaceTo(direction);
        }
        public void Shoot() => Bullets.Add(new Bullet("pB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Faceing));
        public void DeleteBullet(Bullet bullet) => Bullets.Remove(bullet);
    }
}
