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
    internal class ShooterLooks:ITextures
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public ShooterLooks(double height, double width,double yLoc, double xLoc, char faceing,int turretNum)
        {
            Texture = new ImageBrush();
            if (turretNum == 1)
            {
                Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Shooter1.png"));//shooter body texture
            }
            else if (turretNum == 2)
            {
                Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Shooter2.png"));//shooter body texture
            }
            else if (turretNum == 3)
            {
                Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Shooter3.png"));//shooter body texture
            }
            else 
            {
                Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Shooter4.png"));//shooter body texture
            }
            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (faceing == 'T') aRotateTransform.Angle = 0;
            else if (faceing == 'B') aRotateTransform.Angle = 180;
            else if (faceing == 'L') aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;
            Texture.RelativeTransform = aRotateTransform;

            Hitbox = new Rect(xLoc,yLoc,width,height);

            Mesh = new Rectangle();
            Mesh.Width = width;
            Mesh.Height = height;
            Mesh.Stroke = Brushes.Black;
            Mesh.Fill = Texture;
        }
    }
}
