import type { WordsPair } from '../types/Pair';

export interface DictionaryStore {
  pairs: WordsPair[];
  add: (pair: WordsPair) => void;
  remove: (pair: WordsPair) => void;
  edit: (oldPair: WordsPair, newPair: WordsPair) => void;
}