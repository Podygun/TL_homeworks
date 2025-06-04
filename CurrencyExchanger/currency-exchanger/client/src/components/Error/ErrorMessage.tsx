import styles from './ErrorMessage.module.css';

type ErrorMessageProps = {
  message: string;
};

export default function ErrorMessage({ message }: ErrorMessageProps) {
  return (
    <div className={styles.overlay}>
      <div className={styles.messageBox}>
        {message}
      </div>
    </div>
  );
}
