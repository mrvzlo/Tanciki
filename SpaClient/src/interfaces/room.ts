import { GameStateType } from '@/enums/game-state.type';

export default interface Room {
   id: string;
   players: string[];
   gameState: GameStateType;
}
