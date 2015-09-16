import * as React from 'react';
import { Router, Route, Link, Navigation } from 'react-router';
import {App} from './App';

//import Characters from 'characters/Characters.jsx';
//import Login from 'app/Login.jsx';
//import Industry from 'industry/Industry.jsx';
//import App from 'app/App.jsx';
//import Logout from 'app/Logout.jsx';


//var routes = (
//    <Route path="/" handler={App}>
//        <Route name="characters" path="/characters" handler={Characters}/>
//        <Route name="industry" path="/industry" handler={Industry}/>
//        <Route name="login" path="/login" handler={Login}/>
//        <Route name="logout" path="/logout" handler={Logout}/>
//    </Route>    
//);


//module.exports = routes;
export var routes = (
    <Route path="/" handler={App} />

);
