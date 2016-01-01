import * as React from 'react';

import {IKeyInfoServiceContext} from './../api/KeyInfoService';
import {IStoreContext} from './../app/IStoreContext';
import {IRouterContext} from './../app/IRouterContext';
import {createKeyAddAction} from './../actions/KeyActions';
import {createNotificationShowAction, NotificationType} from './../actions/NotificationActions';


var Input = require('./../forms/input');
var LinkedStateMixin  = require('react/lib/LinkedStateMixin');

require('./../app/Login.css');

import Formsy = require('formsy-react');


class KeyAddState {
    formError: string;
    canSubmit: boolean;

    constructor() {
        this.formError = '';
        this.canSubmit = false;
    }
}

class KeyAddModel {
    keyid: number;
    vcode: string;
}

export class KeyAdd extends React.Component<any, KeyAddState> {
    static mixins = [LinkedStateMixin];

    constructor() {
        super();
        this.state = new KeyAddState();
    }

    context: IStoreContext & IRouterContext & IKeyInfoServiceContext;

    static contextTypes: React.ValidationMap<any> = {
        history: React.PropTypes.object.isRequired,
        store: React.PropTypes.object.isRequired,
        keyInfoService: React.PropTypes.object.isRequired
    };

    enableButton() {
        this.setState(
            (prevState: KeyAddState, props: any): KeyAddState => {
                prevState.canSubmit = true;
                return prevState;
            })
    }

    disableButton() {
        this.setState(
            (prevState: KeyAddState, props: any): KeyAddState => {
                prevState.canSubmit = false;
                return prevState;
            })
    }

    submit(model: KeyAddModel): When.Promise<void> {
        var that = this;
        return this.context.keyInfoService.AddKey(model.keyid, model.vcode)
            .then(function (keyinfoid: number) {
                that.context.store.dispatch(createKeyAddAction(keyinfoid, model.keyid, model.vcode));
                that.context.history.pushState('/keys', '/keys');
            })
            .catch(function(err) {
                console.log("Error logging in", err);
                that.context.store.dispatch(createNotificationShowAction(NotificationType.error, "error", err.errorMessage));
                that.setState((prevState: KeyAddState, props: any): KeyAddState => {
                    prevState.formError = err.errorMessage;
                    return prevState;
                });
            });
    }

    render(): JSX.Element {
        var that = this;
        var submitProxy = function (model: KeyAddModel) {
            that.submit(model);
        };

        return (
            <div className="row">
                <div className="col-sm-6 col-md-4 col-md-offset-4">
                    <h1 className="text-center login-title">Add a key (use <a href="http://community.eveonline.com/support/api-key/CreatePredefined?accessMask=17236104">this</a> link)</h1>
                    <div className="account-wall">
                        <Formsy.Form className="form-signin" onValidSubmit={submitProxy.bind(this)} onValid={this.enableButton.bind(this)} onInvalid={this.disableButton.bind(this)}>
                            <span className="help-block" ref="helpblock">{this.state.formError}</span>
                            <Input name="keyid" type="text" validations="isNumeric" placeholder="key id" required autofocus layout="elementOnly" validationError="This is not a valid key id" ref="keyid"></Input>
                            <Input name="vcode" type="text" placeholder="vcode" required autofocus layout="elementOnly" validationError="This is not a valid vcode" ref="vcode"></Input>
                            <button className="btn btn-lg btn-primary btn-block" type="submit" disabled={!this.state.canSubmit} ref="button">
                                Add</button>
                        </Formsy.Form>
                    </div>                    
                </div>
            </div>
        );
    }

}


