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
} from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import styles from "./DictionaryPage.module.scss";
import { useNavigate } from "react-router";
import { useStore } from "../../store/useStore";
import type { DictionaryStore } from "../../store/DictionaryStore";
import type { WordsPair } from "../../types/Pair";
import { PairAction } from "./components/PairAction";

export const DictionaryPage = () => {
  const navigate = useNavigate();
  const pairs = useStore((state: DictionaryStore) => state.pairs);

  const handleGoBack = () => {
    navigate("/");
  };

  return (
    <div className={styles.wrapper}>
      <Box className="form-header">
        <IconButton onClick={handleGoBack} className="back-button">
          <ArrowBackIcon />
        </IconButton>
        <Button
        variant="contained"
        size="large"
        onClick={() => navigate("/newpair")}
      >
        + Добавить слово
      </Button>
      </Box>

      <TableContainer sx={{ backgroundColor: "#fff" }}>
        <Table>
          <TableHead sx={{ backgroundColor: "#e0e4e9" }}>
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
    </div>
  );
};
