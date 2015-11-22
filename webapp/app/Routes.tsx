import * as React from 'react';
import { Router, Route, Link, Navigation } from 'react-router';
import {App} from './App';
import {Login} from './Login';
import {Logout} from './Logout';
import {Register} from './Register';

import {Characters} from './../characters/Characters';
import {Industry} from './../industry/Industry';

export var routes = (
    <Route path="/" handler={App} >
        <Route name="characters" path="/characters" handler={Characters}/>
        <Route name="industry" path="/industry" handler={Industry}/>
        <Route name="login" path="/login" handler={Login}/>
        <Route name="logout" path="/logout" handler={Logout}/>
        <Route name="register" path="/register" handler={Register}/>
    </Route>
);
