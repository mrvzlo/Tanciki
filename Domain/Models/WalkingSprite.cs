using System;
using Domain.Enums;

namespace Domain.Models
{
    public abstract class WalkingSprite
    {
        public Point Point { get; set; }
        public DirectionType Direction { get; set; }
        public DateTime LastUpdate { get; set; }
        public WalkingObjectState State { get; set; }

        protected WalkingSprite() { }

        protected WalkingSprite(Point point)
        {
            Point = point;
            Direction = point.Y == 0 ? DirectionType.Bottom : DirectionType.Top;
        }

        public void Move()
        {
            Point = Point.Move(Direction);
            LastUpdate = DateTime.Now;
        }

        public void RotateLeft()
        {
            Direction = IntToDirection((int)Direction - 1);
            LastUpdate = DateTime.Now;
        }

        public void RotateRight()
        {
            Direction = IntToDirection((int)Direction + 1);
            LastUpdate = DateTime.Now;
        }

        private DirectionType IntToDirection(int dir) => (DirectionType)((dir + RotationsCount) % RotationsCount);

        private int RotationsCount => Enum.GetNames(typeof(DirectionType)).Length;

        public bool CanMove() => IsAlive() && LastUpdate.AddSeconds(Consts.MovementRecharge) <= DateTime.Now;
        public bool IsAlive() => State == WalkingObjectState.Alive;
    }
}
