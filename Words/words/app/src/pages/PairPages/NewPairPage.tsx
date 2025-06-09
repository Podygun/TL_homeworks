import { useState } from "react";
import { useNavigate } from "react-router-dom";
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
import "./Pairs.scss";

export const NewPairPage = () => {
  const navigate = useNavigate();
  const { add } = useStore();

  const [russianWord, setRussianWord] = useState("");
  const [englishWord, setEnglishWord] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (russianWord.trim() && englishWord.trim()) {
      add({ ru: russianWord, en: englishWord });
      navigate('/dictionary');
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
            Добавление слова
          </Typography>
        </Box>

        <form onSubmit={handleSubmit} className="word-form">
          <Typography variant="h6" gutterBottom className="form-title">
            Словарное слово
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
            <Button 
              variant="contained" 
              color="secondary"
              onClick={handleCancel}
              className="cancel-button"
            >
              Отменить
            </Button>
            <Button 
              type="submit" 
              variant="contained" 
              color="primary"
              className="submit-button"
            >
              Сохранить
            </Button>
          </Box>
        </form>
      </Paper>
    </Container>
  );
};
