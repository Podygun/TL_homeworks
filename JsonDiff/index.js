import { init } from "./auth.js";
import { jsonDiffForm } from "./form-handler.js";
import { formValidation } from './form-validator.js';


document.addEventListener('DOMContentLoaded', () => {
  init();  
  jsonDiffForm();
  formValidation();
});
