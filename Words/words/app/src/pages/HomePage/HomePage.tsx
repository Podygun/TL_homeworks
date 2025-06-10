import { Button, Container, Typography, Paper } from "@mui/material";
import { useNavigate } from "react-router";
import "./HomePage.scss";

export const HomePage = () => {
  const navigate = useNavigate();

  return (
    <Container className="home-container">
      <Paper elevation={3} className="form-paper">
        <Typography variant="h4" align="center" gutterBottom>
          Выберите режим
        </Typography>
        <div className="button-container">
          <Button
            variant="contained"
            color="primary"
            className="home-button"
            onClick={() => navigate("/dictionary")}
          >
            ЗАПОЛНИТЬ СЛОВАРЬ
          </Button>
          <Button
            variant="contained"
            color="secondary"
            className="home-button"
            onClick={() => navigate("/testing")}
          >
            ПРОВЕРИТЬ ЗНАНИЯ
          </Button>
        </div>
      </Paper>
    </Container>
  );
};
