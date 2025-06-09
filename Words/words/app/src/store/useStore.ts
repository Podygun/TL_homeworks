import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { DictionaryStore } from "./DictionaryStore";

export const useStore = create<DictionaryStore>()(
  persist(
    (set) => ({
      pairs: [],

      add: (pair) => {
        set((state) => {
          const isDuplicate = state.pairs.some(
            (word) =>
              word.ru.trim().toLowerCase() === pair.ru.trim().toLowerCase() &&
              word.en.trim().toLowerCase() === pair.en.trim().toLowerCase()
          );

          return isDuplicate ? state : { pairs: [...state.pairs, pair] };
        });
      },

      remove: (pair) => {
        set((state) => ({
          pairs: state.pairs.filter(
            (word) =>
              !(
                word.ru === pair.ru &&
                word.en === pair.en
              )
          ),
        }));
      },

      edit: (oldPair, newPair) => {
        set((state) => ({
          pairs: state.pairs.map((word) =>
            word.ru === oldPair.ru &&
            word.en === oldPair.en
              ? newPair
              : word
          ),
        }));
      },
    }),
    {
      name: "dictionary-data"
    }
  )
);