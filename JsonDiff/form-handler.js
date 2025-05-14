import { JsonDiff } from "./json-diff.js";

const resultVisibleClass = 'result__visible';

export function JsonDiffForm() {
  const form = document.querySelector('.main-form');
  const textareaOld = document.querySelector('#oldJson');
  const textareaNew = document.querySelector('#newJson');
  const resultBlock = document.querySelector('.result');
  const button = document.querySelector('.main-form button');

  form.addEventListener('submit', async (event) => {
    console.log('submit');
    event.preventDefault();

    const defaultButtonHtml = button.innerHTML;
    button.innerHTML = 'Loading...';
    button.disabled = true;

    try {
      const oldValue = JSON.parse(textareaOld.value);
      const newValue = JSON.parse(textareaNew.value);
      const result = await JsonDiff.create(oldValue, newValue);

      const jsonResult = JSON.stringify(result, undefined, 2);
      resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
      
      if (!resultBlock.classList.contains(resultVisibleClass)) {
        resultBlock.classList.add(resultVisibleClass);
      }
    } catch (error) {
      resultBlock.innerHTML = `<pre class="error">Error: ${error.message}</pre>`;
      if (!resultBlock.classList.contains(resultVisibleClass)) {
        resultBlock.classList.add(resultVisibleClass);
      }
    } finally {
      button.innerHTML = defaultButtonHtml;
      button.disabled = false;
    }
  });
}