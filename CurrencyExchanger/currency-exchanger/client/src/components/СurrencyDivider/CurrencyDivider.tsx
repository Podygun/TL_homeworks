import styles from './CurrencyDivider.module.css';

type CurrencyDividerProps = {
  baseCode: string;
  targetCode: string;
  onClick: () => void;
  isExpanded: boolean;
};

export default function CurrencyDivider({ 
  baseCode, 
  targetCode, 
  onClick,
  isExpanded 
}: CurrencyDividerProps) {
  return (
    <div className={styles.wrapper}>
      <hr className={styles.line} />
      <button 
        className={styles.label} 
        onClick={onClick}
        aria-expanded={isExpanded}
      >
        {baseCode}/{targetCode}: about {isExpanded ? '↑' : '↓'}
      </button>
    </div>
  );
}