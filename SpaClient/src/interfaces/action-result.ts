import { ActionResultType } from '@/enums/action-result.type';
import { Direction } from '@/enums/direction.type';
import Point from './point';

export default interface ActionResult {
   resultType: ActionResultType;
   moveStart: Point;
   moveEnd: Point;
   directionStart: Direction;
   directionEnd: Direction;
}
