import * as React from 'react';
import { Router, Route} from 'react-router';

import {App} from './App';
import {Login} from './Login';
import {Logout} from './Logout';
import {Register} from './Register';

import {Characters} from './../characters/Characters';
import {PilotDetails} from './../characters/PilotDetails';
import {Industry} from './../industry/Industry';
import {Keys} from './../keys/Keys';


export var routes = (
    <Router>
        <Route path="/" component={App} >
            <Route path="/characters" component={Characters}/>
            <Route path="/pilot/:name" component={PilotDetails}/>
            <Route path="/industry" component={Industry}/>
            <Route path="/keys" component={Keys}/>
            <Route path="/login" component={Login}/>
            <Route path="/logout" component={Logout}/>
            <Route path="/register" component={Register}/>
        </Route>
    </Router>
);
