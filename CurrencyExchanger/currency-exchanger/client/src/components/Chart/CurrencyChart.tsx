import React, { useMemo, useState } from 'react';
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
import styles from './CurrencyChart.module.css';
import { useCurrencyStore } from '../stores/useCurrencyStore';

const timeIntervals = ['1 MIN', '2 MIN', '3 MIN', '4 MIN', '5 MIN'] as const;
type TimeInterval = (typeof timeIntervals)[number];

ChartJS.register(LineElement, PointElement, LinearScale, CategoryScale, Title, Tooltip, Legend);

const CurrencyChart: React.FC = () => {
  const { ratesData } = useCurrencyStore();
  const [timeInterval, setTimeInterval] = useState<TimeInterval>('5 MIN');

  const getMilliseconds = (interval: TimeInterval) => {
    const minutes = parseInt(interval.split(' ')[0]);
    return minutes * 60 * 1000;
  };

  const filteredData = useMemo(() => {
    if (!ratesData || ratesData.length === 0) return [];
    
    const ms = getMilliseconds(timeInterval);
    const cutoff = Date.now() - ms;
    
    return ratesData.filter(item => 
      new Date(item.dateTime).getTime() > cutoff
    );
  }, [ratesData, timeInterval]);

  const chartData = useMemo(() => ({
    labels: filteredData.map((item) => new Date(item.dateTime)),
    datasets: [
      {
        data: filteredData.map((item) => item.price),
        borderColor: '#3467d5',
        fill: true,
        backgroundColor: 'rgba(52, 103, 213, 0.5)',
        pointRadius: 3,
        tension: 0.1
      }
    ]
  }), [filteredData]);

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