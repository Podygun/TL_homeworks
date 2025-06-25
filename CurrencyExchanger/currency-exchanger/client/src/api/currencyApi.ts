import { CurrencyInfo, PriceEntry } from "../components/Types";

const BASE_URL = 'http://localhost:5081';

export async function fetchCurrencies(): Promise<CurrencyInfo[]> {
  const res = await fetch(`${BASE_URL}/Currency`);
  if (!res.ok) throw new Error('Failed to fetch currencies');
  return res.json();
}

export function getLatestRate(rates: PriceEntry[]): { price: number; dateTime: string } | null {
  return rates.length > 0 ? rates[rates.length - 1] : null;
}

export async function fetchRates(
  paymentCurrency: string,
  purchasedCurrency: string,
  fromDateTime: string
): Promise<PriceEntry[]> {

  const url = `${BASE_URL}/prices/?PaymentCurrency=${encodeURIComponent(
    paymentCurrency
  )}&PurchasedCurrency=${encodeURIComponent(purchasedCurrency)}&FromDateTime=${encodeURIComponent(fromDateTime)}`;

  const res = await fetch(url);
  if (!res.ok) throw new Error('Failed to fetch prices');
  
  const data = await res.json();
  return data;
}