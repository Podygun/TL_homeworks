import { hideElement, showElement } from "../utils/dom-helpers.js";

export const validateForm = (form) => {
  const inputs = form.querySelectorAll("input, textarea");
  let isValid = true;

  inputs.forEach((input) => {
    const validationRules = input.getAttribute("validation-rules");
    const validationMessage = input.nextElementSibling;

    validationMessage.textContent = "";

    if (validationRules) {
      if (validationRules.includes('required')) {

        if (!input.value.trim()) {
          validationMessage.textContent = 'Поле обязательно для заполнения.';
          showElement(validationMessage);
          isValid = false;
        } else if (!validateLogin(input.value.trim())) {
          validationMessage.textContent = 'Логин должен быть > 2 символов';
          showElement(validationMessage);
          isValid = false;
        }
        else{
          hideElement(validationMessage)
        }
        return;
      }

      if (validationRules.includes('json-format') && !isValidJson(input.value.trim())) {
        validationMessage.textContent = 'Неверный формат JSON.';

        input.classList.add("error");

        showElement(validationMessage);
        isValid = false;
        return;
      }
      else{
        hideElement(validationMessage)
        input.classList.remove("error");
      }

    }
  });

  return isValid;
};

const isValidJson = (str) => {
  try {
    JSON.parse(str);
    return true;
  } catch (e) {
    return false;
  }
};

const validateLogin = (loginString) => {
  if (!loginString) {
    return false;
  }

  if (loginString.length < 2 || loginString.length > 20) {
    return false;
  }

  return true;
};
