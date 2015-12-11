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

            it('login wrong password', function () {
                var r = authService.login("wrong user", "wron password")
                    .catch((reason: ts.dto.Error) => {
                        assert.equal(reason.errorMessage, "Invalid user/password");
                    });
                return r;
                    //.then((skey: string) => {
                    //    assert.isFalse(skey);
                    //    //assert.equal(skey.error.errorMessage, "Invalid user/password");
                    //    done();
                    //});
            });
        });
    }
}


 