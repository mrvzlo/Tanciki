import Tank from './tank';
import Preset from './preset';

export default interface GameMap {
   preset: Preset;
   tanks: Tank[];
}
