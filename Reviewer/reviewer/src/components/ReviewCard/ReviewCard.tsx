import styles from './ReviewCard.module.css';

export function ReviewCard({ review }: { review: Review }) {
  return (
    <div className={styles.reviewCard}>
      <img 
        src={review.avatar} 
        alt={review.name} 
        className={styles.avatar}
      />
      <div className={styles.reviewContent}>
        <h3 className={styles.reviewName}>{review.name}</h3>
        <p className={styles.reviewText}>{review.feedback}</p>
      </div>
      <div className={styles.ratingBadge}>
        {review.rating}/5
      </div>
    </div>
  );
}