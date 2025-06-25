import type { IColumn } from "../types/types";

const data: IColumn[] = [
    {
      id: "1",
      title: "Срочно",
      notes: [{ id: "1", content: "Фротенд задачки TL", order: 1 }],
    },
    {
      id: "2",
      title: "Завтра",
      notes: [
        { id: "2", content: "Доделать курсач", order: 1 },
        { id: "3", content: "Съездить к маме", order: 2 },
      ],
    },
    {
      id: "3",
      title: "Не важно",
      notes: [{ id: "4", content: "Сон", order: 1 }],
    },
    {
      id: "4",
      title: "Потом как нибудь",
      notes: [
        { id: "5", content: "Звонок другу", order: 2 },
        { id: "6", content: "Скосить траву", order: 3 },
      ],
    },
  ];

export default data;