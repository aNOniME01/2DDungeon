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
    internal class PortalBody : IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }

        public PortalBody( double[] location)
        {
            //entity width
            double width = 10;

            //entity height
            double height = 15;

            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Portal.png"));//shooter body texture

            Mesh = new Rectangle();
            Mesh.Width = width;
            Mesh.Height = height;
            Mesh.Stroke = Brushes.Black;
            Mesh.Fill = Texture;

            Canvas.SetTop(Mesh, location[0]);
            Canvas.SetLeft(Mesh, location[1]);

            MoveHitbox();
        }
        public void FaceTo(char faceing)
        {
            if (faceing == 'T' || faceing == 'B')
            {
                Mesh.Height = 15;
                Mesh.Width = 10;
            }
            else
            {
                Mesh.Height = 10;
                Mesh.Width = 15;
            }
            MoveHitbox();

            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (faceing == 'T') aRotateTransform.Angle = 0;
            else if (faceing == 'B') aRotateTransform.Angle = 180;
            else if (faceing == 'L') aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;
            Texture.RelativeTransform = aRotateTransform;
        }
        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh), Canvas.GetTop(Mesh), Mesh.Width, Mesh.Height);
    }
}
