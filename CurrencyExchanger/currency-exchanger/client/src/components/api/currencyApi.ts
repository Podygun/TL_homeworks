export type CurrencyInfo = {
  code: string;
  name: string;
  description: string;
  symbol: string;
};

export type PriceEntry = {
  purchasedCurrencyCode: string;
  paymentCurrencyCode: string;
  price: number;
  dateTime: string;
};

const BASE_URL = 'https://localhost:7145';

export async function fetchCurrencies(): Promise<CurrencyInfo[]> {
  const res = await fetch(`${BASE_URL}/Currency`);
  if (!res.ok) throw new Error('Failed to fetch currencies');
  return res.json();
}

export async function fetchCurrency(code: string): Promise<CurrencyInfo> {
  const res = await fetch(`${BASE_URL}/Currency/${encodeURIComponent(code)}`);
  if (!res.ok) throw new Error(`Failed to fetch currency ${code}`);
  return res.json();
}

/**
 * Получить последний курс из массива
 */
export async function fetchLatestRate(
  paymentCurrency: string,
  purchasedCurrency: string,
  fromDateTime: string
): Promise<{ price: number; dateTime: string } | null> {
  const url = `${BASE_URL}/prices/?PaymentCurrency=${encodeURIComponent(
    paymentCurrency
  )}&PurchasedCurrency=${encodeURIComponent(purchasedCurrency)}&FromDateTime=${encodeURIComponent(fromDateTime)}`;
  console.log(url);

  const res = await fetch(url);
  if (!res.ok) throw new Error('Failed to fetch rates');
  const data: PriceEntry[] = await res.json();
  if (data.length === 0) return null;
  return data[data.length - 1];
}
