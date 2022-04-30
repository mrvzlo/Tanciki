import Room from '@/interfaces/room';
import ResponseModel from '@/models/response-model';

export default class RoomApiService {
   axios = require('axios');
   url = 'https://localhost:5001/room';

   constructor(private id: string) {}

   async getRoom(): Promise<ResponseModel<Room>> {
      const url = this.url + '?playerId=' + this.id;
      try {
         const room = (await this.axios.get(url)).data;
         const response = new ResponseModel<Room>(true);
         response.model = room;
         return response;
      } catch (error) {
         console.log(error);
         return new ResponseModel<Room>();
      }
   }

   updateRoom(): ResponseModel<Room> {
      return new ResponseModel<Room>();
   }

   leaveRoom(): void {
      /** */
   }
}
