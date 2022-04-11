using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class SwifterLooks
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public SwifterLooks(double height, double width, double yLoc, double xLoc, char faceing)
        {
            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Swifter.png"));//shooter body texture

            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (faceing == 'T') aRotateTransform.Angle = 0;
            else if (faceing == 'B') aRotateTransform.Angle = 180;
            else if (faceing == 'L') aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;
            Texture.RelativeTransform = aRotateTransform;


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

            Hitbox = new Rect(xLoc, yLoc, Mesh.Width, Mesh.Height);
        }
    }
}
