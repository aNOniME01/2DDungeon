using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class PlayerLooks
    {
        public ImageBrush ImageB { get; private set; }
        public Rectangle Body { get; private set; }
        public PlayerLooks(double height, double width)
        {
            ImageB = new ImageBrush();
            ImageB.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Player.png"));
            Body = new Rectangle();
            Body.Width = width;
            Body.Height = height;
            Body.Stroke = Brushes.Black;
            Body.Fill = ImageB;
        }
        public void FaceTo(char direction) 
        {
            RotateTransform aRotateTransform = new RotateTransform();
            aRotateTransform.CenterX = 0.5;
            aRotateTransform.CenterY = 0.5;

            if (direction == 'T')
            {
                aRotateTransform.Angle = 0;
            }
            else if (direction == 'B')
            {
                aRotateTransform.Angle = 180;
            }
            else if (direction == 'L')
            {
                aRotateTransform.Angle = 270;
            }
            else
            {
                aRotateTransform.Angle = 90;
            }

            ImageB.RelativeTransform = aRotateTransform;
        }
    }
}
