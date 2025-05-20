export const formValidation = () => {
    const form = document.querySelector('.main-form');
    const textareaOld = document.querySelector('#oldJson');
    const textareaNew = document.querySelector('#newJson');
    const resultBlock = document.querySelector('.result');
    const button = document.querySelector('.main-form button');

    // Валидация принятых json строк
    const isValidJson = (str) => {
        try {
            JSON.parse(str);
            return true;
        } catch (e) {
            return false;
        }
    }

    const showError = (elementId, message) => {
        const errorElement = document.getElementById(elementId);
        errorElement.textContent = message;
        errorElement.classList.add('active');
    }

    const resetErrors = () => {
        document.querySelectorAll('.error-message').forEach(el => {
            el.textContent = '';
            el.classList.remove('active');
        });
        textareaOld.classList.remove('error');
        textareaNew.classList.remove('error');
    }

    form.addEventListener('submit', async (event) => {
        event.preventDefault();
        resetErrors();
        resultBlock.classList.remove('result__visible');
        resultBlock.innerHTML = '';
        
        const oldValue = textareaOld.value.trim();
        const newValue = textareaNew.value.trim();
        let isValid = true;

        // Валидация oldJson
        if (!oldValue) {
            showError('oldJsonError', 'Поле не может быть пустым');
            textareaOld.classList.add('error');
            isValid = false;
        } else if (!isValidJson(oldValue)) {
            showError('oldJsonError', 'Невалидный JSON');
            textareaOld.classList.add('error');
            isValid = false;
        }

        // Валидация newJson
        if (!newValue) {
            showError('newJsonError', 'Поле не может быть пустым');
            textareaNew.classList.add('error');
            isValid = false;
        } else if (!isValidJson(newValue)) {
            showError('newJsonError', 'Невалидный JSON');
            textareaNew.classList.add('error');
            isValid = false;
        }

        if (!isValid) return;

        const defaultButtonHtml = button.innerHTML;
        button.innerHTML = 'Loading...';
        button.disabled = true;
    });
}