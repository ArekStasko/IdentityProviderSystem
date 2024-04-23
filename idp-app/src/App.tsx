import React from 'react';
import logo from './logo.svg';
import './App.css';
import {BrowserRouter} from "react-router-dom";
import {createTheme, ThemeProvider} from "@mui/material";
import MainRouting from "./app/routing/mainRouting";

const darkTheme = createTheme({
  palette: {
    mode: 'dark'
  }
});

function App() {
  return (
      <BrowserRouter>
        <ThemeProvider theme={darkTheme}>
          <div className="App">
            <MainRouting />
          </div>
        </ThemeProvider>
      </BrowserRouter>
  );
}

export default App;
