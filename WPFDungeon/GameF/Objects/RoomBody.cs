using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class RoomBody : IBody
    {
        public ImageBrush Texture { get; private set; }

        public Rect Hitbox { get; private set; }

        public Rectangle Mesh { get; private set; }
        public RoomBody(double height,double width)
        {
            Texture = new ImageBrush();
            Hitbox = new Rect(0,0,width,height);
            Mesh = new Rectangle();
            Mesh.Width = width;
            Mesh.Height = height;
            Mesh.Stroke = Brushes.Black;
            Mesh.Fill = Brushes.Green;
        }
        public void FaceTo(char direction)
        {
            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (direction == 'T') aRotateTransform.Angle = 0;
            else if (direction == 'B') aRotateTransform.Angle = 180;
            else if (direction == 'L') aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;

            Texture.RelativeTransform = aRotateTransform;
        }
        public void MoveHitbox(double[] location)
        {
            Hitbox = new Rect(location[1], location[0], Mesh.Width, Mesh.Height);
        }
    }
}
