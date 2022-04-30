export default class ResponseModel<T> {
   public model?: T;
   constructor(public success: boolean = false) {}
}
