using System;
using Domain.Enums;

namespace Domain.Models
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point() {}

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Move(DirectionType direction)
        {
            var point = new Point(X, Y);
            switch (direction)
            {
                case DirectionType.Left:
                    point.X--;
                    break;
                case DirectionType.Top:
                    point.Y--;
                    break;
                case DirectionType.Right:
                    point.X++;
                    break;
                case DirectionType.Bottom:
                    point.Y++;
                    break;
            }

            return point;
        }
    }
}
