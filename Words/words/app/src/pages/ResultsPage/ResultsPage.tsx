import styles from "./ResultsPage.module.scss";
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import MenuBookIcon from "@mui/icons-material/MenuBook";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableRow,
  Typography,
  type SvgIconProps,
} from "@mui/material";
import { useLocation, useNavigate } from "react-router";
import type { Results } from "../../types/Results";
import { useEffect } from "react";

type Row = {
  icon: React.ElementType;
  iconColor: SvgIconProps["color"];
  label: string;
  value: number;
};

export const ResultsPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const result: Results = location.state;

  useEffect(() => {
    if (!result || typeof result.correctCount !== "number") {
      navigate("/", { replace: true });
    }
  }, [result, navigate]);

  if (!result) return null;

  const rows: Row[] = [
    {
      icon: CheckCircleOutlineIcon,
      iconColor: "success",
      label: "Правильные",
      value: result.correctCount,
    },
    {
      icon: HighlightOffIcon,
      iconColor: "error",
      label: "Ошибочные",
      value: result.allCount - result.correctCount,
    },
    {
      icon: MenuBookIcon,
      iconColor: "secondary",
      label: "Всего слов",
      value: result.allCount,
    },
  ];

  return (
    <div className={styles.resultWrapper}>
      <div className={styles.resultInfoWrapper}>
        <Typography color="primary" align="left">
          Ответы
        </Typography>
        <Table>
          <TableBody>
            {rows.map((row, key) => (
              <TableRow key={key}>
                <TableCell size="small">
                  <row.icon color={row.iconColor}></row.icon>
                </TableCell>
                <TableCell>
                  <Typography>{row.label}</Typography>
                </TableCell>
                <TableCell>{row.value}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
      <div className={styles.resultMenuWrapper}>
        <Button variant="contained" onClick={() => navigate("/check")}>
          Проверить знания ещё раз
        </Button>
        <Button variant="outlined" onClick={() => navigate("/")}>
          Вернуться в начало
        </Button>
      </div>
    </div>
  );
};
