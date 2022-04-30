<template>
   <div>
      <button class="button-78" v-if="!room" v-on:click="getRoom()">Give me a room</button>
      <div v-if="room">
         <div>Room id is {{ room.id }}</div>
         <div>Mates are:</div>
         <div v-for="player in room.players" v-bind:key="player">{{ player }}</div>
      </div>
   </div>
   <canvas :style="'width:' + canvasSize + 'px'" id="canvas"></canvas>
   <div></div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import RoomApiService from '../data-access/room.api.service';
import RoomHubService from '../data-access/room.hub.service';
import Room from '../interfaces/room';
import GameMap from '../interfaces/map';
import MapDrawer from '../drawers/map.drawer';
import { GameStateType } from '@/enums/game-state.type';
import { ActionType } from '@/enums/action.type';
import Tank from '@/interfaces/tank';
import ActionResult from '@/interfaces/action-result';
import { ActionResultType } from '@/enums/action-result.type';
import { Direction } from '@/enums/direction.type';

@Options({
   props: {
      id: String,
   },
})
export default class Game extends Vue {
   roomService!: RoomApiService;
   hubService!: RoomHubService;
   room?: Room | null = null;
   mapLoaded = false;
   map?: GameMap | null = null;
   id!: string;
   tank?: Tank | null = null;
   canvasSize = 0;
   drawer!: MapDrawer;

   perf1 = 0;
   perf2 = 0;

   created() {
      this.hubService = new RoomHubService(this.id);
      this.hubService.subscribeRoomUpdate((room: Room) => {
         this.room = room;
         this.checkMap();
      });
      this.hubService.subscribeMapUpdate((map: GameMap) => {
         this.map = map;
         this.tank = map.tanks.find((x) => x.playerId === this.id);
         this.drawer.drawMap(this.map);
      });
      this.hubService.subscribeActionResults((results: ActionResult[]) => {
         results.forEach((x) => {
            this.perf2 = performance.now();
            console.log(this.perf2 - this.perf1, 'ms');
            this.applyActionResult(x);
            //this.drawer.drawResult(x);
         });
      });
      const maxWidth = window.innerWidth;
      const maxHeight = window.innerHeight - 50;
      this.canvasSize = maxWidth > maxHeight ? maxHeight * 0.8 : maxWidth;
   }

   mounted() {
      this.drawer = new MapDrawer('canvas');
      this.setKeyBinds();
   }

   setKeyBinds() {
      window.addEventListener('keyup', (event) => this.performAction(event.code), true);
   }

   applyActionResult(action: ActionResult) {
      if (!this.tank) return;
      switch (action.resultType) {
         case ActionResultType.Move:
         case ActionResultType.Rotate:
            this.tank.direction = action.directionEnd;
            this.tank.point = action.moveEnd;
            this.drawer.drawChange(action);
            break;
      }
   }

   performAction(code: string) {
      if (this.room?.gameState !== GameStateType.Running) return;
      let action = ActionType.None;
      if (code == 'KeyA') action = ActionType.TurnLeft;
      if (code == 'KeyD') action = ActionType.TurnRight;
      if (code == 'KeyW') action = ActionType.Move;
      if (code == 'Space') action = ActionType.Shoot;
      if (action === ActionType.None) return;
      this.hubService.sendAction(this.room.id, action);
      this.perf1 = performance.now();
   }

   checkMap() {
      if (this.room?.gameState !== GameStateType.Running) return;
      this.hubService.getMap(this.room.id);
   }

   getRoom() {
      this.hubService.getRoom();
   }
}
</script>
