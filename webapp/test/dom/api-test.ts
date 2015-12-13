/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect, assert} from 'chai';
import {IAuthServiceContext, IAuthService, AuthService} from './../../api/AuthService';
import {IApiCaller} from './../../api/IApiCaller';
import {ApiCaller} from './../../api/ApiCaller';
import {IKeyInfoService, KeyInfoService} from './../../api/KeyInfoService';

import {Helper} from './Helper';

import * as Sinon from 'sinon';
import * as When from 'when';


//const TestUtils = ReactAddons.addons.TestUtils;

export class Runner {
    static run(): void {
        describe('AuthService login', function () {
            let api: IApiCaller;
            let authService: IAuthService;
            let keyInfoService: IKeyInfoService;

            beforeEach(function() {
                api = new ApiCaller();
                authService = new AuthService(api);
                keyInfoService = new KeyInfoService(api);
            });

            afterEach(function() {
            });

            it('login wrong password', function () {
                var r = authService.login("wrong user", "wron password")
                    .catch((reason: ts.dto.Error) => {
                        assert.equal(reason.errorMessage, "Invalid user/password");
                    });
                return r;
            });

            it('create user', function() {
                var helper = new Helper();
                return helper.createTestUser();
            });

            it('logout user', function () {
                var helper = new Helper();
                return helper.createTestUser().then((skey: string) => {
                    api.setKey(skey);
                    return authService.logout();
                })
                .then(() => {
                    var r = keyInfoService.GetAll();
                        r.then((data: ts.dto.KeyInfoDto[]) => {
                            assert.fail("Unreachable");
                        });
                    return r;
                })
                .catch((reason: ts.dto.Error) => {
                    assert.equal(reason.errorMessage, "Invalid or expired session key");
                });
            });

            it('logon user', function () {
                var helper = new Helper();
                return helper.createTestUser().then((skey: string) => {
                        api.setKey(skey);
                        return authService.logout();
                    })
                    .then(() => {
                        var r = authService.login(helper._email, helper._password);
                        return r;
                    });
            });

        });
    }
}


 