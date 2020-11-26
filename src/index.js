import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Linker from './Linker';
import NewUser from './NewUser'
import reportWebVitals from './reportWebVitals';
import {Link, BrowserRouter, Switch, Route} from 'react-router-dom';
ReactDOM.render(
  <React.StrictMode>
    <BrowserRouter>
    <Linker />
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);
reportWebVitals();
