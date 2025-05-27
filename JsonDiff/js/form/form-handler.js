import { jsonDiff } from "../services/json-diff.js";
import { validateForm } from "./form-validator.js";
import { AuthHandler } from "../auth/auth.js";
import {
  hideElement,
  showElement,
  updateStateUI,
} from "../utils/dom-helpers.js";

export const handleForms = () => {
  handleAuthForm();
  handleDiffForm();
};

const handleAuthForm = () => {
  const authForm = document.getElementById("authForm");

  authForm.addEventListener("submit", (e) => {
    e.preventDefault();

    if (validateForm(authForm)) {
      const loginInput = document.getElementById("login");
      const authBlock = document.getElementById("authBlock");
      const username = loginInput.value.trim();

      const appContentBlock = document.getElementById("appContentBlock");

      AuthHandler.setUsername(username);
      hideElement(authBlock);
      showElement(appContentBlock);
      updateStateUI();
    }
  });
};

const handleDiffForm = () => {
  const diffForm = document.getElementById("diffForm");
  const textareaOld = document.querySelector("#oldJson");
  const textareaNew = document.querySelector("#newJson");
  const resultBlock = document.getElementById("resultBlock");
  const button = document.querySelector(".main-form button");

  diffForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    if (validateForm(diffForm)) {
      const defaultButtonHtml = button.innerHTML;
      button.innerHTML = "Loading...";
      button.disabled = true;

      try {
        const oldValue = JSON.parse(textareaOld.value);
        const newValue = JSON.parse(textareaNew.value);
        const result = await jsonDiff.create(oldValue, newValue);
        const jsonResult = JSON.stringify(result, undefined, 2);

        resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
        showElement(resultBlock);
      } catch (error) {
        resultBlock.innerHTML = `<pre class="error">Error: ${error.message}</pre>`;
        showElement(resultBlock);
      } finally {
        button.innerHTML = defaultButtonHtml;
        button.disabled = false;
      }
    }
    else{
      hideElement(resultBlock);
    }
  });
};
