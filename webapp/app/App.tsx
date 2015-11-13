import * as React from 'react';
import {Navigation} from './Navigation';
import ReactRouter = require('react-router');
import {KernelCreator} from './kernel';
import {Kernel} from './../node_modules/inversify/source/inversify';
import {IKernelContext} from './IKernelContext';

import { bindActionCreators } from 'redux';

var RouteHandler = ReactRouter.RouteHandler;

var kernelSingleton = KernelCreator.create();

export class App extends React.Component<any, any> implements React.ChildContextProvider<IKernelContext> {

    static childContextTypes: React.ValidationMap<any> = {
        kernel: React.PropTypes.object
    };

    getChildContext(): IKernelContext {
        return { kernel: kernelSingleton };
    };

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