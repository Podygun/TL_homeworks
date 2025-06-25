import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import {
  Container,
  Paper,
  Box,
  IconButton,
  Typography,
  TextField,
  Button,
} from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { useStore } from "../../store/useStore";
import type { WordsPair } from "../../types/Pair";
import "./Pairs.scss";

interface PairPageProps {
  mode: "edit" | "new";
}

export const PairPage: React.FC<PairPageProps> = ({ mode }) => {
  const navigate = useNavigate();
  const location = useLocation();
  const { add, edit } = useStore();

  const pair = (location.state as { pair?: WordsPair })?.pair;

  const [russianWord, setRussianWord] = useState(mode === "edit" && pair ? pair.ru : "");
  const [englishWord, setEnglishWord] = useState(mode === "edit" && pair ? pair.en : "");

  const isSaveButtonDisabled = !russianWord.trim() || !englishWord.trim();

  useEffect(() => {
    if (mode === "edit" && !pair) {
      navigate("/dictionary");
    }
  }, [mode, pair, navigate]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const ruTrimmed = russianWord.trim();
    const enTrimmed = englishWord.trim();

    if (!ruTrimmed || !enTrimmed) return;

    if (mode === "edit" && pair) {
      edit({ ru: pair.ru, en: pair.en }, { ru: ruTrimmed, en: enTrimmed });
    } else if (mode === "new") {
      add({ ru: ruTrimmed, en: enTrimmed });
    }

    navigate("/dictionary");
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
            {mode === "edit" ? "Редактирование слова" : "Добавление слова"}
          </Typography>
        </Box>

        <form onSubmit={handleSubmit} className="word-form">
          <Typography variant="h6" gutterBottom className="form-title">
            {mode === "edit" ? "Измените слово" : "Словарное слово"}
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
