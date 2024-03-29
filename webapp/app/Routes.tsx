﻿import * as React from 'react';
import { Router, Route, RouterState, RedirectFunction} from 'react-router';
import * as History from 'history';

import {App} from './App';
import {Login} from './Login';
import {Logout} from './Logout';
import {Register} from './Register';

import {Characters} from './../characters/Characters';
import {PilotDetails} from './../characters/PilotDetails';
import {Industry} from './../industry/Industry';
import {Keys} from './../keys/Keys';
import {KeyAdd} from './../keys/KeyAdd';
import {NotFound} from './../special/NotFound';
import {Store, createStore, applyMiddleware} from 'redux';
import {IAppState} from './AppState';


export function createRoutes(store: Store): JSX.Element {
    function onEnter(nextState: RouterState, replaceState: RedirectFunction, callback?: Function) {
        var state = store.getState() as IAppState;
        if (!state.loginInfo.isLoggedIn())
            replaceState({ nextPathname: nextState.location.pathname }, '/login');
        callback();
    }

    return <Router history={History.createHistory()}>
             <Route path="/" component={App} >
                 <Route path="characters" component={Characters} onEnter={onEnter}/>
                 <Route path="pilot/:name" component={PilotDetails} onEnter={onEnter}/>
                 <Route path="industry" component={Industry}/>
                 <Route path="keys" component={Keys} onEnter={onEnter}/>
                 <Route path="keyadd" component={KeyAdd} onEnter={onEnter}/>
                 <Route path="login" component={Login}/>
                 <Route path="logout" component={Logout}/>
                 <Route path="register" component={Register}/>
                 <Route path="*" component={NotFound}/>
            </Route>
        </Router>;
}
    

