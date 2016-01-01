import * as React from 'react';
import {Navigation} from './Navigation';
import ReactRouter = require('react-router');
import {KernelCreator} from './kernel';
import * as TypeIoc from 'typeioc';
import {IKernelContext} from './IKernelContext';
import {IAuthServiceContext, IAuthService} from './../api/AuthService';
import {IApiCaller} from './../api/IApiCaller';
import {IApiContext} from './IApiContext';
import * as RactNotifications from 'react-notifications';
import {IStoreContext} from './IStoreContext';
import {IAppState} from './AppState';
import {createNotificationHideAction} from './../actions/NotificationActions';

import { bindActionCreators } from 'redux';

require('./../styles/notifications.css');

var nsm = require('react-notifications');

var kernelSingleton = KernelCreator.create();

class AppComponentState {
    constructor(notifications:ReactNotifications.Notification[]) {
        this.notifications = notifications;
    }
    notifications: ReactNotifications.Notification[];
}


export class App extends React.Component<any, AppComponentState> implements React.ChildContextProvider<IAuthServiceContext> {

    context: IStoreContext;
    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired
    };

    static childContextTypes: React.ValidationMap<any> = {
        authService: React.PropTypes.object,
        api: React.PropTypes.object
    };
    private changeListener: () => void;

    constructor(props, context) {
        super(props, context);
        var s: IAppState = this.context.store.getState() as IAppState;
        this.state = new AppComponentState(s.notifications);
    }

    getChildContext(): IAuthServiceContext & IApiContext{
        return {
            authService: kernelSingleton.resolve<IAuthService>("IAuthService"),
            api: kernelSingleton.resolve<IApiCaller>("IApiCaller")
        };
    };


    componentDidMount() {
        this.changeListener = this._onChange.bind(this);
        this.context.store.subscribe(this.changeListener);
    }

    _onChange() {
        var s: IAppState = this.context.store.getState() as IAppState;
        this.setState(s);
    }

    handleRequestHide(notification: ReactNotifications.Notification) {
        this.context.store.dispatch(createNotificationHideAction(notification.id));
    };

    render(): JSX.Element {
        const { state, dispatch } = this.props;
        var Notifications = nsm as any as new() => ReactNotifications.INotifications;

        return (
            <div>
                <Navigation/>
                <div className="container-fluid">
                {this.props.children}
                    </div>
                <Notifications notifications={this.state.notifications} onRequestHide={this.handleRequestHide.bind(this)}/>
            </div>
        );
    }
}