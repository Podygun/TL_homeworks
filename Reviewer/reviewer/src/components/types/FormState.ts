export interface FormState {
  errors?: {
    rating?: string;
    name?: string;
    feedback?: string;
  };
  name?: string;
  feedback?: string;
  rating?: number;
}