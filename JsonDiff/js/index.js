import { authentication } from './auth/auth.js';
import { jsonDiffForm } from './form/form-handler.js';
import { formValidation } from './form/form-validator.js';
import { pagination } from './services/pagination.js'

authentication();
pagination();
jsonDiffForm();
formValidation();