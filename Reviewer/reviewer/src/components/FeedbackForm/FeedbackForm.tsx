import { useEffect, useState, useActionState } from "react";
import styles from "./Feedback.module.css";

import { useAutoResizeTextarea } from "../hooks/useAutoResizeTextarea";
import { sendFeedback } from "../api/feedbackApi";
import { emojis } from "../constants/data";
import { ReviewCard } from "../ReviewCard/ReviewCard";

interface Review {
  id: number;
  name: string;
  feedback: string;
  rating: number;
  avatar: string;
  date: string;
}

export default function FeedbackForm() {
  const [selectedEmoji, setSelectedEmoji] = useState<number | null>(null);
  const [reviews, setReviews] = useState<Review[]>([]);
  const [message, formAction, isPending] = useActionState(sendFeedback, null);
  const [error, setError] = useState<string | null>(null);
  const { textareaRef, formRef } = useAutoResizeTextarea();
  
  // Загрузка отзывов при запуске
  useEffect(() => {
    const savedReviews = localStorage.getItem("feedbackReviews");
    if (savedReviews) {
      try {
        setReviews(JSON.parse(savedReviews));
      } catch (error) {
        console.error("Error parsing saved reviews:", error);
        localStorage.removeItem("feedbackReviews");
      }
    }
  }, []);

  // Загрузка отзывов при добавлении отзыва
  useEffect(() => {
    if (message?.success) {
      if (Array.isArray(message.reviews)) {
        setReviews(message.reviews);
      } else {
        console.error("Invalid reviews data:", message.reviews);
        const savedReviews = localStorage.getItem("feedbackReviews");
        if (savedReviews) setReviews(JSON.parse(savedReviews));
      }

      // Сброс формы
      if (formRef.current) {
        formRef.current.reset();
        if (textareaRef.current) {
          textareaRef.current.style.height = 'auto';
        }
      }

      setSelectedEmoji(null);
    }
  }, [message, formRef, textareaRef]);

  // Отправка формы
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (selectedEmoji === null) {
      setError("Выберите оценку (смайлик)");
      return;
    }
    setError(null);

    const formData = new FormData(e.currentTarget as HTMLFormElement);
    formData.set("rating", selectedEmoji.toString());
    formAction(formData);
  };

  return (
    <div className={styles.container}>
      <form
        action={formAction}
        className={styles.form}
        ref={formRef}
        onSubmit={handleSubmit}
      >
        <div className={styles.formContent}>
          <h2 className={styles.title}>
            Помогите нам сделать процесс бронирования лучше
          </h2>

          <div className={styles.emojiContainer}>
            {emojis.map((emoji) => (
              <div
                key={emoji.id}
                className={`${styles.emojiOption} ${
                  selectedEmoji === emoji.id ? styles.selected : ""
                }`}
                onClick={() => setSelectedEmoji(emoji.id)}
              >
                <span className={styles.emoji}>{emoji.icon}</span>

                <input
                  type="radio"
                  name="rating"
                  value={emoji.id}
                  checked={selectedEmoji === emoji.id}
                  onChange={() => {}}
                  className={styles.hiddenInput}
                />
              </div>
            ))}
          </div>
          {error && <div className={styles.error}>{error}</div>}


          <div className={styles.inputGroup}>
            <span className={styles.floatingLabel}>*Имя</span>
            <input
              type="text"
              name="name"
              id="name"
              placeholder="Как вас зовут?"
              className={styles.input}
              required
            />
          </div>

          <textarea
            ref={textareaRef}
            name="feedback"
            placeholder="Напишите, что понравилось, что непонятно"
            className={styles.textarea}
          />

          <div className={styles.buttonContainer}>
            <button
              type="submit"
              className={styles.submitButton}
              disabled={isPending}
            >
              {isPending ? "Отправка..." : "Отправить"}
            </button>
          </div>
        </div>
      </form>

      <div className={styles.reviewsContainer}>
        {reviews.map((review) => (
          <ReviewCard key={review.id} review={review} />
        ))}
      </div>
    </div>
  );
}
