import { useEffect, useState } from 'react';
import { fetchCurrencies, fetchLatestRate, CurrencyInfo } from '../api/currencyApi';
import { formatNumber, formatUpdateTime } from '../utils/formatters';
import styles from './Exchanger.module.css';
import CurrencyDescription from '../CurrencyDescription/CurrencyDescription';

export default function ExchangeWidget() {
  const [currencies, setCurrencies] = useState<CurrencyInfo[]>([]);
  const [baseCurrency, setBaseCurrency] = useState('PLN');
  const [targetCurrency, setTargetCurrency] = useState('CAD');
  const [rate, setRate] = useState<number | null>(null);
  const [baseAmount, setBaseAmount] = useState<string>('1');
  const [convertedAmount, setConvertedAmount] = useState<number>(0);
  const [lastUpdateTime, setLastUpdateTime] = useState<Date | null>(null);

  const baseCurrencyInfo = currencies.find((c) => c.code === baseCurrency);
  const targetCurrencyInfo = currencies.find((c) => c.code === targetCurrency);

  useEffect(() => {
    fetchCurrencies().then(setCurrencies).catch(console.error);
  }, []);

  useEffect(() => {
    const fromDateTime = '2000-01-01T00:00:00';
    fetchLatestRate(baseCurrency, targetCurrency, fromDateTime)
      .then((data) => {
        if (data) {
          setRate(data.price);
          setLastUpdateTime(new Date(data.dateTime));
        } else {
          setRate(null);
          setLastUpdateTime(null);
        }
      })
      .catch(console.error);
  }, [baseCurrency, targetCurrency]);

  // Расчет итоговой единицы (при изменении параметров)
  useEffect(() => {
    const amountNum = parseFloat(baseAmount);

    if (isNaN(amountNum) || amountNum < 0) {
      setConvertedAmount(0);
      return;
    }

    if (targetCurrency === baseCurrency) {
      setConvertedAmount(amountNum);
      return;
    }

    if (rate) {
      setConvertedAmount(amountNum * rate);
    } else {
      setConvertedAmount(0);
    }
  }, [baseAmount, targetCurrency, baseCurrency, rate]);

  const handleBaseAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    // Разрешаем только числа и точку
    if (/^\d*\.?\d*$/.test(val)) {
      setBaseAmount(val);
    }
  };

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <div>
          {baseAmount || 0} {baseCurrency} is
        </div>
        <div>
          {formatNumber(convertedAmount)} {targetCurrency}
        </div>
        <div className={styles.time}>{lastUpdateTime ? `Last update: ${formatUpdateTime(lastUpdateTime)}` : ''}</div>
      </div>
      <div className={styles.converterRow}>
        <input
          type="text"
          inputMode="decimal"
          value={baseAmount}
          onChange={handleBaseAmountChange}
          className={styles.input}
          aria-label="Base amount"
        />
        <select value={baseCurrency} onChange={(e) => setBaseCurrency(e.target.value)}>
          {currencies.map((c) => (
            <option key={c.code} value={c.code}>
              {c.code} - {c.name}
            </option>
          ))}
        </select>
      </div>
      <div className={styles.converterRow}>
        <input
          type="text"
          value={formatNumber(convertedAmount)}
          readOnly
          className={styles.input}
          aria-label="Converted amount"
        />
        <select value={targetCurrency} onChange={(e) => setTargetCurrency(e.target.value)}>
          {currencies.map((c) => (
            <option key={c.code} value={c.code}>
              {c.code} - {c.name}
            </option>
          ))}
        </select>
      </div>
      <div>
        {rate !== null ? (
          <>
            <p>Rate: {rate}</p>
          </>
        ) : (
          //TODO Loading icon
          <p>Loading rate...</p>
        )}
      </div>
      {baseCurrencyInfo === targetCurrencyInfo ? (
        baseCurrencyInfo && <CurrencyDescription currency={baseCurrencyInfo} />
      ) : (
        <>
          {baseCurrencyInfo && <CurrencyDescription currency={baseCurrencyInfo} />}
          {targetCurrencyInfo && <CurrencyDescription currency={targetCurrencyInfo} />}
        </>
      )}
    </div>
  );
}
