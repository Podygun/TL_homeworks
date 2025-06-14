import { STORAGE_KEY } from '../constants/keys';
import type { Review } from '../types/Review';


const getReviews = (): Review[] => {
  const saved = localStorage.getItem(STORAGE_KEY);
  if (!saved) return [];
  try {
    return JSON.parse(saved) as Review[];
  } catch {
    localStorage.removeItem(STORAGE_KEY);
    return [];
  }
};

const setReviews = (reviews: Review[]) => {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(reviews));
};

export const ReviewsStore = { getReviews, setReviews };
