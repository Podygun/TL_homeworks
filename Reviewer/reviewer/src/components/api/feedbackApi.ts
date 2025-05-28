import { userAvatars } from '../constants/data';

export async function sendFeedback(prevState: any, formData: FormData) {  
  const name = formData.get('name') as string;
  const feedback = formData.get('feedback') as string;
  const rating = Number(formData.get('rating'));

  if (!name || !rating) {
    return { error: 'Заполните все поля' };
  }

  const newReview = {
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
