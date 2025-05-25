import { hideElement, showElement, updateStateUI } from "../utils/dom-helpers.js";
import { AUTH_KEY } from '../utils/configuration.js';
import { isUserAuthenticated } from "../auth/new-auth.js";

export const pagination = () => {
  const logo = document.getElementById('logo');
  const authSection = document.getElementById('authSection');
  const promo = document.getElementById('promo');
  const startLink = document.getElementById('startLink');
  const authForm = document.getElementById('authForm');
  const appContent = document.getElementById('appContent');
  const loginInput = document.getElementById('login');
  const loginError = document.getElementById('loginError');

  let currentUser = localStorage.getItem(AUTH_KEY);

  hideElement(loginError);

  logo.addEventListener('click', () => {
    showPromo();
  });

  startLink.addEventListener('click', () => {
    
    if (isUserAuthenticated()) {
      showAppContent();
    } else {
      showAuthForm();
    }

  });

  authSection.addEventListener('click', () => {   
    
    if (isUserAuthenticated()) {
      logout();
    } else {
      showAuthForm();
    }

  });

  const logout = () => {
    localStorage.removeItem(AUTH_KEY);
    currentUser = null;
    updateStateUI();
    showPromo();
  }

  const showPromo = () => {
    showElement(promo);
    hideElement(authForm);
    hideElement(appContent);
  }

  const showAuthForm = () => {
    hideElement(promo);
    showElement(authForm);
    hideElement(appContent);

    loginInput.value = '';
    loginError.textContent = '';
  }

  const showAppContent = () => {
    hideElement(promo);
    hideElement(authForm);
    showElement(appContent);
  }

    updateStateUI();
    showPromo();
}