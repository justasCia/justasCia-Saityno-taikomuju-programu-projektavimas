import './App.css';
import {
  BrowserRouter as Router,
  Switch,
  Route
} from "react-router-dom";
import Topics from './pages/Topics';
import { CssBaseline, GlobalStyles} from '@mui/material';
import React from 'react';
import { Container } from '@mui/system';
import NavBar from './components/NavBar';
import Home from './pages/Home';
import NotFound from './pages/NotFound';

const App = () => {
  return (
    <React.Fragment>
      <GlobalStyles styles={{ ul: { margin: 0, padding: 0, listStyle: 'none' } }} />
      <CssBaseline />

      <Router>
        <NavBar />
        <Container>
          <Switch>
            <Route path="/topics">
              <Topics />
            </Route>
            <Route exact path="/">
              <Home />
            </Route>
            <Route path="/">
              <NotFound />
            </Route>
          </Switch>
        </Container>
      </Router>

    </React.Fragment>
  );
}

export default App;
