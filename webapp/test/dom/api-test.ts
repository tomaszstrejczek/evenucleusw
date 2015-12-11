/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect, assert} from 'chai';
import {IAuthServiceContext, IAuthService, AuthService} from './../../api/AuthService';
import * as Restful from 'restful.js';


import * as Sinon from 'sinon';
import * as When from 'when';

//const TestUtils = ReactAddons.addons.TestUtils;

export class Runner {
    static run(): void {
        describe('AuthService login', function () {
            let api: Restful.Api;
            let authService: IAuthService;
            
            beforeEach(function () {
                api = Restful.default("http://localhost:8080");
                authService = new AuthService(api);
            })

            afterEach(function () {
            })

            it('login wrong password', function (done: MochaDone) {
                authService.login("wrong user", "wron password")
                    .then((skey: string) => {
                        done();
                    })
                    .catch((err: any) => {
                        var error = err as ts.dto.Error;
                        if (error.errorMessage !== "Invalid user/password")
                            done(err);
                        else
                            done();
                    })
            });
        });
    }
}


 