import { hideElement, showElement, updateStateUI } from "../utils/dom-helpers.js";
import { AUTH_KEY } from '../utils/configuration.js';
import { validateLogin } from "../form/form-validator.js";

export const authentication = () => {
  
  const signInForm = document.getElementById('signInForm');
  const loginInput = document.getElementById('login');
  const loginError = document.getElementById('loginError');
  const oldJsonTextarea = document.getElementById('oldJson');
  const newJsonTextarea = document.getElementById('newJson');
  const resultBlock = document.querySelector('.result');

  let currentUser = localStorage.getItem(AUTH_KEY);

  signInForm.addEventListener('submit', (e) => {
    e.preventDefault();
    loginError.textContent = '';
    
    const login = loginInput.value.trim();

    if (!validateLogin(login)) {
      showElement(loginError);
      loginError.textContent = 'Uncorrect login';
      return;
    }

    hideElement(loginError);

    currentUser = { name: login };
    localStorage.setItem(AUTH_KEY, JSON.stringify(currentUser));
    updateStateUI();
    resetForm();
  });

  const resetForm = () => {
    oldJsonTextarea.value = `{
  "timeout": 20,
  "verbose": true,
  "host": "google.com"
}`;
    newJsonTextarea.value = `{
  "timeout": 50,
  "proxy": "888.888.88.88",
  "host": "google.com"
}`;
    resultBlock.innerHTML = '';
    resultBlock.classList.remove('result__visible');
  }

  updateStateUI();
}