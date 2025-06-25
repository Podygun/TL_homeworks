import { useState } from "react";
import "./App.module.css";
import { FeedbackForm } from "../FeedbackForm/FeedbackForm";
import type { Review } from "../types/Review";
import { ReviewsStore } from "../store/ReviewsStore";
import styles from "./App.module.css";
import { ReviewCard } from "../ReviewCard/ReviewCard";

export const App: React.FC = () => {
  const [reviews, setReviews] = useState<Review[]>(() =>
    ReviewsStore.getReviews()
  );

  const addReview = (review: Review) => {
    const newReviews = [review, ...reviews];
    setReviews(newReviews);
    ReviewsStore.setReviews(newReviews);
  };

  return (
    <div className={styles.container}>
      <FeedbackForm addReview={addReview} />
      <div className={styles.reviewsContainer}>
        {reviews.map((review) => (
          <ReviewCard key={review.id} review={review} />
        ))}
      </div>
    </div>
  );
};
