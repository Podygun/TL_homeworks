export interface InputRatingProps {
  defaultRating?: number;
  error?: string | null;
  onChange: (rating: number) => void;
}