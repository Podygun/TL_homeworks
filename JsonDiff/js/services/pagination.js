import { hideElement, hideErrors, showElement, updateStateUI } from "../utils/dom-helpers.js";
import { AuthHandler } from "../auth/auth.js";

const domElements = {
  logo: document.getElementById("logo"),
  loginSection: document.getElementById("loginSection"),
  promo: document.getElementById("promo"),
  startLink: document.getElementById("startLink"),
  startBlock: document.getElementById("startBlock"),
  authBlock: document.getElementById("authBlock"),
  appContent: document.getElementById("appContentBlock"),
  loginInput: document.getElementById("login"),
}

export const setPagination = () => {

  domElements.logo.addEventListener("click", () => {
    showPromo();
  });

  domElements.startLink.addEventListener("click", () => {
    if (AuthHandler.isAuthenticated()) {
      showAppContent();
    } else {
      showAuthForm();
    }
  });

  loginSection.addEventListener("click", () => {
    if (AuthHandler.isAuthenticated()) {
      logout();
    } else {
      showAuthForm();
    }
  });

  const logout = () => {
    AuthHandler.clearUser();
    updateStateUI();
    showPromo();
  };

  const showPromo = () => {
    showElement(domElements.promo);
    hideElement(domElements.authBlock);
    hideElement(domElements.appContent);
  };

  const showAuthForm = () => {
    hideErrors();
    hideElement(domElements.promo);
    showElement(domElements.authBlock);
    hideElement(domElements.appContent);
  };

  const showAppContent = () => {
    hideElement(domElements.promo);
    hideElement(domElements.authBlock);
    showElement(domElements.appContent);
  };

  updateStateUI();
  showPromo();
};

