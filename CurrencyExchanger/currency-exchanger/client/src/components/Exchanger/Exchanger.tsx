import { useEffect, useState } from 'react';
import { fetchCurrencies, fetchLatestRate, CurrencyInfo } from '../api/currencyApi';
import { formatNumber, formatUpdateTime } from '../utils/formatters';
import styles from './Exchanger.module.css';
import CurrencyDescription from '../CurrencyDescription/CurrencyDescription';
import CurrencyDivider from '../СurrencyDivider/CurrencyDivider';
import Loader from '../Loader/Loader';
import ErrorMessage from '../Error/ErrorMessage';
import CurrencyChart from '../Chart/CurrencyChart';

export default function ExchangeWidget() {
  const [currencies, setCurrencies] = useState<CurrencyInfo[]>([]);
  const [baseCurrency, setBaseCurrency] = useState('PLN');
  const [targetCurrency, setTargetCurrency] = useState('CAD');
  const [rate, setRate] = useState<number | null>(null);
  const [baseAmount, setBaseAmount] = useState<string>('1');
  const [convertedAmount, setConvertedAmount] = useState<number>(0);
  const [lastUpdateTime, setLastUpdateTime] = useState<Date | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const [savedPairs, setSavedPairs] = useState<string[]>(() => {
    const saved = localStorage.getItem('currencyPairs');
    return saved ? JSON.parse(saved) : [];
  });

  const baseCurrencyInfo = currencies.find((c) => c.code === baseCurrency);
  const targetCurrencyInfo = currencies.find((c) => c.code === targetCurrency);

  const fromDateTimeApi = '2000-01-01T00:00:00';

  const handleError = (message: string) => {
    setErrorMessage(message);
    setIsLoading(false);
  };

  useEffect(() => {
    setIsLoading(true);
    fetchCurrencies()
      .then((data) => {
        setCurrencies(data);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, []);

  useEffect(() => {
    if (!baseCurrency || !targetCurrency) return;
    setIsLoading(true);
    fetchLatestRate(baseCurrency, targetCurrency, fromDateTimeApi)
      .then((data) => {
        if (data) {
          setRate(data.price);
          setLastUpdateTime(new Date(data.dateTime));
        } else {
          setRate(null);
          setLastUpdateTime(null);
        }
      })
      .catch(() => handleError('COULD NOT GET DATA FROM THE SERVER'))
      .finally(() => {
        setIsLoading(false);
      });
  }, [baseCurrency, targetCurrency]);

  useEffect(() => {
    localStorage.setItem('currencyPairs', JSON.stringify(savedPairs));
  }, [savedPairs]);

  // Расчет итоговой единицы (при изменении параметров)
  useEffect(() => {
    const amountNum = parseFloat(baseAmount);

    if (isNaN(amountNum) || amountNum < 0) {
      setConvertedAmount(0);
      return;
    }

    if (targetCurrency === baseCurrency) {
      setConvertedAmount(amountNum);
    } else {
      setConvertedAmount(rate ? amountNum * rate : 0);
    }
  }, [baseAmount, targetCurrency, baseCurrency, rate]);

  const handleBaseAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    if (/^\d*\.?\d*$/.test(val)) {
      setBaseAmount(val);
    }
  };

  const handleSaveFilter = () => {
    if (!baseCurrency || !targetCurrency) return;
    if (baseCurrency === targetCurrency) return; // не сохраняем одинаковые валюты

    const pair = `${baseCurrency}/${targetCurrency}`;

    if (savedPairs.includes(pair)) return; // уже есть

    setSavedPairs((prev) => [...prev, pair]);
  };

  const handleClearFilters = () => {
    setSavedPairs([]);
  };

  const renderCurrencyDescriptions = () => {
    if (baseCurrencyInfo && targetCurrencyInfo) {
      if (baseCurrencyInfo.code === targetCurrencyInfo.code) {
        return <CurrencyDescription currency={baseCurrencyInfo} />;
      }
    }

    return (
      <>
        {baseCurrencyInfo && <CurrencyDescription currency={baseCurrencyInfo} />}
        {targetCurrencyInfo && <CurrencyDescription currency={targetCurrencyInfo} />}
      </>
    );
  };

  if (isLoading) {
    return <Loader />;
  }

  if (errorMessage) {
    return <ErrorMessage message={errorMessage} />;
  }

  if (currencies.length !== 0) {
    return (
      <>
        {savedPairs.length > 0 && (
          <div className={styles.filtersContainer}>
            <div className={styles.filtersList}>
              {savedPairs.map((pair) => (
                <button
                  key={pair}
                  className={styles.filterBtn}
                  onClick={() => {
                    const [from, to] = pair.split('/');
                    setBaseCurrency(from);
                    setTargetCurrency(to);
                  }}
                  type="button"
                >
                  {pair}
                </button>
              ))}
            </div>
            <button className={styles.clearButton} onClick={handleClearFilters} type="button">
              Clear filters
            </button>
          </div>
        )}
        <div className={styles.container}>
          <div className={styles.newHeader}>
            <div>
              <div className={styles.fromCurrencyTitle}>
                {1} {baseCurrencyInfo?.name} is
              </div>
              <div className={styles.toCurrencyTitle}>
                {1 * (rate ?? 0)} {targetCurrencyInfo?.name}
              </div>
            </div>
            <div className={styles.saveBtnContainer}>
              <button className={styles.saveFilterBtn} onClick={handleSaveFilter} type="button">
                Save Filter
              </button>
            </div>
          </div>

          <div className={styles.header}>
            <div className={styles.leftHeader}>
              <div className={styles.time}>{lastUpdateTime ? `${formatUpdateTime(lastUpdateTime)}` : ''}</div>
              <div className={styles.converterRow}>
                <input
                  type="text"
                  inputMode="decimal"
                  value={baseAmount}
                  onChange={handleBaseAmountChange}
                  className={styles.input}
                  aria-label="Base amount"
                />
                <select
                  value={baseCurrency}
                  onChange={(e) => setBaseCurrency(e.target.value)}
                  className={styles.select}
                >
                  {currencies.map((c) => (
                    <option key={c.code} value={c.code}>
                      {c.code}
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
                <select
                  value={targetCurrency}
                  onChange={(e) => setTargetCurrency(e.target.value)}
                  className={styles.select}
                >
                  {currencies.map((c) => (
                    <option key={c.code} value={c.code}>
                      {c.code}
                    </option>
                  ))}
                </select>
              </div>
            </div>

            <div className={styles.rightHeader}>
              <CurrencyChart baseCurrency={baseCurrency} targetCurrency={targetCurrency} />
            </div>
          </div>

          {baseCurrencyInfo && targetCurrencyInfo && (
            <CurrencyDivider baseCode={baseCurrencyInfo.code} targetCode={targetCurrencyInfo.code} />
          )}

          {renderCurrencyDescriptions()}
        </div>
      </>
    );
  }
}
