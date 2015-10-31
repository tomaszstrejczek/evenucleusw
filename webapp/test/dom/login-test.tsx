/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect} from 'chai';
import {Login} from './../../app/Login';
import * as ReactAddons from 'react/addons';
import * as React from 'react';
import {TestContext} from './TestContext';

//import ReactAddons from 'react/addons';

//const TestUtils = ReactAddons.addons.TestUtils;

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
                login = TestContext.getRouterComponent(Login);
                var node = React.findDOMNode(login);
                expect(node).not.to.be.null;

                emailDiv = React.findDOMNode<HTMLDivElement>(login.refs["email"]);
                expect(emailDiv).not.to.be.null;

                email = emailDiv.getElementsByTagName("input")[0];
                expect(email).not.to.be.null;

                button = React.findDOMNode<HTMLButtonElement>(login.refs["button"]);
                expect(button).not.to.be.null;              

                passwordDiv = React.findDOMNode<HTMLDivElement>(login.refs["password"]);
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


 