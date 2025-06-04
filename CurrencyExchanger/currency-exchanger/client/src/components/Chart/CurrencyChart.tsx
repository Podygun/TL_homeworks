import React, { useEffect, useState } from 'react';
import { Line } from 'react-chartjs-2'; // Импортируем компонент Line
import { Chart as ChartJS, LineElement, PointElement, LinearScale, CategoryScale, Title, Tooltip, Legend } from 'chart.js';
import axios from 'axios';
import { fetchRates } from '../api/currencyApi';


interface CurrencyChartProps {
  baseCurrency: string;
  targetCurrency: string;
}

interface CurrencyPrice {
  dateTime: string; // или Date, если вы парсите сразу
  price: number;
}

type TimeInterval = '1min' | '2min' | '3min' | '4min' | '5min';


// Регистрируем компоненты Chart.js
ChartJS.register(LineElement, PointElement, LinearScale, CategoryScale, Title, Tooltip, Legend);

const CurrencyChart: React.FC<CurrencyChartProps> = ({ baseCurrency, targetCurrency }) => {
  const [data, setData] = useState<CurrencyPrice[]>([]);
  const [timeInterval, setTimeInterval] = useState<TimeInterval>('5min');

  const getMilliseconds = (interval: TimeInterval) => {
    switch (interval) {
      case '5min':
        return 5 * 60 * 1000;
      case '4min':
        return 4 * 60 * 1000;
      case '3min':
        return 3 * 60 * 1000;
      case '2min':
        return 2 * 60 * 1000;
      case '1min':
        return 1 * 60 * 1000;
      default:
        return 5 * 60 * 1000;
    }
  };

  const fetchData = async () => {
  try {
    const fromDateTime = new Date(Date.now() - getMilliseconds(timeInterval)).toISOString();
    const prices = await fetchRates(baseCurrency, targetCurrency, fromDateTime);
    setData(prices);
  } catch (error) {
    console.error('Ошибка при загрузке данных:', error);
    setData([]);
  }
};

  useEffect(() => {
    if (baseCurrency && targetCurrency) {
      fetchData();
    }
  }, [baseCurrency, targetCurrency, timeInterval]);

  const chartData = {
    labels: data.map(item => new Date(item.dateTime).toLocaleTimeString()),
    datasets: [
      {
        label: `${baseCurrency} to ${targetCurrency}`,
        data: data.map(item => item.price),
        fill: false,
        borderColor: 'rgba(75,192,192,1)',
        tension: 0.1,
      },
    ],
  };

  return (
    <div>
      <div>
        {(['5min', '4min', '3min', '2min', '1min'] as TimeInterval[]).map(interval => (
          <button
            key={interval}
            onClick={() => setTimeInterval(interval)}
            disabled={timeInterval === interval}
          >
            {interval}
          </button>
        ))}
      </div>
      <Line data={chartData} />
    </div>
  );
};

export default CurrencyChart;
