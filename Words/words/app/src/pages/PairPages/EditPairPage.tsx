import { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import {
  Container,
  TextField,
  Button,
  Typography,
  IconButton,
  Box,
  Paper,
} from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { useStore } from "../../store/useStore";
import type { WordsPair } from "../../types/Pair";
import "./Pairs.scss";

export const EditPairPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { edit } = useStore();

  const pair = (location.state as { pair: WordsPair })?.pair;

  const [russianWord, setRussianWord] = useState(pair?.ru || "");
  const [englishWord, setEnglishWord] = useState(pair?.en || "");

  const isSaveButtonDisabled = !russianWord.trim() || !englishWord.trim();

  useEffect(() => {
    if (!pair) {
      navigate("/dictionary");
    }
  }, [pair, navigate]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (russianWord.trim() && englishWord.trim() && pair) {
      edit(
        { ru: pair.ru, en: pair.en },
        { ru: russianWord.trim(), en: englishWord.trim() }
      );
      navigate("/dictionary");
    }
  };

  const handleCancel = () => {
    navigate("/dictionary");
  };

  return (
    <Container maxWidth="sm" className="add-word-container">
      <Paper elevation={3} className="form-paper">
        <Box className="form-header">
          <IconButton onClick={handleCancel} className="back-button">
            <ArrowBackIcon />
          </IconButton>
          <Typography variant="h5" component="h1" className="title">
            Редактирование слова
          </Typography>
        </Box>

        <form onSubmit={handleSubmit} className="word-form">
          <Typography variant="h6" gutterBottom className="form-title">
            Измените слово
          </Typography>

          <TextField
            label="Слово на русском языке"
            variant="outlined"
            fullWidth
            margin="normal"
            value={russianWord}
            onChange={(e) => setRussianWord(e.target.value)}
            required
            className="form-input"
          />

          <TextField
            label="Перевод на английский язык"
            variant="outlined"
            fullWidth
            margin="normal"
            value={englishWord}
            onChange={(e) => setEnglishWord(e.target.value)}
            required
            className="form-input"
          />

          <Box className="form-buttons">
            <Button onClick={handleCancel} className="cancel-button">
              Отменить
            </Button>
            <Button
              type="submit"
              className="submit-button"
              disabled={isSaveButtonDisabled}
            >
              Сохранить
            </Button>
          </Box>
        </form>
      </Paper>
    </Container>
  );
};
