import React, { memo } from "react";
import type { ColumnProps } from "../types/types";
import NewNotePreview from "../Notes/Ghosts/NewNotePreview";
import Note from "../Notes/Note";
import styles from './Column.module.css';

const Column: React.FC<ColumnProps> = memo(
  ({
    column,
    onNoteMouseDown,
    dropIndex,
    idNoteDrag,
    columnRef,
    noteRefs,
  }) => {
    return (
      <div className={styles.column} ref={columnRef}>
        <div className={styles.noteContainer}>
          <div className={styles.noteBody}>
            <h5 className={styles.noteTitle}>{column.title}</h5>
            {dropIndex === 0 && <NewNotePreview />}
            {column.notes.map((note, index) => (
              <React.Fragment key={note.id}>
                <Note
                  note={note}
                  idColumn={column.id}
                  onDragStart={onNoteMouseDown}
                  isDragging={idNoteDrag === note.id}
                  noteRef={(el: HTMLDivElement | null) => {
                    noteRefs.current[note.id] = el;
                  }}
                />
                {dropIndex === index + 1 && <NewNotePreview />}
              </React.Fragment>
            ))}
          </div>
        </div>
      </div>
    );
  }
);

export default Column;