import * as React from 'react';
import {IAuthServiceContext} from './../api/AuthService';
import {IStoreContext} from './IStoreContext';
import {createLogoutAction} from './../actions/LoginActions';


export class Logout extends React.Component<any, any> {
    context: IStoreContext & IAuthServiceContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired,
        authService: React.PropTypes.object.isRequired
    };

    componentDidMount():void {
        var that = this;
        this.context.authService.logout().then(function() {
            that.context.store.dispatch(createLogoutAction());
        });
    }

    render(): JSX.Element {
        return (
            <p>You are now logged out</p>
        );
    }
}

