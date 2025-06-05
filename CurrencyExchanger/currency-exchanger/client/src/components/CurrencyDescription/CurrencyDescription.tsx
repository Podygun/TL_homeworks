import { CurrencyInfo } from '../api/currencyApi';
import styles from './CurrencyDescription.module.css'

type CurrencyDescriptionProps = {
  currency: CurrencyInfo;
};

export default function CurrencyDescription({ currency }: CurrencyDescriptionProps) {
  return (
    <div className={styles.CurrencyDescription}>
      <h4>
        {currency.name} - {currency.code} - {currency.symbol}
      </h4>
      <p>{currency.description}</p>
    </div>
  );
}
