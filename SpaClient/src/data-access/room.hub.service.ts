import { ActionType } from '@/enums/action.type';
import ActionResult from '@/interfaces/action-result';
import GameMap from '@/interfaces/map';
import Room from '@/interfaces/room';

export default class RoomHubService {
   signalR = require('@microsoft/signalr');
   connection: any;
   private readonly baseUrl = 'https://localhost:5001/signalr';

   constructor(private id: string) {
      this.connection = new this.signalR.HubConnectionBuilder().withUrl(this.url).build();
      this.connection.start();
   }

   subscribeRoomUpdate(action: (x: Room) => void) {
      this.connection.on('roomUpdate', action);
   }

   subscribeMapUpdate(action: (x: GameMap) => void) {
      this.connection.on('mapUpdate', action);
   }

   subscribeActionResults(action: (x: ActionResult[]) => void) {
      this.connection.on('resolveAction', action);
   }

   async getRoom() {
      await this.connection.invoke('ConnectPlayer', this.id);
   }

   async getMap(room: string) {
      await this.connection.invoke('GetMap', this.id, room);
   }

   async sendAction(room: string, action: ActionType) {
      await this.connection.invoke('PerformAction', this.id, room, action);
   }

   get url() {
      return this.baseUrl + '?connectionId=' + this.id;
   }
}
