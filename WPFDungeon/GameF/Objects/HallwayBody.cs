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
    internal class HallwayBody : IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public HallwayBody(Door D1, Door D2)
        {
            Texture = new ImageBrush();
            Texture.ImageSource = new BitmapImage(new Uri(Transfer.GetLocation() + "WPFDungeon\\textures\\Player.png"));

            Mesh = new Rectangle();
            if (D1.Faceing == 'T')
            {
                Mesh.Width = 15;
                Mesh.Height = Logic.ToPositive(D1.Location[0] - D2.Location[0]);
                D1.Location[0] -= D1.Location[0] - D2.Location[0];
            }
            else if (D1.Faceing == 'B')
            {
                Mesh.Width = 15;
                Mesh.Height = Logic.ToPositive(D2.Location[0] - D1.Location[0]);
            }
            else if (D1.Faceing == 'L')
            {
                Mesh.Width = Logic.ToPositive(D1.Location[1] - D2.Location[1]);
                Mesh.Height = 15;
                D1.Location[1] -= D1.Location[1] - D2.Location[1];
            }
            else
            {
                Mesh.Width = Logic.ToPositive(D2.Location[1] - D1.Location[1]);
                Mesh.Height = 15;
            }
            Mesh.Stroke = Brushes.Black;
            Mesh.Fill = Brushes.Red;

            Hitbox = new Rect(D1.Location[1], D1.Location[0], Mesh.Width, Mesh.Height);
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
        public void MoveHitbox(double[] Loc)
        {
            Hitbox = new Rect(Loc[1], Loc[0], Mesh.Width, Mesh.Height);
        }
    }
}
