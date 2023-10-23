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
    internal class ShooterBody:IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }

        public double HitboxGap { get; private set; }

        public ShooterBody( double[] location, Direction faceing,int turretNum)
        {
            HitboxGap = 2;

            //entity width
            double width = 10;

            //entity height
            double height = 10;

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
            FaceTo(faceing);

            Mesh = new Rectangle();
            Mesh.Width = width;
            Mesh.Height = height;
            Mesh.Fill = Texture;


            Canvas.SetTop(Mesh, location[0]);
            Canvas.SetLeft(Mesh, location[1]);

            MoveHitbox();
        }
        public void FaceTo(Direction faceing)
        {
            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (faceing == Direction.Top) aRotateTransform.Angle = 0;
            else if (faceing == Direction.Bottom) aRotateTransform.Angle = 180;
            else if (faceing == Direction.Left) aRotateTransform.Angle = 270;
            else aRotateTransform.Angle = 90;
            Texture.RelativeTransform = aRotateTransform;
        }
        public void MoveHitbox() => Hitbox = new Rect(Canvas.GetLeft(Mesh) + HitboxGap, Canvas.GetTop(Mesh) + HitboxGap, Mesh.Width - HitboxGap, Mesh.Height - HitboxGap);
    }
}
