using Domain.Enums;

namespace Domain.Models
{
    public class ActionResult
    {
        public ActionResultType ResultType { get; set; }
        public Point MoveStart { get; set; }
        public Point MoveEnd { get; set; }
        public DirectionType DirectionStart { get; set; }
        public DirectionType DirectionEnd{ get; set; }

        public ActionResult(WalkingSprite startState, WalkingSprite endState)
        {
            MoveStart = startState.Point;
            DirectionStart = startState.Direction;
            MoveEnd = endState.Point;
            DirectionEnd = startState.Direction;
        }
    }
}
