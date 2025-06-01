// import React, { useState } from "react";
// import "./App.css";

// const App = () => {
//   const [cardList, setCardList] = useState([
//     { id: 1, order: 3, text: "Third" },
//     { id: 2, order: 1, text: "One" },
//     { id: 3, order: 2, text: "Second" },
//     { id: 4, order: 4, text: "Four" },
//   ]);
//   const [currentCard, setCurrentCard] = useState(null);

//   function dragStartHandler(
//     e: React.DragEvent<HTMLDivElement>,
//     card: { id: number; order: number; text: string }
//   ): void {
//     console.log("drag", card);
//     setCurrentCard(card);
//   }

//   function dragEndHandler(e: React.DragEvent<HTMLDivElement>): void {
//     e.target.style.background = "transparent";
//   }

//   function dragOverHandler(e: React.DragEvent<HTMLDivElement>): void {
//     e.preventDefault();
//     e.target.style.background = "lightgray";
//   }

//   function dropHandler(
//     e: React.DragEvent<HTMLDivElement>,
//     card: { id: number; order: number; text: string }
//   ): void {
//     e.preventDefault();
//     setCardList(
//       cardList.map((c) => {
//         if (c.id === card.id) {
//           return { ...c, order: currentCard.order };
//         }
//         if (c.id === currentCard.id) {
//           return { ...c, order: card.order };
//         }
//         return c;
//       })
//     );
//     e.target.style.background = "transparent";
//   }

//   const sortCards = (a, b) => {
//     if (a.order > b.order) {
//       return 1;
//     } else {
//       return -1;
//     }
//   };

//   return (
//     <div className="app">
//       {cardList.sort(sortCards).map((card) => (
//         <div
//           onDragStart={(e) => dragStartHandler(e, card)}
//           onDragLeave={(e) => dragEndHandler(e)}
//           onDragEnd={(e) => dragEndHandler(e)}
//           onDragOver={(e) => dragOverHandler(e)}
//           onDrop={(e) => dropHandler(e, card)}
//           className={"card"}
//           draggable={true}
//         >
//           {card.text}
//         </div>
//       ))}
//     </div>
//   );
// };

// export default App;

import React, { useState } from "react";
import "./App.css";

const App = () => {
  interface BoardItem {
    id: number;
    title: string;
  }

  interface Board {
    id: number;
    title: string;
    items: BoardItem[];
  }

  const [boards, setBoards] = useState<Board[]>([
    {
      id: 1,
      title: "Понедельник",
      items: [
        { id: 1, title: "Go on a date" },
        { id: 2, title: "Go play football" },
      ],
    },
    {
      id: 2,
      title: "Вторник",
      items: [{ id: 3, title: "Read a book" }],
    },
    {
      id: 3,
      title: "Среда",
      items: [
        { id: 4, title: "Wash shirts" },
        { id: 5, title: "Do home assignment" },
      ],
    },
    {
      id: 4,
      title: "Четверг",
      items: [{ id: 6, title: "Buy a car" }],
    },
  ]);

  const [currentBoard, setCurrentBoard] = useState(null);
  const [currentItem, setCurrentItem] = useState(null);

  function dragOverHandler(e: React.DragEvent<HTMLDivElement>): void {
    e.preventDefault();
    if (e.target.className === "item") {
      e.target.style.boxShadow = "0 4px 3px gray";
    }
  }

  function dragLeaveHandler(e: React.DragEvent<HTMLDivElement>): void {
    e.target.style.boxShadow = "none ";
  }

  function dragStartHandler(
    e: React.DragEvent<HTMLDivElement>,
    board: Board,
    item: BoardItem
  ): void {
    setCurrentBoard(board);
    setCurrentItem(item);
  }

  function dragEndHandler(e: React.DragEvent<HTMLDivElement>): void {
    e.target.style.boxShadow = "none";
  }

  function dropHandler(
    e: React.DragEvent<HTMLDivElement>,
    board: Board,
    item: BoardItem
  ): void {
    e.preventDefault();

    const currentIndex = currentBoard.items.indexOf(currentItem);
    currentBoard.items.splice(currentIndex, 1);

    const dropIndex = board.items.indexOf(item);
    board.items.splice(dropIndex + 1, 0, currentItem);

    setBoards(
      boards.map((b) => {
        if (b.id === board.id) {
          return board;
        }
        if (b.id === currentBoard.id) {
          return currentBoard;
        }
        return b;
      })
    );
  }

  function dropCardHandler(
    e: React.DragEvent<HTMLDivElement>,
    board: Board
  ): void {
    board.items.push(currentItem);
    const currentIndex = currentBoard.items.indexOf(currentItem);
    currentBoard.items.splice(currentIndex, 1);

    setBoards(
      boards.map((b) => {
        if (b.id === board.id) {
          return board;
        }
        if (b.id === currentBoard.id) {
          return currentBoard;
        }
        return b;
      })
    );
  }

  return (
    <div className="app">
      {boards.map((board) => (
        <div
          className="board"
          onDragOver={(e) => dragOverHandler(e)}
          onDrop={(e) => dropCardHandler(e, board)}
        >
          <div className="board__title">{board.title}</div>
          {board.items.map((item) => (
            <div
              className="item"
              draggable={true}
              onDragOver={(e) => dragOverHandler(e)}
              onDragStart={(e) => dragStartHandler(e, board, item)}
              onDragLeave={(e) => dragLeaveHandler(e)}
              onDragEnd={(e) => dragEndHandler(e)}
              onDrop={(e) => dropHandler(e, board, item)}
            >
              {item.title}
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};

export default App;
