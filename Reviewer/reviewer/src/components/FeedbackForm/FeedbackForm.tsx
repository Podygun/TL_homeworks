import React, { useState } from 'react';
import styles from './Feedback.module.css';

const FeedbackForm = () => {
  const [ratings, setRatings] = useState({
    speed: 0,
    speechCulture: 0,
    interface: 0,
    functionality: 0,
    support: 0
  });
  const [name, setName] = useState('');
  const [feedback, setFeedback] = useState('');

  const handleRatingChange = (category, value) => {
    setRatings({
      ...ratings,
      [category]: value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Здесь можно добавить логику отправки данных
    console.log({
      ratings,
      name,
      feedback
    });
    // Очистка формы после отправки
    setRatings({
      speed: 0,
      speechCulture: 0,
      interface: 0,
      functionality: 0,
      support: 0
    });
    setName('');
    setFeedback('');
  };

  return (
    <div className="feedback-container">
      <h2>Помогите нам сделать процесс бронирования лучше</h2>
      
      <form onSubmit={handleSubmit}>
        <RatingLine 
          title="Скорость"
          value={ratings.speed}
          onChange={(value) => handleRatingChange('speed', value)}
        />
        <RatingLine 
          title="Культура речи"
          value={ratings.speechCulture}
          onChange={(value) => handleRatingChange('speechCulture', value)}
        />
        <RatingLine 
          title="Удобство интерфейса"
          value={ratings.interface}
          onChange={(value) => handleRatingChange('interface', value)}
        />
        <RatingLine 
          title="Функциональность"
          value={ratings.functionality}
          onChange={(value) => handleRatingChange('functionality', value)}
        />
        <RatingLine 
          title="Поддержка"
          value={ratings.support}
          onChange={(value) => handleRatingChange('support', value)}
        />
        
        <div className="input-group">
          <label htmlFor="name">Ваше имя (необязательно)</label>
          <input
            type="text"
            id="name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Как к вам обращаться?"
          />
        </div>
        
        <div className="input-group">
          <label htmlFor="feedback">Ваш отзыв</label>
          <textarea
            id="feedback"
            value={feedback}
            onChange={(e) => setFeedback(e.target.value)}
            placeholder="Что нам улучшить в процессе бронирования?"
            required
          />
        </div>
        
        <button type="submit" className="submit-btn">Отправить</button>
      </form>
    </div>
  );
};

const RatingLine = ({ title, value, onChange }) => {
  return (
    <div className="rating-line">
      <div className="rating-dots">
        {[1, 2, 3, 4, 5].map((num) => (
          <button
            key={num}
            type="button"
            className={`dot ${value >= num ? 'active' : ''}`}
            onClick={() => onChange(num)}
            aria-label={`Оценка ${num} из 5`}
          />
        ))}
      </div>
      <span className="rating-title">{title}</span>
    </div>
  );
};

export default FeedbackForm;