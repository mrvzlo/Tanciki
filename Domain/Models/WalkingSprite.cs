using System;
using Domain.Enums;

namespace Domain.Models
{
    public abstract class WalkingSprite
    {
        public Point Point { get; set; }
        public DirectionType Direction { get; set; }

        protected WalkingSprite(){}

        protected WalkingSprite(Point point)
        {
            Point = point;
            Direction = point.Y == 0 ? DirectionType.Bottom : DirectionType.Top;
        }

        protected void RotateLeft()
        {
            var newDirection = ((int)Direction + RotationsCount - 1) % RotationsCount;
            Direction = (DirectionType)newDirection;
        }

        protected void RotateRight()
        {
            var newDirection = ((int)Direction + 1) % RotationsCount;
            Direction = (DirectionType)newDirection;
        }

        private int RotationsCount => Enum.GetNames(typeof(DirectionType)).Length;
    }
}
