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
        describe('KeyInfo api', function () {
            let api: IApiCaller;
            let keyInfoService: IKeyInfoService;
            let helper: Helper;
            let skey: string;

            beforeEach(function() {
                api = new ApiCaller();
                keyInfoService = new KeyInfoService(api);
                helper = new Helper();
                return helper.createTestUser().then((s: string) => { skey = s; api.setKey(s); });
            });

            afterEach(function() {
            });

            it('no key defined', function () {
                var r = keyInfoService.GetAll().then((data: ts.dto.KeyInfoDto[]) => {
                    assert.equal(data.length, 0);
                });
                return r;
            });

            it('add valid key', function () {
                var code = 3483492;
                var vcode = "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG";

                var r = keyInfoService.AddKey(code, vcode).then((data: number) => {
                    assert.notEqual(data, 0);
                })
                .then(() => {
                    return keyInfoService.GetAll();
                })
                .then((data: ts.dto.KeyInfoDto[]) => {
                        assert.equal(data.length, 1);
                        assert.equal(data[0].pilots.length, 1);
                        assert.equal(data[0].pilots[0].name, "MicioGatto");
                });
                return r;
            });

            it('add invalid key', function () {
                var code = 1;
                var vcode = "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG";

                var r = keyInfoService.AddKey(code, vcode)
                    .then((data: number) => {
                        assert.fail("Unreachable");
                    })
                    .catch((reason: ts.dto.Error) => {
                        assert.equal(reason.errorMessage, "Authentication failure. Invalid key and/or verification code");
                    });
                return r;
            });

            it('add duplicate key', function () {
                var code = 3483492;
                var vcode = "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG";

                var r = keyInfoService.AddKey(code, vcode)
                    .then((data: number) => {
                        return keyInfoService.AddKey(code, vcode);
                    })
                    .catch((reason: ts.dto.Error) => {
                        assert.equal(reason.errorMessage, "Provided key is already defined");
                    });
                return r;
            });
        });
    }
}


 