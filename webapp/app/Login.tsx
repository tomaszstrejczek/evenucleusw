﻿import * as React from 'react';
import {Link} from 'react-router';

import {IAuthServiceContext} from './../api/AuthService';
import {IStoreContext} from './IStoreContext';
import {IRouterContext} from './IRouterContext';
import {createLoginAction} from './../actions/LoginActions';
import {createNotificationShowAction, NotificationType} from './../actions/NotificationActions';
import {IApiContext} from './IApiContext';



var Input = require('./../forms/input');
var LinkedStateMixin  = require('react/lib/LinkedStateMixin');

require('app/Login.css');

import Formsy = require('formsy-react');

class FormsyBase<P extends Formsy.FormsyBaseProps, S extends Formsy.FormsyBaseState> extends React.Component<P, S> {
    _implementation = Formsy.Mixin;

    constructor() {
        super();
        this.state = this._implementation.getInitialState() as S;
        this.props = this._implementation.getDefaultProps() as P;
    }

    componentWillMount(): void {
        this._implementation.componentWillMount();
    }
    componentWillReceiveProps(nextProps: any): void {
        this._implementation.componentWillReceiveProps(nextProps);
    }
    componentDidUpdate(): void {
        this._implementation.componentDidUpdate();
    }
    componentWillUnmount(): void {
        this._implementation.componentWillUnmount();
    }
    setValidations(validations: any, required: boolean): void {
        this._implementation.setValidations(validations, required);
    }
    setValue(value: any): void {
        this._implementation.setValue(value);
    }
    resetValue(): void {
        this._implementation.resetValue();
    }
    getValue(): any {
        return this._implementation.getValue();
    }
    hasValue(): boolean {
        return this._implementation.hasValue();
    }
    getErrorMessage(): any {
        return this._implementation.getErrorMessage
    }
    isFormDisabled(): boolean {
        return this._implementation.isFormDisabled();
    }
    isValid(): boolean {
        return this._implementation.isValid();
    }
    isPristine(): boolean {
        return this._implementation.isPristine();
    }
    isFormSubmitted(): boolean {
        return this._implementation.isFormSubmitted();
    }
    isRequired(): boolean {
        return this._implementation.isRequired();
    }
    showRequired(): boolean {
        return this._implementation.showRequired();
    }
    showError(): boolean {
        return this._implementation.showError();
    }
    isValidValue(): boolean {
        return this._implementation.isValidValue();
    }
};

interface MyInputProps extends React.Props<MyInput>, Formsy.FormsyBaseProps {
    type: string;
    placeholder: string;
    name: string;
    layout: string;
    required: boolean;
    autofocus?: boolean;
    validations?: string;
    value?: any;
}

class MyInput extends FormsyBase<MyInputProps, Formsy.FormsyBaseState> {
    // setValue() will set the value of the component, which in 
    // turn will validate it and the rest of the form
    changeValue(event: React.FormEvent) {
        this.setValue((event.currentTarget as HTMLInputElement).value);
    };

    render(): JSX.Element {

        // Set a specific className based on the validation
        // state of this component. showRequired() is true 
        // when the value is empty and the required prop is 
        // passed to the input. showError() is true when the 
        // value typed is invalid
        var className = this.showRequired() ? 'form-group required' : this.showError() ? 'form-group has-error' : 'form-group has-success';

        // An error message is returned ONLY if the component is invalid
        // or the server has returned an error message
        var errorMessage = this.getErrorMessage();

        return (
            <div className={className}>
                <Input type={this.props.type} className="form-control" placeholder={this.props.placeholder} onChange={this.changeValue} value={this.getValue()}/>
                <span>{errorMessage}</span>
            </div>
        );
    }

}


class LoginState {
    formError: string;
    canSubmit: boolean;

    constructor() {
        this.formError = '';
        this.canSubmit = false;
    }
}

class LoginModel {
    email: string;
    password: string;
}

export class Login extends React.Component<any, LoginState> {
    static mixins = [LinkedStateMixin];

    constructor() {
        super();
        this.state = new LoginState();
    }

    context: IStoreContext & IRouterContext & IAuthServiceContext & IApiContext;

    static contextTypes: React.ValidationMap<any> = {
        history: React.PropTypes.object.isRequired,
        store: React.PropTypes.object.isRequired,
        authService: React.PropTypes.object.isRequired,
        api: React.PropTypes.object.isRequired
    };

    enableButton() {
        this.setState(
            (prevState: LoginState, props: any): LoginState => {
                prevState.canSubmit = true;
                return prevState;
            })
    }

    disableButton() {
        this.setState(
            (prevState: LoginState, props: any): LoginState => {
                prevState.canSubmit = false;
                return prevState;
            })
    }

    submit(model: LoginModel): When.Promise<void> {
        var that = this;
        return this.context.authService.login(model.email, model.password)
            .then(function (jwt: string) {
                that.context.store.dispatch(createLoginAction(that.context.api, jwt, model.email));
                var nextPath = '/';
                var location = that.props.location as any;
                if (location && location.state && location.state.nextPathname)
                    nextPath = location.state.nextPathname;
                that.context.history.pushState(nextPath, nextPath);                    
            })
            .catch(function (err: ts.dto.Error) {
                console.log("Error logging in ", err);
                that.context.store.dispatch(createNotificationShowAction(NotificationType.error, "error", err.errorMessage));
                that.setState((prevState: LoginState, props: any): LoginState => {
                    prevState.formError = err.errorMessage;
                    return prevState;
                });
            });
    }

    render(): JSX.Element {
        var that = this;
        var submitProxy = function (model: LoginModel) {
            that.submit(model);
        };

        return (
            <div style={{display: "flex", justifyContent:"center"}}>
                <div>
                    <h1 className="text-center login-title">Sign in to continue</h1>
                    <div className="account-wall">
                        <Formsy.Form className="form-signin" onValidSubmit={submitProxy.bind(this)} onValid={this.enableButton.bind(this)} onInvalid={this.disableButton.bind(this)}>
                            <span className="help-block" ref="helpblock">{this.state.formError}</span>
                            <Input name="email" type="text" validations="isEmail" placeholder="Email" required autofocus layout="elementOnly" validationError="This is not a valid email" ref="email"></Input>
                            <Input name="password" type="password" placeholder="Password" required layout="elementOnly" ref="password" ></Input>
                            <button className="btn btn-lg btn-primary btn-block" type="submit" disabled={!this.state.canSubmit} ref="button">
                                Sign in</button>
                            <label className="checkbox pull-left">
                                <input type="checkbox" value="remember-me"></input>
                                Remember me
                            </label>
                        </Formsy.Form>
                    </div>
                    <Link to="/register" className="text-center new-account">Create an account </Link>
                </div>
            </div>
        );
    }

}


