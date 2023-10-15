using System;

namespace WPFDungeon
{
    internal class Logic
    {
        public static Random rnd = new Random();
        public static double ToPositive(double x) => Math.Sqrt(x*x);
        public static Direction RotateFaceing90(Direction facing)
        {
            if (facing == Direction.Top) return Direction.Right;
            else if (facing == Direction.Right) return Direction.Bottom;
            else if (facing == Direction.Bottom) return Direction.Left;
            else return Direction.Top;
        }

        public static Direction RandomFaceing()
        {
            Array dir = Enum.GetValues(typeof(Direction));
            return (Direction)dir.GetValue(rnd.Next(dir.Length));
        }

        public static Direction RotateFaceingWithRoom(Direction roomFaceing,Direction elementFacing)
        {
            Direction newFacing = elementFacing;
            if (roomFaceing == Direction.Bottom)
            {
                if (elementFacing == Direction.Top) newFacing = Direction.Bottom;
                else if (elementFacing == Direction.Bottom) newFacing = Direction.Top;
                else if (elementFacing == Direction.Left) newFacing = Direction.Right;
                else newFacing = Direction.Left;
            }
            else if (roomFaceing == Direction.Left)
            {
                if (elementFacing == Direction.Top) newFacing = Direction.Left;
                else if (elementFacing == Direction.Bottom) newFacing = Direction.Right;
                else if (elementFacing == Direction.Left) newFacing = Direction.Bottom;
                else newFacing = Direction.Top;
            }
            else if (roomFaceing == Direction.Right)
            {
                if (elementFacing == Direction.Top) newFacing = Direction.Right;
                else if (elementFacing == Direction.Bottom) newFacing = Direction.Left;
                else if (elementFacing == Direction.Left) newFacing = Direction.Top;
                else newFacing = Direction.Bottom;
            }
            return newFacing;
        }
    }
}
