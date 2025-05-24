import React from 'react';
import FeedbackForm from '../FeedbackForm/FeedbackForm';
import MixedForm from '../FeedbackForm/FeedbackForm';

import styles from './Home.module.css'; // CSS-модуль

const Home: React.FC = () => {
  return (
    <div className={styles.home}>
      <h1>Помогите нам сделать процесс бронирования лучше</h1>
      <FeedbackForm />
    </div>
  );
};

export default Home;