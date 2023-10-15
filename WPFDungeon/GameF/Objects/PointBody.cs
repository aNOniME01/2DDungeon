using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class PointBody : IBody
    {
        public ImageBrush Texture { get; private set; }

        public Rect Hitbox { get; private set; }

        public Rectangle Mesh { get; private set; }

        public double HitboxGap { get; private set; }

        public PointBody(double[] location)
        {
            HitboxGap = 2;

            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Point.png"));

            Mesh = new Rectangle();
            Mesh.Width = 10;
            Mesh.Height = 10;
            Mesh.Fill = Texture;

            Render.RefreshElement(Mesh, location);

            MoveHitbox();
        }

        public void FaceTo(Direction direction)
        {
            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (direction == Direction.Top) aRotateTransform.Angle = 0;
            else if (direction == Direction.Bottom) aRotateTransform.Angle = 180;
            else if (direction == Direction.Left) aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;

            Texture.RelativeTransform = aRotateTransform;
        }

        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh) + HitboxGap, Canvas.GetTop(Mesh) + HitboxGap, Mesh.Width - HitboxGap, Mesh.Height - HitboxGap);
    }
}
