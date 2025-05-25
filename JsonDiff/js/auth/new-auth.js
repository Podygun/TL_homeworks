import { hideElement, showElement } from "../utils/dom-helpers.js";
import { AUTH_KEY } from '../utils/configuration.js';
import { validateLogin } from "../form/form-validator.js";

export const isUserAuthenticated = () => {
    const user = localStorage.getItem(AUTH_KEY)

    if(Boolean(user)){
        return true
    }
    return false;
}

export const clearUser = () => {

}

export const getUserAuthenticated = () => {

    initAuthForm();
    //hide all
    //show auth form
    
    const signInForm = document.getElementById('signInForm');
    const loginInput = document.getElementById('login');
    const loginError = document.getElementById('loginError');

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

        localStorage.setItem(AUTH_KEY, JSON.stringify(currentUser));
        const isAuthenticated = true;

        return ({name:login, isAuthenticated:isAuthenticated});

    });
}

const initAuthForm = () => {
    //hideElement(promo);
    showElement(authForm);
    //hideElement(appContent);

    loginInput.value = '';
    loginError.textContent = '';
}


// вместо showAuthPage делаю const user = getUserAuthenticated