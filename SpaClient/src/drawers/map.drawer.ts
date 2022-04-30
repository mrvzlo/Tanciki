import { Direction } from '@/enums/direction.type';
import { TileType } from '@/enums/tile.type';
import ActionResult from '@/interfaces/action-result';
import GameMap from '@/interfaces/map';
import Point from '@/interfaces/point';
import Tank from '@/interfaces/tank';

export default class MapDrawer {
   context: CanvasRenderingContext2D;
   canvas: HTMLCanvasElement;
   untouched = true;
   size = 1000;
   cellSize = 0;

   constructor(id: string) {
      this.canvas = document.getElementById(id) as HTMLCanvasElement;
      this.context = this.canvas.getContext('2d')!;
      this.context.beginPath();
   }

   drawMap(map: GameMap) {
      if (this.untouched) {
         this.canvas.width = this.size;
         this.canvas.height = this.canvas.width;
         this.context.setTransform(1, 0, 0, 1, 0, 0);
         this.cellSize = this.size / map.preset.size;
      }

      map.preset.tiles.forEach((col, x) =>
         col.forEach((item: TileType, y) => {
            this.drawTile(x, y, item);
         })
      );

      map.tanks.forEach((tank) => this.drawTank(tank));
   }

   drawTile(x: number, y: number, type: TileType) {
      const pos = this.getAbsolutePosition(x, y);
      this.context.fillStyle = type == TileType.Air ? '#787' : '#444';
      this.context.fillRect(pos.x, pos.y, this.cellSize, this.cellSize);
   }

   drawTank(tank: Tank) {
      this.drawTankByPrimitives(tank.point, tank.direction);
   }

   drawChange(actionResult: ActionResult) {
      this.context.fillStyle = '#787';
      console.log(actionResult.moveStart, actionResult.moveEnd);
      const pos = this.getAbsolutePosition(actionResult.moveStart.x, actionResult.moveStart.y);
      this.context.fillRect(pos.x, pos.y, this.cellSize, this.cellSize);
      this.drawTankByPrimitives(actionResult.moveEnd, actionResult.directionEnd);
   }

   drawTankByPrimitives(point: Point, dir: Direction) {
      const pos = this.getAbsolutePosition(point.x, point.y);
      const img = new Image();
      img.onload = () => this.context.drawImage(img, pos.x, pos.y, this.cellSize, this.cellSize);
      img.src = require(`@/assets/tank_${+dir}.png`);
   }

   private getAbsolutePosition(x: number, y: number): Point {
      const left = this.cellSize * x;
      const top = this.cellSize * y;
      return { x: left, y: top } as Point;
   }

   private getAngle(dir: Direction) {
      return dir * 90;
   }
}
