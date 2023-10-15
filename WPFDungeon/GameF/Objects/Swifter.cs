using System.Collections.Generic;
using System.Windows;

namespace WPFDungeon
{
    internal class Swifter:IEntity
    {
        public double[] Location { get; private set; }
        public Direction Facing { get; private set; }
        public IBody Body { get; private set; }
        public int RoomId { get; private set; }
        public List<MoveCheck> MoveChecks { get; private set; }

        public Swifter(double yLoc,double xLoc,Direction facing,int roomId)
        {
            RoomId = roomId;
            Location = new double[2];

            Location[0] = yLoc;
            Location[1] = xLoc;

            Facing = facing;

            Body = new SwifterBody(Location,Facing);

            MoveChecks = new List<MoveCheck>();
            RefreshMoveCheck();
        }
        private void RefreshMoveCheck()
        {
            MoveChecks.Clear();
            MoveChecks.Add(new MoveCheck('T', Body));
            MoveChecks.Add(new MoveCheck('B', Body));
            MoveChecks.Add(new MoveCheck('L', Body));
            MoveChecks.Add(new MoveCheck('R', Body));
        }
        public void Navigate(bool canMove)
        {
            double speed = 1.5;
            if (Facing == Direction.Top)
            {
                if (!canMove) Facing = Direction.Bottom;
                else Location[0] -= speed;
            }
            else if (Facing == Direction.Bottom)
            {
                if (!canMove) Facing = Direction.Top;
                else Location[0] += speed;
            }
            else if (Facing == Direction.Left)
            {
                if (!canMove) Facing = Direction.Right;
                else Location[1] -= speed;
            }
            else
            {
                if (!canMove) Facing = Direction.Left;
                else Location[1] += speed;
            }
            Body.FaceTo(Facing);

            Render.RefreshEntity(this);

            (Body as SwifterBody).MoveHitbox();
            RefreshMoveCheck();
        }
        public void ChangeLocationBy(double y, double x)
        {
            Location[0] += y;
            Location[1] += x;

            Render.RefreshEntity(this);

            (Body as SwifterBody).MoveHitbox();
        }
        public void GoTo(double[] location)
        {
            Location[0] = location[0];
            Location[1] = location[1];

            Render.RefreshEntity(this);

            (Body as SwifterBody).MoveHitbox();

        }
        public void FaceTo(Direction facing)
        {
            Facing = facing;
            (Body as SwifterBody).FaceTo(Facing);
            RefreshMoveCheck();
        }

        public bool CheckFront(Rect hitbox)
        {
            if (Facing == Direction.Top && !MoveChecks[0].CheckSingle(hitbox)) return true;
            else if (Facing == Direction.Bottom && !MoveChecks[1].CheckSingle(hitbox)) return true;
            else if (Facing == Direction.Left && !MoveChecks[2].CheckSingle(hitbox)) return true;
            else if (Facing == Direction.Right && !MoveChecks[3].CheckSingle(hitbox)) return true;

            return false;
        }
    }
}
