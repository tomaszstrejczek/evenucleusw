/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect} from 'chai';
import {Login} from './../../app/Login';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as ReactAddons from 'react/addons';
import {IAuthServiceContext, IAuthService, AuthService} from './../../api/AuthService';
import {AppState} from './../../app/AppState';
import {Store, createStore} from 'redux';
import {rootReducer} from './../../actions/rootReducer';
import {IStoreContext} from './../../app/IStoreContext';
import * as Sinon from 'sinon';

//const TestUtils = ReactAddons.addons.TestUtils;

interface ContainerProps {
    store: Store;
    authService: IAuthService;
}

class Container extends React.Component<ContainerProps, any> implements React.ChildContextProvider<IAuthServiceContext & IStoreContext> {

    static childContextTypes: React.ValidationMap<any> = {
        authService: React.PropTypes.object,
        store: React.PropTypes.object
    };

    getChildContext(): IAuthServiceContext&IStoreContext {
        return {
            authService: this.props.authService,
            store: this.props.store
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
            let login: ReactAddons.Component<any, any>;
            let emailDiv: HTMLDivElement;
            let email: HTMLInputElement;
            let passwordDiv: HTMLDivElement;
            let password: HTMLInputElement;
            let button: HTMLButtonElement;
            
            beforeEach(function () {
                const store: Store = createStore(rootReducer, new AppState());
                var div = document.createElement('div');
                var authService = Sinon.stub(new AuthService()).returns("ala") as any as IAuthService;

                var component = ReactDOM.render(
                    <Container store={store} authService={authService}/>
                    , div);

                login = ReactAddons.addons.TestUtils.findRenderedComponentWithType(component, Login);

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
                //React.unmountComponentAtNode(div);
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
                ReactAddons.addons.TestUtils.Simulate.change(email);
                expect(button.disabled).to.be.true;
                var msg = emailDiv.getElementsByClassName("validation-message")[0];
                expect(msg).not.to.be.null;
                expect(msg.firstChild.nodeValue).to.be.equal("This is not a valid email");

                // Check valid email
                email.value = "ala@a.a";
                ReactAddons.addons.TestUtils.Simulate.change(email);
                var elems = emailDiv.getElementsByClassName("validation-message");
                expect(elems.length).to.be.equal(0);
            });

            it('password validation', function () {
                expect(button.disabled).to.be.true;

                // Check password
                password.value = "ala";
                ReactAddons.addons.TestUtils.Simulate.change(password);
                expect(button.disabled).to.be.true;
            });

            it('form validation', function () {
                expect(button.disabled).to.be.true;

                email.value = "ala@a.a";
                ReactAddons.addons.TestUtils.Simulate.change(email);
                password.value = "123";
                ReactAddons.addons.TestUtils.Simulate.change(password);

                expect(button.disabled).to.be.false;
            });


        });
    }
}


 