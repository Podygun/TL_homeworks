import React, { memo, useCallback, useRef } from "react";
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

const Note = ({
  note,
  idColumn,
  onDragStart,
  isDragging,
  noteRef,
}: NoteProps) => {
  const noteElementRef = useRef<HTMLDivElement>(null);
  const handleMouseDown = useCallback(
    (e: React.MouseEvent) => {
      e.preventDefault();
      const element = noteElementRef.current;
      if (!element) return;

      const { offsetX, offsetY } = calculateOffset(
        element,
        e.clientX,
        e.clientY
      );

      onDragStart(idColumn, note.id, e.clientX, e.clientY, offsetX, offsetY);
    },
    [onDragStart, idColumn, note.id]
  );

  return (
    <div
      ref={(el) => {
        noteElementRef.current = el;
        noteRef(el);
      }}
      className={`${styles.note} ${isDragging ? styles.noteDragging : ""}`}
      onMouseDown={handleMouseDown}
    >
      <div className={styles.noteBody}>
        <p className={styles.noteText}>{note.content}</p>
      </div>
    </div>
  );
};

export default memo(Note);
