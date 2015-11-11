import * as React from 'react';
import {AuthService} from './../api/AuthService';

export class Logout extends React.Component<any, any> {
    componentDidMount():void {
        AuthService.logout();
    }

    render(): JSX.Element {
        return (
            <p>You are now logged out</p>
        );
    }
}

