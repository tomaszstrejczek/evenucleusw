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
            let login;
            
            beforeEach(function () {
                var login = TestContext.getRouterComponent(Login);
            })

            it('empty', function () {
                expect(() => {
                    throw 'ala';
                }).to.throw('ala');
            });
        });
    }
}


 