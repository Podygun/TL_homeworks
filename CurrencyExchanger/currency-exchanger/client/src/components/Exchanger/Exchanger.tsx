import { useEffect, useState, useMemo } from 'react';
import { fetchCurrencies } from '../../api/currencyApi';
import { formatNumber, formatUpdateTime } from '../utils/formatters';
import styles from './Exchanger.module.css';
import CurrencyDescription from '../CurrencyDescription/CurrencyDescription';
import CurrencyDivider from '../СurrencyDivider/CurrencyDivider';
import Loader from '../Loader/Loader';
import ErrorMessage from '../Error/ErrorMessage';
import CurrencyChart from '../Chart/CurrencyChart';
import { CurrencyInfo } from '../Types';
import { useCurrencyStore } from '../stores/useCurrencyStore';
import { FilterStorage } from '../../services/filtersStorage';

export default function Exchanger() {
  const { loading, error, getLatestRate, fetchExchangeData } = useCurrencyStore();

  const [currencies, setCurrencies] = useState<CurrencyInfo[]>([]);
  const [baseCurrency, setBaseCurrency] = useState('PLN');
  const [targetCurrency, setTargetCurrency] = useState('CAD');
  const [baseAmount, setBaseAmount] = useState('1');
  const [showDescriptions, setShowDescriptions] = useState(false);
 
  const [savedFilters, setSavedFilters] = useState<string[]>(() => {
    return FilterStorage.getFilters();
  });

  const latestRate = getLatestRate();

  const saveFilters = (filters: string[]) => {
    setSavedFilters(filters);
    FilterStorage.saveFilters(filters);
  };

  const baseCurrencyInfo = currencies.find((c) => c.code === baseCurrency);
  const targetCurrencyInfo = currencies.find((c) => c.code === targetCurrency);

  useEffect(() => {
    const loadCurrencies = async () => {
      try {
        const data = await fetchCurrencies();
        setCurrencies(data);
      } catch (error) {
        console.error('Failed to load currencies', error);
      }
    };

    loadCurrencies();
  }, []);

  useEffect(() => {
    fetchExchangeData(baseCurrency, targetCurrency);
    const intervalId = setInterval(() => {
      fetchExchangeData(baseCurrency, targetCurrency);
    }, 10000);
    return () => clearInterval(intervalId);
  }, [baseCurrency, targetCurrency, fetchExchangeData]);

  const convertedAmount = useMemo(() => {
    const amountNum = parseFloat(baseAmount) || 0;
    if (targetCurrency === baseCurrency) return amountNum;
    return latestRate ? amountNum * latestRate.price : 0;
  }, [baseAmount, targetCurrency, baseCurrency, latestRate]);

  const handleBaseAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const val = e.target.value;
    if (/^\d*\.?\d*$/.test(val)) {
      setBaseAmount(val);
    }
  };

  const handleSaveFilter = () => {
    if (!baseCurrency || !targetCurrency || baseCurrency === targetCurrency) return;

    const filter = `${baseCurrency}/${targetCurrency}`;
    if (!savedFilters.includes(filter)) {
      saveFilters([...savedFilters, filter]);
    }
  };

  const handleClearFilters = () => {
    FilterStorage.clearFilters();
    setSavedFilters([]);
  };

  const handleFilterClick = (filter: string) => {
    const [from, to] = filter.split('/');
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

  if (currencies.length === 0 || loading) {
    return <Loader />;
  }

  if (error) {
    return <ErrorMessage message={error} />;
  }

  return (
    <>
      {savedFilters.length > 0 && (
        <div className={styles.filtersContainer}>
          <div className={styles.filtersList}>
            {savedFilters.map((filter) => (
              <button key={filter} className={styles.filterBtn} onClick={() => handleFilterClick(filter)} type="button">
                {filter}
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
              {latestRate ? latestRate.price : 0} {targetCurrencyInfo?.name}
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
            <div className={styles.time}>
              {latestRate?.dateTime ? formatUpdateTime(new Date(latestRate.dateTime)) : ''}
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
            <CurrencyChart />
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
