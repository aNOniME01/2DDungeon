using System.Windows;
using System.Windows.Controls;

namespace WPFDungeon
{
    internal class MoveCheck
    {
        public Rect C1 { get; }
        public Rect C2 { get; }
        public MoveCheck(Direction faceing,IBody body)
        {
            //distance between the hitbox and the checker
            double dis = 2;

            //checker width
            double w = 4;

            //checker height
            double h = 2;

            if (faceing == Direction.Top)
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh), Canvas.GetTop(body.Mesh) - h - dis,w,h);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width - w, Canvas.GetTop(body.Mesh) - dis - h, w, h);
            }
            else if (faceing == Direction.Bottom)
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh), Canvas.GetTop(body.Mesh) + body.Mesh.Height + dis, w, h);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width - w, Canvas.GetTop(body.Mesh) + body.Mesh.Height + dis, w, h);
            }
            else if (faceing == Direction.Left)
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh) - dis - h, Canvas.GetTop(body.Mesh),h,w);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) - dis - h, Canvas.GetTop(body.Mesh) + body.Mesh.Height - w, h, w);
            }
            else if (faceing == Direction.Right)
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width + dis, Canvas.GetTop(body.Mesh), h, w);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width + dis, Canvas.GetTop(body.Mesh) + body.Mesh.Height - w, h, w);
            }
        }
        public bool CheckBoth(Rect hitbox)
        {
            return C1.IntersectsWith(hitbox) && C2.IntersectsWith(hitbox);
        }
        public bool CheckSingle(Rect hitbox)
        {
            return C1.IntersectsWith(hitbox) || C2.IntersectsWith(hitbox);
        }
        
    }
}
