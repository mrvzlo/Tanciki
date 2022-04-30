import { Direction } from '@/enums/direction.type';
import Point from './point';

export default interface Tank {
   point: Point;
   playerId: string;
   direction: Direction;
}
