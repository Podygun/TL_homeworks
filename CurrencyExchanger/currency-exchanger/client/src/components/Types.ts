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