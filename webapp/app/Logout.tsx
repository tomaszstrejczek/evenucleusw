import * as React from 'react';
import {IAuthService} from './../api/AuthService';
import {IStoreContext} from './IStoreContext';
import {IKernelContext} from './IKernelContext';
import {createLogoutAction} from './../actions/LoginActions';


export class Logout extends React.Component<any, any> {
    context: IStoreContext & IKernelContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired,
        kernel: React.PropTypes.object.isRequired
    };

    _authService: IAuthService;

    componentDidMount():void {
        this._authService = this.context.kernel.resolve<IAuthService>("IAuthService");

        var that = this;
        this._authService.logout().then(function() {
            that.context.store.dispatch(createLogoutAction());
        });
    }

    render(): JSX.Element {
        return (
            <p>You are now logged out</p>
        );
    }
}

