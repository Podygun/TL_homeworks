import React from "react";
import styles from "./InputRating.module.css";
import { emojis } from "../constants/data";
import type { InputRatingProps } from "./InputRatingProps";


export const InputRating: React.FC<InputRatingProps> = ({ defaultRating, error, onChange }) => {

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(Number(e.target.value));
  };

  return (
    <div>
      <div className={styles.emojiContainer}>
        {emojis.map((emoji) => (
          <label
            key={emoji.id}
            className={`${styles.emojiOption} ${
              defaultRating === emoji.id ? styles.selected : ""
            }`}
          >
            <input
              type="radio"
              name="rating"
              value={emoji.id}
              className={styles.hiddenInput}
              defaultChecked={defaultRating === emoji.id}
              onChange={handleChange}
            />
            <span className={styles.emoji}>{emoji.icon}</span>
          </label>
        ))}
      </div>
      {error && <div className={styles.error}>{error}</div>}
    </div>
  );
};
