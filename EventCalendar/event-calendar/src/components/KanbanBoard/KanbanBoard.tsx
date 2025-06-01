import React, { useState, useRef, useCallback, useEffect } from "react";
import type { IColumn, Note, NoteState } from "../types/types";
import Column from "../Columns/Column";
import DragNote from "../Notes/Ghosts/DragNote";
import styles from "./Kanban.module.css";
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
  const cardRefs = useRef<{ [key: string]: HTMLDivElement | null }>({});

  const findColumnById = (id: string) => columns.find(col => col.id === id);
  const findNoteIndexById = (notes: Note[], id: string) => notes.findIndex(note => note.id === id);

  const handleDragStart = useCallback(
    (
      idColumn: string,
      idNote: string,
      clientX: number,
      clientY: number,
      offsetX: number,
      offsetY: number
    ) => {

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

      let targetColId: string | null = null;
      columns.forEach((col) => {
        const ref = columnRefs.current[col.id];
        if (!ref) return;
        const rect = ref.getBoundingClientRect();
        if (
          e.clientX >= rect.left &&
          e.clientX <= rect.right &&
          e.clientY >= rect.top &&
          e.clientY <= rect.bottom
        ) {
          targetColId = col.id;
        }
      });

      if (!targetColId) {
        setDropTarget(null);
        return;
      }

      const targetColumn = columns.find((col) => col.id === targetColId)!;
      const cards = targetColumn.notes;
      let insertionIndex = cards.length;
      for (let i = 0; i < cards.length; i++) {
        const card = cards[i];
        if (
          targetColId === dragState.idColumn &&
          card.id === dragState.idNote
        ) {
          continue;
        }
        const cardElement = cardRefs.current[card.id];
        if (!cardElement) continue;
        const cardRect = cardElement.getBoundingClientRect();
        const cardCenterY = cardRect.top + cardRect.height / 2;
        if (e.clientY < cardCenterY) {
          insertionIndex = i;
          break;
        }
      }

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
        )!;
        const cardIndex = originCol.notes.findIndex(
          (c) => c.id === dragState.idNote
        );
        const [movedCard] = originCol.notes.splice(cardIndex, 1);
        const targetCol = newColumns.find(
          (col) => col.id === dropTarget.colId
        )!;
        let insertIndex = dropTarget.index;
        if (targetCol.id === originCol.id && cardIndex < insertIndex) {
          insertIndex--;
        }
        targetCol.notes.splice(insertIndex, 0, movedCard);
        newColumns.forEach((col) => {
          col.notes.forEach((note, idx) => {
            note.order = idx + 1;
          });
        });
        return newColumns;
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
            noteRefs={cardRefs}
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
