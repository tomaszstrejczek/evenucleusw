/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect} from 'chai';
import {Login} from './../../app/Login';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as ReactAddons from 'react-addons-test-utils';
import {IAuthServiceContext, IAuthService, AuthService} from './../../api/AuthService';
import {LoginInfo} from './../../app/AppState';
import {Store, createStore, applyMiddleware} from 'redux';
import {rootReducer} from './../../actions/rootReducer';
import {IStoreContext} from './../../app/IStoreContext';
import {IRouterContext} from './../../app/IRouterContext';
import {IApiContext} from './../../app/IApiContext';
import {IApiCaller} from './../../api/IApiCaller';
import {ApiCaller} from './../../api/ApiCaller';
import reduxThunk = require("redux-thunk");

import * as Sinon from 'sinon';
import * as When from 'when';

//const TestUtils = ReactAddons.addons.TestUtils;

interface ContainerProps {
    store: Store;
    authService: IAuthService;
    history: any;
    api: IApiCaller;
}

class Container extends React.Component<ContainerProps, any> implements React.ChildContextProvider<IAuthServiceContext & IStoreContext & IRouterContext > {

    static childContextTypes: React.ValidationMap<any> = {
        authService: React.PropTypes.object,
        store: React.PropTypes.object,
        history: React.PropTypes.object,
        api: React.PropTypes.object,
    };

    getChildContext(): IAuthServiceContext & IStoreContext & IRouterContext & IApiContext{
        return {
            authService: this.props.authService,
            store: this.props.store,
            history: this.props.history,
            api: this.props.api
        };
    };

    render(): JSX.Element {
        return (
            <Login/>
        );
    }
}

export class Runner {
    static run(): void {
        describe('Login', function () {
            let login: React.Component<any, any>;
            let emailDiv: HTMLDivElement;
            let email: HTMLInputElement;
            let passwordDiv: HTMLDivElement;
            let password: HTMLInputElement;
            let button: HTMLButtonElement;
            let loginStub: Sinon.SinonStub;
            let pushStateStub: Sinon.SinonStub;
            
            beforeEach(function () {
                var initialState = {
                    loginInfo: new LoginInfo()
                }
                const store: Store = applyMiddleware(reduxThunk as any)(createStore)(rootReducer, initialState);
                var div = document.createElement('div');
                var api = new ApiCaller();
                var authService = new AuthService(api);
                loginStub = Sinon.stub(authService, "login");
                var router = { };
                (router as any).pushState = (adr: string) => { };
                (router as any).createHref = (to: string): string => to;
                (router as any).isActive = (): boolean => true;
                
                pushStateStub = Sinon.stub(router, "pushState");

                var component = ReactDOM.render(
                    <Container store={store} authService={authService} history={router} api={api}/>
                    , div);

                login = ReactAddons.findRenderedComponentWithType(component, Login);

                var node = ReactDOM.findDOMNode(login);
                expect(node).not.to.be.null;

                emailDiv = ReactDOM.findDOMNode<HTMLDivElement>(login.refs["email"]);
                expect(emailDiv).not.to.be.null;

                email = emailDiv.getElementsByTagName("input")[0];
                expect(email).not.to.be.null;

                button = ReactDOM.findDOMNode<HTMLButtonElement>(login.refs["button"]);
                expect(button).not.to.be.null;              

                passwordDiv = ReactDOM.findDOMNode<HTMLDivElement>(login.refs["password"]);
                expect(passwordDiv).not.to.be.null;

                password = passwordDiv.getElementsByTagName("input")[0];
                expect(password).not.to.be.null;
            })

            afterEach(function () {
                //var div = document.body.getElementsByTagName("div")[0];
                //ReactDOM.unmountComponentAtNode(div);
                //document.body.innerHTML = "";
            })

            it('sample test', function () {
                expect(() => {
                    throw 'ala';
                }).to.throw('ala');
            });

            it('email validation', function () {
                expect(button.disabled).to.be.true;

                // Check if invalid email
                email.value = "ala";
                ReactAddons.Simulate.change(email);
                expect(button.disabled).to.be.true;
                var msg = emailDiv.getElementsByClassName("validation-message")[0];
                expect(msg).not.to.be.null;
                expect(msg.firstChild.nodeValue).to.be.equal("This is not a valid email");

                // Check valid email
                email.value = "ala@a.a";
                ReactAddons.Simulate.change(email);
                var elems = emailDiv.getElementsByClassName("validation-message");
                expect(elems.length).to.be.equal(0);
            });

            it('password validation', function () {
                expect(button.disabled).to.be.true;

                // Check password
                password.value = "ala";
                ReactAddons.Simulate.change(password);
                expect(button.disabled).to.be.true;
            });

            it('form validation', function () {
                expect(button.disabled).to.be.true;

                email.value = "ala@a.a";
                ReactAddons.Simulate.change(email);
                password.value = "123";
                ReactAddons.Simulate.change(password);

                expect(button.disabled).to.be.false;
            });

            it('authorize', function () {
                email.value = "ala@a.a";
                ReactAddons.Simulate.change(email);
                password.value = "123";
                ReactAddons.Simulate.change(password);

                expect(button.disabled).to.be.false;

                loginStub.returns(When.resolve('ala'));

                var submitSpy = Sinon.spy(login, "submit");

                ReactAddons.Simulate.submit(button);

                expect(submitSpy.called).to.be.true;
                var submitResult = submitSpy.returnValues[0] as When.Promise<void>;
                return submitResult
                    .then(() => {
                        expect(loginStub.calledOnce).to.be.true;
                        expect(loginStub.calledWith("ala@a.a", "123")).to.be.true;

                        expect(pushStateStub.calledOnce).to.be.true;
                        expect(pushStateStub.calledWith("/")).to.be.true;
                    });
            });

            it('invalid authorization', function () {
                email.value = "ala@a.a";
                ReactAddons.Simulate.change(email);
                password.value = "123";
                ReactAddons.Simulate.change(password);

                expect(button.disabled).to.be.false;

                loginStub.returns(When.promise(() => { throw { errorMessage: "Invalid ala" }; }));

                var submitSpy = Sinon.spy(login, "submit");

                ReactAddons.Simulate.submit(button);

                expect(submitSpy.called).to.be.true;
                var submitResult = submitSpy.returnValues[0] as When.Promise<void>;
                return submitResult
                    .then(() => {
                        expect(loginStub.calledOnce).to.be.true;
                        expect(loginStub.calledWith("ala@a.a", "123")).to.be.true;

                        var helpBlock = button = ReactDOM.findDOMNode<HTMLButtonElement>(login.refs["helpblock"]);
                        expect(helpBlock.textContent).to.be.eq("Invalid ala");

                        expect(pushStateStub.calledOnce).to.be.false;

                    });
            });

        });
    }
}


 