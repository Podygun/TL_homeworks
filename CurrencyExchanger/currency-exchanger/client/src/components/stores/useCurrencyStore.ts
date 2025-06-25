import { create } from 'zustand';
import { fetchRates } from '../../api/currencyApi';
import { PriceEntry } from '../Types';

interface CurrencyStore {
  ratesData: PriceEntry[];
  loading: boolean;
  error: string | null;
  fetchExchangeData: (baseCurrency: string, targetCurrency: string) => Promise<void>;
  getLatestRate: () => { price: number; dateTime: string } | null;
}

export const useCurrencyStore = create<CurrencyStore>((set, get) => ({
  ratesData: [],
  loading: false,
  error: null,

  fetchExchangeData: async (baseCurrency, targetCurrency) => {
    if (!baseCurrency || !targetCurrency) return;

    set({ loading: true, error: null });

    try {
      const fromDate = new Date(Date.now() - 24 * 60 * 60 * 1000).toISOString();
      const rates = await fetchRates(baseCurrency, targetCurrency, fromDate);

      set({
        ratesData: rates,
        loading: false
      });
    } catch (error) {
      set({
        error: error instanceof Error ? error.message : 'Failed to fetch exchange data',
        loading: false
      });
    }
  },

  getLatestRate() {
    const { ratesData } = get();
    if (ratesData.length === 0) return null;
    
    const latest = ratesData.at(-1)!;
    return { 
      price: latest.price, 
      dateTime: latest.dateTime 
    };
  }
}));
