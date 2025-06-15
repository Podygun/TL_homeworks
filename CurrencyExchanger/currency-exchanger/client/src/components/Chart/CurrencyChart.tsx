import React, { useCallback, useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  LineElement,
  PointElement,
  LinearScale,
  CategoryScale,
  Title,
  Tooltip,
  Legend
} from 'chart.js';
import { fetchRates } from '../../api/currencyApi';
import styles from './CurrencyChart.module.css';

interface CurrencyChartProps {
  baseCurrency: string;
  targetCurrency: string;
}

interface CurrencyPrice {
  dateTime: string;
  price: number;
}

const timeIntervals = ['1 MIN', '2 MIN', '3 MIN', '4 MIN', '5 MIN'] as const;
type TimeInterval = (typeof timeIntervals)[number];

ChartJS.register(LineElement, PointElement, LinearScale, CategoryScale, Title, Tooltip, Legend);

const CurrencyChart: React.FC<CurrencyChartProps> = ({ baseCurrency, targetCurrency }) => {
  const [data, setData] = useState<CurrencyPrice[]>([]);
  const [timeInterval, setTimeInterval] = useState<TimeInterval>('5 MIN');

  const getMilliseconds = useCallback((interval: TimeInterval) => {
    const minutes = parseInt(interval.split(' ')[0]);
    return minutes * 60 * 1000;
  }, []);

  const fetchData = useCallback(async () => {
    if (!baseCurrency || !targetCurrency) return;

    try {
      const fromDateTime = new Date(Date.now() - getMilliseconds(timeInterval)).toISOString();
      const prices = await fetchRates(baseCurrency, targetCurrency, fromDateTime);
      setData(prices);
    } catch (error) {
      setData([]);
    }
  }, [baseCurrency, targetCurrency, timeInterval, getMilliseconds]);

  useEffect(() => {
    if (baseCurrency && targetCurrency) {
      fetchData();
    }
  }, [fetchData, baseCurrency, targetCurrency]);

  const chartData = {
    labels: data.map((item) => new Date(item.dateTime)),
    datasets: [
      {
        data: data.map((item) => item.price),
        borderColor: '#3467d5',
        fill: true,
        backgroundColor: 'rgba(52, 103, 213, 0.5)',
        pointRadius: 3,
        tension: 0.1
      }
    ]
  };

  const options = {
    responsive: true,
    scales: {
      x: {
        ticks: {
          display: false
        }
      }
    },
    plugins: {
      legend: {
        display: false
      },
      title: {
        display: false
      }
    }
  };

  return (
    <div>
      <div>
        {[...timeIntervals].reverse().map((interval) => (
          <button
            className={`${styles.timeButton} ${timeInterval === interval ? styles.active : ''}`}
            key={interval}
            onClick={() => setTimeInterval(interval)}
            disabled={timeInterval === interval}
          >
            {interval}
          </button>
        ))}
      </div>
      <div>
        <Line data={chartData} options={options} />
      </div>
    </div>
  );
};

export default CurrencyChart;
