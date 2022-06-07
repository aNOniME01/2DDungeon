using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class PlayerBody:IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public double HitboxGap { get; private set; }

        public PlayerBody(double[] location)
        {
            HitboxGap = 2;

            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Player.png"));

            Mesh = new Rectangle();
            Mesh.Width = 10;
            Mesh.Height = 10;
            Mesh.Fill = Texture;

            Canvas.SetTop(Mesh, location[0]);
            Canvas.SetLeft(Mesh, location[1]);

            MoveHitbox();
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
        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh) + HitboxGap, Canvas.GetTop(Mesh)+ HitboxGap, Mesh.Width - HitboxGap, Mesh.Height - HitboxGap);
    }
}
