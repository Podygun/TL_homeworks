import { useEffect, useMemo, useState } from "react";
import {
  Container,
  Typography,
  TextField,
  Button,
  FormControl,
  Select,
  MenuItem,
  ListItemText,
  Checkbox,
  Paper,
  Box,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useStore } from "../../store/useStore";
import type { WordsPair } from "../../types/Pair";

const shuffleArray = <T,>(array: T[]): T[] => {
  return [...array].sort(() => Math.random() - 0.5);
};

export const TestingPage = () => {
  const navigate = useNavigate();
  const { pairs } = useStore();

  const [shuffledPairs, setShuffledPairs] = useState<WordsPair[]>([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [selectedAnswer, setSelectedAnswer] = useState<string | null>(null);
  const [correctCount, setCorrectCount] = useState(0);

  useEffect(() => {
    setShuffledPairs(shuffleArray(pairs));
  }, [pairs]);

  const currentPair = shuffledPairs[currentIndex];

  const options = useMemo(() => {
    if (!currentPair) return [];

    const otherOptions = pairs
      .filter((p) => p.en !== currentPair.en)
      .slice(0, 3);

    return shuffleArray([...otherOptions, currentPair]);
  }, [currentPair, pairs]);

  const handleCheck = () => {
    if (!selectedAnswer || !currentPair) return;

    const isCorrect = selectedAnswer === currentPair.en;
    const newCorrectCount = isCorrect ? correctCount + 1 : correctCount;
    const isLastQuestion = currentIndex + 1 >= shuffledPairs.length;

    if (isLastQuestion) {
      navigate("/results", {
        state: {
          TotalAmount: shuffledPairs.length,
          CorrectAmount: newCorrectCount,
        },
      });
    } else {
      setCorrectCount(newCorrectCount);
      setCurrentIndex((prev) => prev + 1);
      setSelectedAnswer(null);
    }
  };

  if (!currentPair) {
    return (
      <Container>
        <Typography variant="h6">Нет слов для тестирования</Typography>
      </Container>
    );
  }

  return (
    <Container maxWidth="sm">
      <Paper elevation={3} sx={{ padding: 3, marginTop: 4 }}>
        <Typography variant="h4" gutterBottom>
          Проверка знаний
        </Typography>

        <Typography variant="subtitle1" gutterBottom>
          Слово: {currentIndex + 1} из {shuffledPairs.length}
        </Typography>

        <Box sx={{ marginBottom: 2 }}>
          <Typography variant="subtitle2">Слово на русском языке</Typography>
          <TextField
            value={currentPair.ru}
            fullWidth
            InputProps={{ readOnly: true }}
            sx={{ marginTop: 1 }}
          />
        </Box>

        <Box sx={{ marginBottom: 2 }}>
          <Typography variant="subtitle2">
            Перевод на английский язык
          </Typography>
          <FormControl fullWidth sx={{ marginTop: 1 }}>
            <Select
              value={selectedAnswer || ""}
              onChange={(e) => setSelectedAnswer(e.target.value)}
              renderValue={(selected) => selected}
            >
              {options.map((option) => (
                <MenuItem key={option.en} value={option.en}>
                  <Checkbox checked={selectedAnswer === option.en} />
                  <ListItemText primary={option.en} />
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Box>

        <Button
          variant="contained"
          color="primary"
          onClick={handleCheck}
          disabled={!selectedAnswer}
        >
          {currentIndex + 1 < shuffledPairs.length ? "Далее" : "Завершить"}
        </Button>
      </Paper>
    </Container>
  );
};
