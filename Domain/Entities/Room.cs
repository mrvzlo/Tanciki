using Domain.Enums;

namespace Domain.Entities
{
    public class Room
    {
        public string Id { get; set; }
        public string Players { get; set; }
        public string DateCreated { get; set; }
        public GameStateType GameState { get; set; }
    }
}
