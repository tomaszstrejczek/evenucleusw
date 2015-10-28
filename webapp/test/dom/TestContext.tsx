import * as React from 'react';
import * as ReactAddons from 'react/addons';

import { Route, run, TestLocation } from 'react-router';

export class TestContext {
    static getRouterComponent(TargetComponent: any): ReactAddons.Component<any, any> {
        var div = document.createElement('div');
        var routes = (
            <Route path="/">
                <Route name="test" handler={TargetComponent}/>
            </Route>
        );
        var component: ReactAddons.Component<any, any>;

        var location = new TestLocation(['/test']);
        run(routes, location, function (Handler: new () => React.Component<any, any>) {
            var mainComponent = React.render(<Handler/>, div);
            component = ReactAddons.addons.TestUtils.findRenderedComponentWithType(mainComponent, TargetComponent);
        });

        return component;
    }
};