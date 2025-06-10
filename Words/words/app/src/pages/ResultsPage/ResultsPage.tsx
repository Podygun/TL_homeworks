import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline";
import MenuBookIcon from "@mui/icons-material/MenuBook";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import {
  Box,
  Button,
  Paper,
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
import "./ResultsPage.scss";


interface ResultsRow {
  icon: React.ElementType;
  iconColor: SvgIconProps["color"];
  description: string;
  value: number;
};


export const ResultsPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const result: Results = location.state;

  useEffect(() => {
    if (!result || typeof result.CorrectAmount !== "number") {
      navigate("/", { replace: true });
    }
  }, [result, navigate]);

  if (!result) return null;

  const rows: ResultsRow[] = [
    {
      icon: CheckCircleOutlineIcon,
      iconColor: "success",
      description: "Правильные",
      value: result.CorrectAmount,
    },
    {
      icon: HighlightOffIcon,
      iconColor: "error",
      description: "Ошибочные",
      value: result.TotalAmount - result.CorrectAmount,
    },
    {
      icon: MenuBookIcon,
      iconColor: "secondary",
      description: "Всего слов",
      value: result.TotalAmount,
    },
  ];

  return (
    <div className="resultWrapper">
      <Typography variant="h4" className="resultTitle">
        Результат проверки знаний
      </Typography>
      <Paper elevation={2} className="resultInfoWrapper">
        <Typography color="primary" align="left" variant="h6">
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
                  <Typography>{row.description}</Typography>
                </TableCell>
                <TableCell>{row.value}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>

      <Box className="resultMenuWrapper">
        <Button variant="contained" onClick={() => navigate("/testing")}>
          Проверить знания ещё раз
        </Button>
        <Button variant="outlined" onClick={() => navigate("/")}>
          Вернуться в начало
        </Button>
      </Box>
    </div>
  );
};
