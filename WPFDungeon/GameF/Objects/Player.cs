using System.Collections.Generic;

namespace WPFDungeon
{
    internal class Player : IEntity
    {
        public double[] Location { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public Direction Facing { get; private set; }
        public IBody Body { get; private set; }
        public List<MoveCheck> MoveChecks { get; private set; }
        public List<Bullet> Bullets { get; private set; }

        public Player()
        {
            Location = new double[2];
            Location[0] = 250;
            Location[1] = 70;

            Width = 10;
            Height = 10;

            Facing = Direction.Top;

            

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

            Render.RefreshEntity(this);

            (Body as PlayerBody).MoveHitbox();
            RefreshMoveCheck();
        }
        public void FaceTo(Direction direction)
        {
            Facing = direction;
            Body.FaceTo(direction);
        }
        public void Shoot() => Bullets.Add(new Bullet("pB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Facing));
        public void DeleteBullet(Bullet bullet) => Bullets.Remove(bullet);
    }
}
