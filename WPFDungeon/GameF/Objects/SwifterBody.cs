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
    internal class SwifterBody:IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }

        public double HitboxGap { get; private set; }

        public SwifterBody(double[] location, char faceing)
        {
            HitboxGap = 2;

            double width = 10;

            double height = 20;

            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Swifter.png"));//shooter body texture


            Mesh = new Rectangle();
            if (faceing == 'T'|| faceing == 'B')
            {
                Mesh.Width = width;
                Mesh.Height = height;
            }
            else
            {
                Mesh.Width = height;
                Mesh.Height = width;
            }
            Mesh.Fill = Texture;

            FaceTo(faceing);

            Render.RefreshElement(Mesh, location);

            MoveHitbox();
        }
        public void FaceTo(char faceing)
        {
            if (faceing == 'T' || faceing == 'B')
            {
                Mesh.Height = 20;
                Mesh.Width = 10;
            }
            else
            {
                Mesh.Height = 10;
                Mesh.Width = 20;
            }

            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (faceing == 'T') aRotateTransform.Angle = 0;
            else if (faceing == 'B') aRotateTransform.Angle = 180;
            else if (faceing == 'L') aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;
            Texture.RelativeTransform = aRotateTransform;

        }
        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh) + HitboxGap, Canvas.GetTop(Mesh) + HitboxGap, Mesh.Width - HitboxGap, Mesh.Height - HitboxGap);
    }
}
