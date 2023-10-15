using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class HallwayBody : IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public double HitboxGap { get; private set; }

        public HallwayBody(Door D1, Door D2)
        {
            HitboxGap = 2;

            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\R1Texture.png"));

            Mesh = new Rectangle();
            SetFaceing(D1, D2);
            Mesh.Fill = Texture;

            MoveHitbox();
        }
        public void RefreshLocRot(Door D1, Door D2)
        {
            SetFaceing(D1, D2);
            MoveHitbox();
        }
        private void SetFaceing(Door D1, Door D2)
        {
            if (D1.Facing == Direction.Top)
            {
                Mesh.Width = 15;
                Mesh.Height = Logic.ToPositive(D1.Location[0] - D2.Location[0]);
                D1.Location[0] -= D1.Location[0] - D2.Location[0];

                Render.RefreshElement(Mesh, D2.Location);
            }
            else if (D1.Facing == Direction.Bottom)
            {
                Mesh.Width = 15;
                Mesh.Height = Logic.ToPositive(D2.Location[0] - D1.Location[0]);

                Render.RefreshElement(Mesh, D1.Location);
            }
            else if (D1.Facing == Direction.Left)
            {
                Mesh.Width = Logic.ToPositive(D1.Location[1] - D2.Location[1]);
                Mesh.Height = 15;
                D1.Location[1] -= D1.Location[1] - D2.Location[1];

                Render.RefreshElement(Mesh, D2.Location);
            }
            else
            {
                Mesh.Width = Logic.ToPositive(D2.Location[1] - D1.Location[1]);
                Mesh.Height = 15;

                Render.RefreshElement(Mesh, D1.Location);
            }

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
        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh) + HitboxGap, Canvas.GetTop(Mesh) + HitboxGap, Mesh.Width - HitboxGap, Mesh.Height - HitboxGap);
    }
}
