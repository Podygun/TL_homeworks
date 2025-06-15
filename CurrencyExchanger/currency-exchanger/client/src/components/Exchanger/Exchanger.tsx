import { useEffect, useState, useCallback } from 'react';
import { fetchCurrencies, fetchLatestRate } from '../../api/currencyApi';
import { formatNumber, formatUpdateTime } from '../utils/formatters';
import styles from './Exchanger.module.css';
import CurrencyDescription from '../CurrencyDescription/CurrencyDescription';
import CurrencyDivider from '../СurrencyDivider/CurrencyDivider';
import Loader from '../Loader/Loader';
import ErrorMessage from '../Error/ErrorMessage';
import CurrencyChart from '../Chart/CurrencyChart';
import { CurrencyInfo } from '../Types';

export default function Exchanger() {
  const [currencies, setCurrencies] = useState<CurrencyInfo[]>([]);
  const [baseCurrency, setBaseCurrency] = useState('PLN');
  const [targetCurrency, setTargetCurrency] = useState('CAD');
  const [rate, setRate] = useState<number | null>(null);
  const [baseAmount, setBaseAmount] = useState<string>('1');
  const [convertedAmount, setConvertedAmount] = useState<number>(0);
  const [lastUpdateTime, setLastUpdateTime] = useState<Date | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [chartKey, setChartKey] = useState(0);
  const [showDescriptions, setShowDescriptions] = useState(false);

  const [savedPairs, setSavedPairs] = useState<string[]>(() => {
    const saved = localStorage.getItem('currencyPairs');
    return saved ? JSON.parse(saved) : [];
  });

  const savePairs = (pairs: string[]) => {
    setSavedPairs(pairs);
    localStorage.setItem('currencyPairs', JSON.stringify(pairs));
  };

  const baseCurrencyInfo = currencies.find((c) => c.code === baseCurrency);
  const targetCurrencyInfo = currencies.find((c) => c.code === targetCurrency);

  useEffect(() => {
    const loadCurrencies = async () => {
      try {
        const data = await fetchCurrencies();
        setCurrencies(data);
      } catch (error) {
        setErrorMessage('Failed to load currencies');
      }
    };

    loadCurrencies();
  }, []);

  const loadExchangeRate = useCallback(async () => {
    if (!baseCurrency || !targetCurrency) return;

    try {
      const data = await fetchLatestRate(baseCurrency, targetCurrency, '2025-05-01T00:00:00');
      setRate(data?.price ?? null);
      setLastUpdateTime(data?.dateTime ? new Date(data.dateTime) : null);
      setChartKey((prev) => prev + 1);
    } catch (error) {
      setErrorMessage('Failed to get exchange rate');
    }
  }, [baseCurrency, targetCurrency]);

  useEffect(() => {
    loadExchangeRate();
    const intervalId = setInterval(loadExchangeRate, 10000);
    return () => clearInterval(intervalId);
  }, [loadExchangeRate]);

  useEffect(() => {
    const amountNum = parseFloat(baseAmount);
    if (isNaN(amountNum)) return;

    setConvertedAmount(targetCurrency === baseCurrency ? amountNum : rate ? amountNum * rate : 0);
  }, [baseAmount, targetCurrency, baseCurrency, rate]);

  const handleBaseAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    if (/^\d*\.?\d*$/.test(val)) {
      setBaseAmount(val);
    }
  };

  const handleSaveFilter = () => {
    if (!baseCurrency || !targetCurrency || baseCurrency === targetCurrency) return;

    const pair = `${baseCurrency}/${targetCurrency}`;
    if (!savedPairs.includes(pair)) {
      savePairs([...savedPairs, pair]);
    }
  };

  const handleClearFilters = () => {
    savePairs([]);
  };

  const handleFilterClick = (pair: string) => {
    const [from, to] = pair.split('/');
    setBaseCurrency(from);
    setTargetCurrency(to);
  };

  const renderCurrencyDescriptions = () => {
    if (!baseCurrencyInfo || !targetCurrencyInfo || !showDescriptions) return null;

    return (
      <>
        <CurrencyDescription currency={baseCurrencyInfo} />
        {baseCurrency !== targetCurrency && <CurrencyDescription currency={targetCurrencyInfo} />}
      </>
    );
  };

  if (currencies.length === 0 || rate === null) {
    return <Loader />;
  }

  if (errorMessage) {
    return <ErrorMessage message={errorMessage} />;
  }

  return (
    <>
      {savedPairs.length > 0 && (
        <div className={styles.filtersContainer}>
          <div className={styles.filtersList}>
            {savedPairs.map((pair) => (
              <button key={pair} className={styles.filterBtn} onClick={() => handleFilterClick(pair)} type="button">
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
        <div className={styles.header}>
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

        <div className={styles.exchangerContainer}>
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
              <select value={baseCurrency} onChange={(e) => setBaseCurrency(e.target.value)} className={styles.select}>
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
            <CurrencyChart key={chartKey} baseCurrency={baseCurrency} targetCurrency={targetCurrency} />
          </div>
        </div>

        {baseCurrencyInfo && targetCurrencyInfo && (
          <CurrencyDivider
            baseCode={baseCurrencyInfo.code}
            targetCode={targetCurrencyInfo.code}
            onClick={() => setShowDescriptions(!showDescriptions)}
            isExpanded={showDescriptions}
          />
        )}

        {renderCurrencyDescriptions()}
      </div>
    </>
  );
}
