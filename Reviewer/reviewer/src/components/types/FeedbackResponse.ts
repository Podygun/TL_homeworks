import type { Review } from "./Review";

export interface FeedbackResponse {
  success?: boolean;
  error?: string;
  reviews?: Review[];
}