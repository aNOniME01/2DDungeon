using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class Door
    {
        public double[] L1 { get; private set; }
        public double[] L2 { get; private set; }
        public char Faceing { get; private set; }
        public Rectangle Mesh { get; set; }
        /// <summary>
        /// Door constructor
        /// </summary>
        /// <param name="x">Distance from the starting point (in one axies)</param>
        /// <param name="faceing">The direction witch the door is faceing</param>
        /// <param name="rHeight">Room height</param>
        /// <param name="rWidth">Room width</param>
        public Door(double x, char faceing, double rHeight, double rWidth)
        {
            L1 = new double[2];
            L2 = new double[2];

            if (faceing == 'T')
            {
                L1[0] = 0;
                L1[1] = x;
            }
            else if (faceing == 'B')
            {
                L1[0] = rHeight;
                L1[1] = x;
            }
            else if (faceing == 'L')
            {
                L1[0] = x;
                L1[1] = 0;
            }
            else
            {
                L1[0] = x;
                L1[1] = rWidth;
            }

            Mesh = new Rectangle();
            Mesh.Stroke = Brushes.Red;

            if (faceing == 'T')
            {
                Mesh.Height = 5;
                Mesh.Width = 15;

                L2[0] = L1[0];
                L2[1] = L1[1] + 15;
            }
            else if (faceing == 'B')
            {
                Mesh.Height = 5;
                Mesh.Width = 15;

                L2[0] = L1[0];
                L2[1] = L1[1] - 15;
            }
            else if (faceing == 'L')
            {
                Mesh.Height = 15;
                Mesh.Width = 5;

                L2[0] = L1[0] - 15;
                L2[1] = L1[1];
            }
            else
            {
                Mesh.Height = 15;
                Mesh.Width = 5;

                L2[0] = L1[0] + 15;
                L2[1] = L1[1];
            }

            Faceing = faceing;

        }
        public Door(Door door)
        {
            L1 = door.L1;
            L2 = door.L2;
            Faceing = door.Faceing;
            Mesh = door.Mesh;
        }
    }
}
