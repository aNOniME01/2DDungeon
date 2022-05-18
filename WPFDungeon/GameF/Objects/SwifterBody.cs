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
        public SwifterBody(double[] location, char faceing)
        {
            double width = 10;

            double height = 20;

            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Swifter.png"));//shooter body texture

            FaceTo(faceing);

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
            Mesh.Stroke = Brushes.Black;
            Mesh.Fill = Texture;
            Canvas.SetTop(Mesh, location[0]);
            Canvas.SetLeft(Mesh, location[1]);

            MoveHitbox();
        }
        public void FaceTo(char faceing)
        {
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
