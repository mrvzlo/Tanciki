using System;
using Domain.Enums;

namespace Domain.Models
{
    public class Tank : WalkingSprite
    {
        public Guid PlayerId { get; set; }
        public DateTime LastShot { get; set; }

        public Tank(){}

        public Tank(Guid playerId, Point point, WalkingObjectState state) : base(point)
        {
            PlayerId = playerId;
            State = state;
        }

        public void Shoot()
        {
            LastShot = DateTime.Now;
        }

        public bool CanShoot() => IsAlive() && LastShot.AddSeconds(Consts.ShootRecharge) <= DateTime.Now;

    }
}
