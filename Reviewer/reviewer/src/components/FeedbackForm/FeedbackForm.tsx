import { useState, useActionState } from "react";
import styles from "./Feedback.module.css";

import { useAutoResizeTextarea } from "../hooks/useAutoResizeTextarea";
import { sendFeedback } from "../api/feedbackApi";
import type { FeedbackFormProps } from "./FeedbackFormProps";
import { InputRating } from "../InputRating/InputRating";
import type { FeedbackResponse } from "../types/FeedbackResponse";

type FormErrors = {
  name?: string;
  feedback?: string;
  rating?: string;
};

export const FeedbackForm: React.FC<FeedbackFormProps> = ({ addReview }) => {
  const [rating, setRating] = useState<number | undefined>(undefined);
  const [name, setName] = useState("");
  const [feedback, setFeedback] = useState("");
  const [errors, setErrors] = useState<FormErrors>({});

  const { textareaRef, formRef } = useAutoResizeTextarea();

  const [, formAction, isPending] = useActionState(
    async (_prevState: FeedbackResponse | null, formData: FormData) => {
      const nameValue = formData.get("name")?.toString().trim() || "";
      const feedbackValue = formData.get("feedback")?.toString().trim() || "";
      const ratingValue = rating;

      const newErrors: FormErrors = {};

      if (!nameValue) {
        newErrors.name = "Пожалуйста, введите имя";
      }

      if (!feedbackValue) {
        newErrors.feedback = "Пожалуйста, оставьте отзыв";
      }

      if (ratingValue === undefined) {
        newErrors.rating = "Пожалуйста, выберите оценку";
      }

      if (Object.keys(newErrors).length > 0) {
        setErrors(newErrors);
        return { success: false };
      }

      formData.set("rating", ratingValue!.toString());

      const response = await sendFeedback(_prevState, formData);

      if (response.success && response.reviews?.length) {
        addReview(response.reviews[0]);

        formRef.current?.reset();
        setRating(undefined);
        setName("");
        setFeedback("");
        setErrors({});
        if (textareaRef.current) {
          textareaRef.current.style.height = "auto";
        }
      }

      return response;
    },
    null
  );

  return (
    <div className={styles.container}>
      <form ref={formRef} action={formAction} className={styles.form}>
        <div className={styles.formContent}>
          <h2 className={styles.title}>
            Помогите нам сделать процесс бронирования лучше
          </h2>

          <InputRating
            defaultRating={rating}
            onChange={setRating}
            error={errors?.rating}
          />

          <div className={styles.inputGroup}>
            <span className={styles.floatingLabel}>*Имя</span>
            <input
              type="text"
              name="name"
              placeholder="Как вас зовут?"
              className={styles.input}
              defaultValue={name}
              onChange={(e) => setName(e.target.value)}
            />
            {errors?.name && <div className={styles.error}>{errors.name}</div>}

            <textarea
              ref={textareaRef}
              name="feedback"
              placeholder="Напишите, что понравилось, что непонятно"
              className={styles.textarea}
              defaultValue={feedback}
              onChange={(e) => setFeedback(e.target.value)}
            />
             {errors?.feedback && <div className={styles.error}>{errors.feedback}</div>}
          </div>
          <button
            type="submit"
            className={styles.submitButton}
            disabled={isPending}
          >
            {isPending ? "Отправка..." : "Отправить"}
          </button>
        </div>
      </form>
    </div>
  );
};