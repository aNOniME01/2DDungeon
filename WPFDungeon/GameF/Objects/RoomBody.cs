using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class RoomBody : IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }

        public double HitboxGap { get; private set; }

    public RoomBody(double height,double width, double[] location,string type)
        {
            HitboxGap = 2;
            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + $"WPFDungeon\\textures\\{type}Texture.png"));

            Hitbox = new Rect(0,0,width,height);
            Mesh = new Rectangle();
            Mesh.Width = width;
            Mesh.Height = height;
            Mesh.Fill = Texture;

            Render.RefreshElement(Mesh, location);

            MoveHitbox();
        }
        public void Refresh(double height, double width, double[] location)
        {
            Mesh.Width = width;
            Mesh.Height = height;
            Render.RefreshElement(Mesh, location);
            MoveHitbox();
        }
        public void FaceTo(char direction)
        {
            if (direction == 'L' || direction == 'R')
            {
                double hlpr = Mesh.Width;
                Mesh.Width = Mesh.Height;
                Mesh.Height = hlpr;
            }
            MoveHitbox();

            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (direction == 'T') aRotateTransform.Angle = 0;
            else if (direction == 'B') aRotateTransform.Angle = 180;
            else if (direction == 'L') aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;

            Texture.RelativeTransform = aRotateTransform;
        }
        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh) + HitboxGap, Canvas.GetTop(Mesh) + HitboxGap, Mesh.Width - HitboxGap, Mesh.Height - HitboxGap);
    }
}
