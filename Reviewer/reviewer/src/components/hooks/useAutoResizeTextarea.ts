import { useEffect, useRef } from "react";

export function useAutoResizeTextarea() {
  const textareaRef = useRef<HTMLTextAreaElement>(null);
  const formRef = useRef<HTMLFormElement>(null);

  useEffect(() => {
    const textarea = textareaRef.current;
    const form = formRef.current;

    if (!textarea || !form) return;

    const adjustHeight = () => {
      textarea.style.height = "auto";
      textarea.style.height = `${textarea.scrollHeight}px`;
    };

    textarea.addEventListener("input", adjustHeight);
    return () => textarea.removeEventListener("input", adjustHeight);
  }, []);

  return { textareaRef, formRef };
}
