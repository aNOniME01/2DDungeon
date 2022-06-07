using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    class BulletBody : IBody
    {
        public ImageBrush Texture { get; private set; }
        public Rect Hitbox { get; private set; }
        public Rectangle Mesh { get; private set; }
        public double HitboxGap => throw new System.NotImplementedException();

        public BulletBody(char faceing, double[] location)
        {

            Mesh = new Rectangle();
            if (faceing == 'T' || faceing == 'B')
            {
                Mesh.Width = 2;
                Mesh.Height = 3;
            }
            else
            {
                Mesh.Width = 3;
                Mesh.Height = 2;
            }
            Mesh.Stroke = Brushes.Black;
            Canvas.SetTop(Mesh, location[0]);
            Canvas.SetLeft(Mesh, location[1]);

            SetHitboxLocation();
        }
        public void SetHitboxLocation() => Hitbox = new Rect(Canvas.GetLeft(Mesh), Canvas.GetTop(Mesh), Mesh.Width, Mesh.Height);
    }
}
