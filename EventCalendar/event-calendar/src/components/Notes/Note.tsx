import React, { memo, useCallback, forwardRef, useRef } from "react";
import type { Note, NoteProps } from "../types/types";
import styles from "./Note.module.css";

const calculateOffset = (
  el: HTMLDivElement,
  clientX: number,
  clientY: number
) => {
  const rect = el.getBoundingClientRect();
  return {
    offsetX: clientX - rect.left,
    offsetY: clientY - rect.top,
  };
};

const Note = forwardRef<HTMLDivElement, NoteProps>(
  ({ note, idColumn, onDragStart, isDragging, noteRef }, ref) => {

    const cardElementRef = useRef<HTMLDivElement>(null);
    const handleMouseDown = useCallback(
      (e: React.MouseEvent) => {
        e.preventDefault();
        const element = cardElementRef.current;
        if (!element) return;

        const { offsetX, offsetY } = calculateOffset(element, e.clientX, e.clientY);

        onDragStart(idColumn, note.id, e.clientX, e.clientY, offsetX, offsetY);
      },
      [onDragStart, idColumn, note.id]
    );

    return (
      <div
        ref={(el) => {
          cardElementRef.current = el;
          noteRef(el);
        }}
        className={`${styles.note} ${isDragging ? styles.noteDragging : ""}`}
        onMouseDown={handleMouseDown}
      >
        <div className={styles.cardBody}>
          <p className={styles.cardText}>{note.content}</p>
        </div>
      </div>
    );
  }
);

export default memo(Note);
