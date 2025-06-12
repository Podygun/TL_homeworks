import { AuthHandler } from "../auth/auth.js";

export const showElement = (element) => element?.classList.remove("hide");
export const hideElement = (element) => element?.classList.add("hide");

const domElements = {
  loginSection: document.getElementById("loginSection"),
  startBlock: document.getElementById("startBlock"),
  resultBlock: document.getElementById("resultBlock")
}

export const updateStateUI = () => {
  if (AuthHandler.isAuthenticated()) {

    hideElement(domElements.resultBlock);

    domElements.loginSection.innerHTML = `
        <span class="user-greeting">Hello, ${AuthHandler.getUsername()}!</span>
        <span class="logout-text">Log out</span>
    `;
    showElement(domElements.startBlock);
  } else {
    domElements.loginSection.innerHTML = '<span class="auth-text">Log in</span>';
    hideElement(domElements.startBlock);
  }
};

export const hideErrors = () => {
  const errorSpans = document.querySelectorAll('.validation-message');
  errorSpans.forEach(span => {
    span.classList.add('hide');
  });
}