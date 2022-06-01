using System.Collections.Generic;

namespace WPFDungeon
{
    internal class Swifter:IEntity
    {
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }
        public IBody Body { get; private set; }
        public int RoomId { get; private set; }
        public List<MoveCheck> MoveChecks { get; private set; }

        public Swifter(double yLoc,double xLoc,char faceing,int roomId)
        {
            RoomId = roomId;
            Location = new double[2];

            Location[0] = yLoc;
            Location[1] = xLoc;

            Faceing = faceing;

            Body = new SwifterBody(Location,Faceing);

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
            if (Faceing == 'T')
            {
                if (!canMove) Faceing = 'B';
                else Location[0] -= speed;
            }
            else if (Faceing == 'B')
            {
                if (!canMove) Faceing = 'T';
                else Location[0] += speed;
            }
            else if (Faceing == 'L')
            {
                if (!canMove) Faceing = 'R';
                else Location[1] -= speed;
            }
            else
            {
                if (!canMove) Faceing = 'L';
                else Location[1] += speed;
            }
            Body.FaceTo(Faceing);

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
        public void FaceTo(char faceing)
        {
            Faceing = faceing;
            (Body as SwifterBody).FaceTo(Faceing);
            RefreshMoveCheck();
        }

    }
}
