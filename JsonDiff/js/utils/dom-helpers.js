import { AUTH_KEY } from './configuration.js';

export const showElement = (element) => element?.classList.remove('hide');
export const hideElement = (element) => element?.classList.add('hide');
export const toggleElement = (element, isVisible) => {
  isVisible ? showElement(element) : hideElement(element);
};

export const updateStateUI = () => {

  const authSection = document.getElementById('authSection');
  const appContent = document.getElementById('appContent');
  const startBlock = document.getElementById('startBlock');

  let currentUser = localStorage.getItem(AUTH_KEY);
  let currentUserName = JSON.parse(localStorage.getItem(AUTH_KEY));

  if (currentUser) {
      authSection.innerHTML = `
        <span class="user-greeting">Hello, ${currentUserName.name}!</span>
        <span class="logout-text">Log out</span>
      `;
      showElement(startBlock);
      showElement(appContent);
      hideElement(authForm)
    } else {
      authSection.innerHTML = '<span class="auth-text">Log in</span>';
      hideElement(startBlock);
      hideElement(appContent);
    }
}