import styles from './CurrencyDivider.module.css';

type CurrencyDividerProps = {
  baseCode: string;
  targetCode: string;
};

export default function CurrencyDivider({ baseCode, targetCode }: CurrencyDividerProps) {
  return (
    <div className={styles.wrapper}>
      <hr className={styles.line} />
      <div className={styles.label}>
        {baseCode}/{targetCode}: about ↑
      </div>
    </div>
  );
}