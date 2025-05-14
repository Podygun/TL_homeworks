// Состояние приложения
const AUTH_KEY = 'jsonDiffAuthKey';

export function auth() {
  const logo = document.getElementById('logo');
  const authSection = document.getElementById('authSection');
  const promo = document.getElementById('promo');
  const startBlock = document.getElementById('startBlock');
  const startLink = document.getElementById('startLink');
  const authForm = document.getElementById('authForm');
  const appContent = document.getElementById('appContent');
  const signInForm = document.getElementById('signInForm');
  const loginInput = document.getElementById('login');
  const loginError = document.getElementById('loginError');
  const oldJsonTextarea = document.getElementById('oldJson');
  const newJsonTextarea = document.getElementById('newJson');
  const resultBlock = document.querySelector('.result');

  let currentUser = JSON.parse(localStorage.getItem(AUTH_KEY));

  // Клик по логотику
  logo.addEventListener('click', () => {
    showPromo();
  });

  // Клик по Start
  startLink.addEventListener('click', (e) => {
    e.preventDefault();
    if (currentUser) {
      showAppContent();
    } else {
      showAuthForm();
    }
  });

  // Клик по Log in/out
  authSection.addEventListener('click', (e) => {
    if (e.target.classList.contains('logout-text')) {
      logout();
    } else if (!currentUser) {
      showAuthForm();
    }
  });

  // Отправка формы авторизации
  signInForm.addEventListener('submit', (e) => {
    e.preventDefault();
    loginError.textContent = '';
    
    const login = loginInput.value.trim();
    if (!login) {
      loginError.textContent = 'Please enter your login';
      return;
    }

    currentUser = { name: login };
    localStorage.setItem(AUTH_KEY, JSON.stringify(currentUser));
    updateUI();
    resetForm();
    showAppContent();
  });

  function logout() {
    localStorage.removeItem(AUTH_KEY);
    currentUser = null;
    updateUI();
    showPromo();
  }

  function updateUI() {
    if (currentUser) {
      authSection.innerHTML = `
        <span class="user-greeting">Hello, ${currentUser.name}!</span>
        <span class="logout-text">Log out</span>
      `;
      startBlock.style.display = 'block';
    } else {
      authSection.innerHTML = '<span class="auth-text">Log in</span>';
      startBlock.style.display = 'none';
    }
  }

  function showPromo() {
    promo.style.display = 'block';
    authForm.style.display = 'none';
    appContent.style.display = 'none';
  }

  function showAuthForm() {
    promo.style.display = 'none';
    authForm.style.display = 'block';
    appContent.style.display = 'none';
    loginInput.value = '';
    loginError.textContent = '';
  }

  function showAppContent() {
    promo.style.display = 'none';
    authForm.style.display = 'none';
    appContent.style.display = 'block';
  }

  function resetForm() {
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

  // Инициализация
  updateUI();
  showPromo();
}