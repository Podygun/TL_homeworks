import type { Review } from "../types/Review";

export interface FeedbackFormProps {
  addReview: (review: Review) => void;
}