import React from "react";
import type { PreviewProps } from "../../types/types";
import styles from './GhostNotes.module.css';

const DragNote: React.FC<PreviewProps> = ({ x, y, note }) => {
  return (
    <div
      className={styles.dragNotePreview}
      style={{
        top: y,
        left: x,
      }}
    >
      <div className={styles.previewBody}>
        <p className={styles.previewText}>{note.content}</p>
      </div>
    </div>
  );
};

export default DragNote;