import * as React from 'react';
import {Navigation} from './Navigation';
import ReactRouter = require('react-router');

var RouteHandler = ReactRouter.RouteHandler;


export class App extends React.Component<any, any> {
    render(): JSX.Element {
        return (
            <div>
                <Navigation/>
                <div className="container">
                <RouteHandler/>
                </div>
            </div>
        );
    }
}