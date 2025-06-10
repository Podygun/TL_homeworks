import type { WordsPair } from '../types/Pair';

export interface DictionaryStoreType {
  pairs: WordsPair[];
  add: (pair: WordsPair) => void;
  remove: (pair: WordsPair) => void;
  edit: (oldPair: WordsPair, newPair: WordsPair) => void;
}