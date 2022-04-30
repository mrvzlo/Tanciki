import { Guid } from 'guid-ts';
import { getCookie, setCookie } from 'typescript-cookie';

export default class PlayerCookieService {
   private idCookie = 'id';

   public getId(): string {
      let saved = getCookie(this.idCookie);
      if (saved) return saved;

      saved = this.generateId();
      this.setId(saved);
      return saved;
   }

   private setId(id: string) {
      setCookie(this.idCookie, id);
   }

   private generateId(): string {
      return Guid.newGuid().toString();
   }
}
