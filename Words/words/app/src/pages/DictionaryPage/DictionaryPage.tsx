import {
  Box,
  Button,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper
} from "@mui/material";
import "./DictionaryPage.scss";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { useNavigate } from "react-router";
import { useStore } from "../../store/useStore";
import type { DictionaryStoreType } from "../../store/DictionaryStoreType";
import type { WordsPair } from "../../types/Pair";
import { PairAction } from "./components/PairAction/PairAction";

export const DictionaryPage = () => {
  const navigate = useNavigate();
  const pairs = useStore((state: DictionaryStoreType) => state.pairs);

  const handleGoBack = () => {
    navigate("/");
  };

  return (
    <div className="wrapper">
      <Box className="header">
        <IconButton onClick={handleGoBack} className="back-button">
          <ArrowBackIcon />
        </IconButton>
        <Button className="addpair-button" onClick={() => navigate("/newpair")}>
          + Добавить слово
        </Button>
      </Box>

      <Paper elevation={0} className="table-paper">
        <TableContainer className="table-container">
          <Table>
            <TableHead className="table-head">
              <TableRow>
                <TableCell>Слово на русском языке</TableCell>
                <TableCell>Перевод на английский язык</TableCell>
                <TableCell>Действие</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {pairs.map((pair: WordsPair, key) => (
                <TableRow key={key}>
                  <TableCell>{pair.ru}</TableCell>
                  <TableCell>{pair.en}</TableCell>
                  <TableCell>
                    <PairAction pair={pair} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>
    </div>
  );
};
