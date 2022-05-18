using System.Collections.Generic;

namespace WPFDungeon
{
    internal class Swifter:IEntity
    {
        public double[] Location { get; private set; }
        public char Faceing { get; private set; }
        public IBody Body { get; private set; }
        public List<MoveCheck> MoveChecks { get; private set; }

        public Swifter(double yLoc,double xLoc,char faceing)
        {
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
            double speed = 2.5;
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
        public void ToRoomLoc(double[] roomLocation)
        {
            Location[0] += roomLocation[0];
            Location[1] += roomLocation[1];

            Render.RefreshEntity(this);

            (Body as SwifterBody).MoveHitbox();
            RefreshMoveCheck();
        }

    }
}
