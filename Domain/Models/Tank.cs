using System;

namespace Domain.Models
{
    public class Tank : WalkingSprite
    {
        public Guid PlayerId { get; set; }

        public Tank(){}

        public Tank(Guid playerId, Point point) : base(point)
        {
            PlayerId = playerId;
        }
    }
}
