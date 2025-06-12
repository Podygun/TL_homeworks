import { hideElement, showElement } from "../utils/dom-helpers.js";

export const validateForm = (form) => {
  const inputs = form.querySelectorAll("input, textarea");
  let isValid = true;

  inputs.forEach((input) => {
    const validationRules = input.getAttribute("validation-rules");
    const validationMessage = input.nextElementSibling;

    validationMessage.textContent = "";

    if (!validationRules) {
      return;
    }

    if (validationRules.includes("required")) {   
      if (!input.value.trim()) {
        validationMessage.textContent = "Поле обязательно для заполнения.";
        showElement(validationMessage);
        isValid = false;
        return isValid;
      }

      hideElement(validationMessage);
      return;
    }

    if (
      validationRules.includes("json-format") &&
      !isValidJson(input.value.trim())
    ) {

      validationMessage.textContent = "Неверный формат JSON.";
      input.classList.add("error");
      showElement(validationMessage);
      isValid = false;
      return false;
    }

      hideElement(validationMessage);
      input.classList.remove("error");   
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
