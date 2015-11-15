import * as React from 'react';
import * as ReactAddons from 'react/addons';
import * as ReactDOM from 'react-dom';

import { Route, run, TestLocation } from 'react-router';
import {AppState} from './../../app/AppState';
import {rootReducer} from './../../actions/rootReducer';
import {Store, createStore} from 'redux';
import {Provider} from 'react-redux';
import {IKernelContext} from './../../app/IKernelContext';
import {KernelCreator} from './../../app/kernel';
import ReactRouter = require('react-router');

var RouteHandler = ReactRouter.RouteHandler;
var kernelSingleton = KernelCreator.create();


export class TestApp extends React.Component<any, any> implements React.ChildContextProvider<IKernelContext> {

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
                <div className="container">
                <RouteHandler/>
                </div>
            </div>
        );
    }
}

export class TestContext {
    static getRouterComponent(TargetComponent: any): ReactAddons.Component<any, any> {
        var div = document.createElement('div');

        var routes = (
            <Route path="/" handler={TestApp}>
                <Route name="test" handler={TargetComponent}/>
            </Route>
        );
        var component: ReactAddons.Component<any, any>;

        var location = new TestLocation(['/test']);
        const store: Store = createStore(rootReducer, new AppState());

        run(routes, location, function (Handler: new () => React.Component<any, any>) {
            var mainComponent = ReactDOM.render(
                <Provider store={store}>
                    <Handler/>
                </Provider>
                , div);
            component = ReactAddons.addons.TestUtils.findRenderedComponentWithType(mainComponent, TargetComponent);
        });

        return component;
    }
};