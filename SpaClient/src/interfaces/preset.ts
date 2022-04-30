import { TileType } from '@/enums/tile.type';

export default interface Preset {
   size: number;
   tiles: TileType[][];
}
