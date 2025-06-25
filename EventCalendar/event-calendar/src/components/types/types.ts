export interface Note {
  id: string;
  content: string;
  order: number;
};

export interface NoteProps {
  note: Note;
  idColumn: string;
  onDragStart: (
    idColumn: string,
    idNote: string,
    clientX: number,
    clientY: number,
    offsetX: number,
    offsetY: number
  ) => void;
  isDragging: boolean;
  noteRef: (el: HTMLDivElement | null) => void;
};

export interface IColumn {
  id: string;
  title: string;
  notes: Note[];
};

export interface ColumnProps {
  column: IColumn;
  onNoteMouseDown: (
    idColumn: string,
    idNote: string,
    clientX: number,
    clientY: number,
    offsetX: number,
    offsetY: number
  ) => void;
  dropIndex: number | null;
  idNoteDrag: string | null;
  columnRef: (el: HTMLDivElement | null) => void;
  noteRefs: React.MutableRefObject<{ [key: string]: HTMLDivElement | null }>;
};

export interface NoteState {
  idNote: string;
  idColumn: string;
  index: number;
  offsetX: number;
  offsetY: number;
  currentNote: Note;
};

export interface PreviewProps {
  x: number;
  y: number;
  note: Note;
};