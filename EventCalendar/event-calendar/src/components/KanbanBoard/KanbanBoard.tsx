import React, { useState, useRef, useCallback, useEffect } from "react";
import type { IColumn, Note, NoteState } from "../types/types";
import Column from "../Columns/Column";
import DragNote from "../Notes/Ghosts/DragNote";
import styles from "./KanbanBoard.module.css";
import data from "../common/notesData";

const KanbanBoard: React.FC = () => {
  const [columns, setColumns] = useState<IColumn[]>(data);

  const [dragState, setDragState] = useState<NoteState | null>(null);

  const [previewPos, setPreviewPos] = useState<{ x: number; y: number }>({
    x: 0,
    y: 0,
  });

  const [dropTarget, setDropTarget] = useState<{
    colId: string;
    index: number;
  } | null>(null);

  const columnRefs = useRef<{ [key: string]: HTMLDivElement | null }>({});
  const noteRefs = useRef<{ [key: string]: HTMLDivElement | null }>({});

  const handleDragStart = useCallback(
    (
      idColumn: string,
      idNote: string,
      clientX: number,
      clientY: number,
      offsetX: number,
      offsetY: number
    ) => {
      const findColumnById = (id: string) =>
        columns.find((col) => col.id === id);
      const findNoteIndexById = (notes: Note[], id: string) =>
        notes.findIndex((note) => note.id === id);

      const column = findColumnById(idColumn);
      if (!column) return;

      const noteIndex = findNoteIndexById(column.notes, idNote);
      if (noteIndex === -1) return;

      const noteData = column.notes[noteIndex];

      setDragState({
        idNote,
        idColumn,
        index: noteIndex,
        offsetX,
        offsetY,
        currentNote: noteData,
      });

      setPreviewPos({ x: clientX - offsetX, y: clientY - offsetY });
    },
    [columns]
  );

  const handleMouseMove = useCallback(
    (e: MouseEvent) => {
      if (!dragState) return;

      setPreviewPos({
        x: e.clientX - dragState.offsetX,
        y: e.clientY - dragState.offsetY,
      });

      const targetCol = columns.find((col) => {
        const ref = columnRefs.current[col.id];
        if (!ref) return false;
        const rect = ref.getBoundingClientRect();
        return (
          e.clientX >= rect.left &&
          e.clientX <= rect.right &&
          e.clientY >= rect.top &&
          e.clientY <= rect.bottom
        );
      });

      if (!targetCol) {
        setDropTarget(null);
        return;
      }

      const targetColId = targetCol.id;

      const targetColumn = columns.find((col) => col.id === targetColId);

      if (!targetColumn) {
        setDropTarget(null);
        return;
      }

      const notes = targetColumn.notes;
      let insertionIndex = notes.length;

      notes.some((note, i) => {
        if (
          targetColId === dragState.idColumn &&
          note.id === dragState.idNote
        ) {
          return false;
        }

        const noteElement = noteRefs.current[note.id];
        if (!noteElement) return false;

        const noteRect = noteElement.getBoundingClientRect();
        if (e.clientY < noteRect.top + noteRect.height / 2) {
          insertionIndex = i;
          return true;
        }
        return false;
      });

      if (targetColId === dragState.idColumn) {
        if (
          insertionIndex === dragState.index ||
          insertionIndex === dragState.index + 1
        ) {
          setDropTarget(null);
          return;
        }
      }

      setDropTarget({ colId: targetColId, index: insertionIndex });
    },
    [columns, dragState]
  );

  const handleMouseUp = useCallback(() => {
    if (!dragState) return;
    if (dropTarget) {
      setColumns((prevColumns) => {
        const newColumns = prevColumns.map((col) => ({
          ...col,
          notes: [...col.notes],
        }));

        const originCol = newColumns.find(
          (col) => col.id === dragState.idColumn
        );
        if (!originCol) return prevColumns;

        const noteIndex = originCol.notes.findIndex(
          (c) => c.id === dragState.idNote
        );
        if (noteIndex === -1) return prevColumns;

        const movedNote = originCol.notes[noteIndex];

        originCol.notes = originCol.notes.filter((_, i) => i !== noteIndex);

        const targetCol = newColumns.find((col) => col.id === dropTarget.colId);
        if (!targetCol) return prevColumns;

        let insertIndex = dropTarget.index;
        if (targetCol.id === originCol.id && noteIndex < insertIndex) {
          insertIndex--;
        }

        targetCol.notes = [
          ...targetCol.notes.slice(0, insertIndex),
          movedNote,
          ...targetCol.notes.slice(insertIndex),
        ];

        const updatedColumns = newColumns.map((col) => ({
          ...col,
          notes: col.notes.map((note, idx) => ({
            ...note,
            order: idx + 1,
          })),
        }));

        return updatedColumns;
      });
    }
    setDragState(null);
    setDropTarget(null);
  }, [dragState, dropTarget]);

  useEffect(() => {
    if (dragState) {
      window.addEventListener("mousemove", handleMouseMove);
      window.addEventListener("mouseup", handleMouseUp);
    }
    return () => {
      window.removeEventListener("mousemove", handleMouseMove);
      window.removeEventListener("mouseup", handleMouseUp);
    };
  }, [dragState, handleMouseMove, handleMouseUp]);

  return (
    <div className={styles.kanbanContainer}>
      <div className={styles.kanbanRow}>
        {columns.map((col) => (
          <Column
            key={col.id}
            column={col}
            onNoteMouseDown={handleDragStart}
            dropIndex={
              dropTarget && dropTarget.colId === col.id
                ? dropTarget.index
                : null
            }
            idNoteDrag={dragState ? dragState.idNote : null}
            columnRef={(el: HTMLDivElement | null) => {
              columnRefs.current[col.id] = el;
            }}
            noteRefs={noteRefs}
          />
        ))}
      </div>

      {dragState && (
        <DragNote
          x={previewPos.x}
          y={previewPos.y}
          note={dragState.currentNote}
        />
      )}
    </div>
  );
};

export default KanbanBoard;
