using System;
using ApiService.Models.Enums;

namespace ApiService.Models
{
    public class Tank
    {
        public int Id { get; }
        public Point Point { get; }
        public DirectionType Direction { get; private set; }

        public Tank(int id, int x, int y)
        {
            Id = id;
            Point = new Point(x, y);
        }

        private void RotateLeft()
        {
            var newDirection = ((int)Direction + RotationsCount - 1) % RotationsCount;
            Direction = (DirectionType)newDirection;
        }

        private void RotateRight()
        {
            var newDirection = ((int)Direction + 1) % RotationsCount;
            Direction = (DirectionType)newDirection;
        }

        private int RotationsCount => Enum.GetNames(typeof(DirectionType)).Length;
    }
}
