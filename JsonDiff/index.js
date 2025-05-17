import { Init } from "./auth.js";
import { JsonDiffForm } from "./form-handler.js";
import { FormValidation } from './form-validator.js';


document.addEventListener('DOMContentLoaded', () => {
  Init();
  JsonDiffForm();
  FormValidation();
});
