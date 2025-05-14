import { auth } from "./auth.js";
import { JsonDiffForm } from "./form-handler.js";
import { formValidation } from './form-validator.js';


document.addEventListener('DOMContentLoaded', () => {
  auth();
  JsonDiffForm();
  formValidation();
});
