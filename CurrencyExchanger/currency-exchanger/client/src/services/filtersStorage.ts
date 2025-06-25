import { LocalStorageService } from './localStorage';

const FILTERS_KEY = 'currencyFilters';

export const FilterStorage = {
  getFilters(): string[] {
    return LocalStorageService.get<string[]>(FILTERS_KEY, []);
  },

  saveFilters(filters: string[]): void {
    LocalStorageService.set(FILTERS_KEY, filters);
  },

  clearFilters(): void {
    LocalStorageService.remove(FILTERS_KEY);
  }
};