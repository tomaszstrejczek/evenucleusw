import * as React from 'react';
import {Navigation} from './Navigation';
import ReactRouter = require('react-router');

import { bindActionCreators } from 'redux';

var RouteHandler = ReactRouter.RouteHandler;


export class App extends React.Component<any, any> {
    render(): JSX.Element {
        const { state, dispatch } = this.props;

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