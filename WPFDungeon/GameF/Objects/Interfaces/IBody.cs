using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal interface IBody
    {
        public ImageBrush Texture { get;}
        public Rect Hitbox { get;}
        public Rectangle Mesh { get;}
        public double HitboxGap { get;}

        public void FaceTo(Direction faceing)
        {
            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (faceing == Direction.Top) aRotateTransform.Angle = 0;
            else if (faceing == Direction.Bottom) aRotateTransform.Angle = 180;
            else if (faceing == Direction.Left) aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;

            Texture.RelativeTransform = aRotateTransform;
        }

    }
}
