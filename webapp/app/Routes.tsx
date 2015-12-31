import * as React from 'react';
import { Router, Route, Link, Navigation } from 'react-router';
import {App} from './App';
import {Login} from './Login';
import {Logout} from './Logout';
import {Register} from './Register';

import {Characters} from './../characters/Characters';
import {PilotDetails} from './../characters/PilotDetails';
import {Industry} from './../industry/Industry';
import {Keys} from './../keys/Keys';

export var routes = (
    <Route path="/" handler={App} >
        <Route name="characters" path="/characters" handler={Characters}/>
        <Route name="pilot" path="/pilot/:name" handler={PilotDetails}/>
        <Route name="industry" path="/industry" handler={Industry}/>
        <Route name="keys" path="/keys" handler={Keys}/>
        <Route name="login" path="/login" handler={Login}/>
        <Route name="logout" path="/logout" handler={Logout}/>
        <Route name="register" path="/register" handler={Register}/>
    </Route>
);
