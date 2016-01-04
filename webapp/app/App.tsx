import * as React from 'react';
import {Navigation} from './Navigation';
import ReactRouter = require('react-router');
import {KernelCreator} from './kernel';
import * as TypeIoc from 'typeioc';
import {IKernelContext} from './IKernelContext';
import {IAuthServiceContext, IAuthService} from './../api/AuthService';
import {IKeyInfoServiceContext, IKeyInfoService} from './../api/KeyInfoService';
import {IApiCaller} from './../api/IApiCaller';
import {IApiContext} from './IApiContext';
import * as RactNotifications from 'react-notifications';
import {IStoreContext} from './IStoreContext';
import {IAppState, ConfirmInfo} from './AppState';
import {createNotificationHideAction} from './../actions/NotificationActions';
import {createConfirmShowAction, createConfirmConfirmAction} from './../actions/ConfirmActions';
import {IDeferredActionExecutor, IDeferredActionExecutorContext} from './../utils/DeferredActionExecutor';
import {IPilotsService, IPilotsServiceContext} from './../api/PilotsService';
import {IBackgroundUpdateService, IBackgroundUpdateServiceContext} from './../api/BackgroundUpdateService';

import { bindActionCreators } from 'redux';

require('./../styles/notifications.css');

var nsm = require('react-notifications');
var modal= require('react-modal');

import update = require('react-addons-update');

var kernelSingleton = KernelCreator.create();

class AppComponentState {
    constructor(notifications:ReactNotifications.Notification[]) {
        this.notifications = notifications;
        this.confirmInfo = { visible: false, key:"", title: "", description:"", okButton:"", confirmed: false };
    }
    notifications: ReactNotifications.Notification[];
    confirmInfo: ConfirmInfo;
}


export class App extends React.Component<any, AppComponentState> implements React.ChildContextProvider<IAuthServiceContext & IApiContext
    & IKeyInfoServiceContext & IDeferredActionExecutorContext & IPilotsServiceContext & IBackgroundUpdateServiceContext> {

    context: IStoreContext;
    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired
    };

    static childContextTypes: React.ValidationMap<any> = {
        authService: React.PropTypes.object,
        keyInfoService: React.PropTypes.object,
        api: React.PropTypes.object,
        deferredActionExecutor: React.PropTypes.object,
        pilotsService: React.PropTypes.object.isRequired,
        backgroundUpdateService: React.PropTypes.object.isRequired
    };
    private changeListener: () => void;
    private unsubscribe: Function;

    constructor(props, context) {
        super(props, context);
        var s: IAppState = this.context.store.getState() as IAppState;
        this.state = new AppComponentState(s.notifications);
    }

    getChildContext(): IAuthServiceContext & IApiContext & IKeyInfoServiceContext & IDeferredActionExecutorContext & IPilotsServiceContext & IBackgroundUpdateServiceContext{
        return {
            authService: kernelSingleton.resolve<IAuthService>("IAuthService"),
            api: kernelSingleton.resolve<IApiCaller>("IApiCaller"),
            keyInfoService: kernelSingleton.resolve<IKeyInfoService>("IKeyInfoService"),
            deferredActionExecutor: kernelSingleton.resolve<IDeferredActionExecutor>("IDeferredActionExecutor"),
            pilotsService: kernelSingleton.resolve<IPilotsService>("IPilotsService"),
            backgroundUpdateService: kernelSingleton.resolve<IBackgroundUpdateService>("IBackgroundUpdateService"),
        };
    };


    componentDidMount() {
        this.changeListener = this._onChange.bind(this);
        this.unsubscribe = this.context.store.subscribe(this.changeListener);
    }

    componentWillUnmount() {
        this.unsubscribe();
    }

    _onChange() {
        var s: IAppState = this.context.store.getState() as IAppState;
        //// merge notifications
        //var currentNotificationIds = this.state.notifications.map(elem => elem.id);
        //var toadd = s.notifications.filter(elem => currentNotificationIds.indexOf(elem.id) === -1);

        //var newIds = s.notifications.map(elem => elem.id);
        //var toremove = this.state.notifications.filter(elem => newIds.indexOf(elem.id) === -1);

        //var currentNotifications = update(this.state.notifications, { $push: toadd, $shift: toremove });
        
        //// merge confirmInfo
        //var ci = update(this.state.confirmInfo, { $merge: s.confirmInfo });

        this.setState({ notifications: s.notifications, confirmInfo: s.confirmInfo});
    }

    handleRequestHide(notification: ReactNotifications.Notification) {
        this.context.store.dispatch(createNotificationHideAction(notification.id));
    };

    handleModalCloseRequest() {
        var deferredActionExecutor = kernelSingleton.resolve<IDeferredActionExecutor>("IDeferredActionExecutor");
        deferredActionExecutor.RemoveAction(this.state.confirmInfo.key);
        this.context.store.dispatch(createConfirmShowAction(false, "", "", ""));
    };

    handleSaveClicked(e: any) {
        this.context.store.dispatch(createConfirmConfirmAction());
        var deferredActionExecutor = kernelSingleton.resolve<IDeferredActionExecutor>("IDeferredActionExecutor");
        deferredActionExecutor.RunAction(this.state.confirmInfo.key);
    };

    render(): JSX.Element {
        const { state, dispatch } = this.props;
        var Notifications = nsm as any as new() => ReactNotifications.INotifications;
        var Modal = modal as any as new () => ReactModal.IReactModal;
        const customStyles = {
            content: {
                top: '50%',
                left: '50%',
                right: 'auto',
                bottom: 'auto',
                marginRight: '-50%',
                transform: 'translate(-50%, -50%)'
            }
        };

        return (
            <div>
                <Navigation/>
                <div className="container-fluid">
                {this.props.children}
                    </div>
                <Notifications notifications={this.state.notifications} onRequestHide={this.handleRequestHide.bind(this) }/>
                <Modal
                    className="modal-dialog"
                    closeTimeoutMS={150}
                    isOpen={this.state.confirmInfo.visible}
                    style={customStyles}
                    >
                    <div className="modal-content">
                        <div className="modal-header">
                            <button type="button" className="close" onClick={this.handleModalCloseRequest.bind(this)}>
                            <span aria-hidden="true">&times; </span>
                            <span className="sr-only">Close</span>
                                </button>
                            <h4 className="modal-title">{this.state.confirmInfo.title}</h4>
                        </div>
                        <div className="modal-body">
                            <p>{this.state.confirmInfo.description}</p>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-default" onClick={this.handleModalCloseRequest.bind(this)}>Close</button>
                            <button type="button" className="btn btn-primary" onClick={this.handleSaveClicked.bind(this)}>{this.state.confirmInfo.okButton}</button>
                        </div>
                    </div>
                </Modal>
            </div>
        );
    }
}