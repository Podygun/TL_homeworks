import { userAvatars } from '../constants/data';
import type { FeedbackResponse } from '../types/FeedbackResponse';
import type { Review } from '../types/Review';

export async function sendFeedback(_prevState: unknown, formData: FormData): Promise<FeedbackResponse> {  
  const name = formData.get('name') as string;
  const feedback = formData.get('feedback') as string;
  const rating = Number(formData.get('rating'));

  if (!name || !rating) {
    return { error: 'Заполните все поля' };
  }

  const newReview: Review = {
    id: Date.now(),
    name,
    feedback,
    rating,
    avatar: userAvatars[Math.floor(Math.random() * userAvatars.length)],
    date: new Date().toLocaleDateString()
  };
  
  const savedReviews = JSON.parse(localStorage.getItem('feedbackReviews') || '[]');
  const updatedReviews = [newReview, ...savedReviews ];

  localStorage.setItem('feedbackReviews', JSON.stringify(updatedReviews));

  return { success: true, reviews: updatedReviews };
}
