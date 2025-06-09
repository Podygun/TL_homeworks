import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { HomePage } from "./pages/HomePage/HomePage";
import { DictionaryPage } from "./pages/DictionaryPage/DictionaryPage";
import { NewPairPage } from "./pages/PairPages/NewPairPage";
import { EditPairPage } from "./pages/PairPages/EditPairPage";
import { TestingPage } from "./pages/TestingPage/TestingPage";
import { ResultsPage } from "./pages/ResultsPage/ResultsPage";
import "./styles/app.scss";

const App: React.FC = () => {
  return (
      <Router>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="dictionary" element={<DictionaryPage />} />
          <Route path="newpair" element={<NewPairPage />} />
          <Route path="editpair" element={<EditPairPage />} />
          <Route path="testing" element={<TestingPage />} />
          <Route path="results" element={<ResultsPage />} />
        </Routes>
      </Router>
  );
};

export default App;
