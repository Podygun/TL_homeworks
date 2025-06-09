import { useCallback, useState } from "react";

export const usePairInputForm = () => {
  const [state, setState] = useState({
    ru: '',
    en: ''
  });

  const handleChange = useCallback((field: keyof typeof state) => 
    (value: string) => {
      setState(prev => ({ ...prev, [field]: value }));
    },
  []);

  return { state, handleChange };
};