import { create } from 'zustand';
import { fetchRates, getLatestRate } from '../../api/currencyApi';
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
    } catch (err) {
      set({ 
        error: 'Failed to fetch exchange data',
        loading: false,
      });
      console.error(err);
    }
  },

  getLatestRate: () => {
    const { ratesData } = get();
    return getLatestRate(ratesData);
  }
}));