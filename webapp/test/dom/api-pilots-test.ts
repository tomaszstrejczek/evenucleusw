/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect, assert} from 'chai';
import {IAuthServiceContext, IAuthService, AuthService} from './../../api/AuthService';
import {IApiCaller} from './../../api/IApiCaller';
import {ApiCaller} from './../../api/ApiCaller';
import {IKeyInfoService, KeyInfoService} from './../../api/KeyInfoService';
import {IBackgroundUpdateService, BackgroundUpdateService} from './../../api/BackgroundUpdateService';
import {IPilotsService, PilotsService} from './../../api/PilotsService';

import {Helper} from './Helper';

import * as Sinon from 'sinon';
import * as When from 'when';


//const TestUtils = ReactAddons.addons.TestUtils;

export class Runner {
    static run(): void {
        describe('Pilots and BackgroundUpdate api', function () {
            let api: IApiCaller;
            let keyInfoService: IKeyInfoService;
            let pilotsService: IPilotsService;
            let backgroundUpdateService: IBackgroundUpdateService;
            let helper: Helper;
            let skey: string;

            beforeEach(function() {
                api = new ApiCaller();
                keyInfoService = new KeyInfoService(api);
                pilotsService = new PilotsService(api);
                backgroundUpdateService = new BackgroundUpdateService(api);
                helper = new Helper();
                return helper.createTestUser().then((s: string) => { skey = s; api.setKey(s); });
            });

            afterEach(function() {
            });

            it('no key defined', function () {
                var r = pilotsService.GetAll().then((data: ts.dto.PilotDto[]) => {
                    assert.equal(data.length, 0);
                });
                return r;
            });

            it('add valid key', function () {
                var code = 3645238;
                var vcode = "sLOD3pSHwuzKtml3inm59qvVWHiKA3rULJY7KRsuWmmHrZ0c8qAZlftLDQIHvxBq";

                var r = keyInfoService.AddKey(code, vcode).then((data: number) => {
                    assert.notEqual(data, 0);
                })
                .then(() => {
                    return pilotsService.GetAll();
                })
                .then((data: ts.dto.PilotDto[]) => {
                    assert.equal(data.length, 1);
                    assert.equal(data[0].name, "MicioGatto");

                    // simple update - no skills retrieved
                    assert.isTrue(data[0].skills.length === 0);
                    assert.isTrue(data[0].skillsInQueue.length === 0);
                });
                return r;
            });

            it('add valid key and background update', function () {
                var code = 3645238;
                var vcode = "sLOD3pSHwuzKtml3inm59qvVWHiKA3rULJY7KRsuWmmHrZ0c8qAZlftLDQIHvxBq";

                var r = keyInfoService.AddKey(code, vcode).then((data: number) => {
                    assert.notEqual(data, 0);
                })
                    .then(() => {
                        return backgroundUpdateService.Update();
                    })
                    .then(() => {
                        return pilotsService.GetAll();
                    })
                    .then((data: ts.dto.PilotDto[]) => {
                        assert.equal(data.length, 1);
                        assert.equal(data[0].name, "MicioGatto");

                        // simple update - no skills retrieved
                        assert.isTrue(data[0].skills.length !== 0);
                        assert.isTrue(data[0].skillsInQueue.length !== 0);
                        assert.isTrue(data[0].skills.filter(s => s.skillName === "Interceptors").length > 0);
                    });
                return r;
            });
        });
    }
}


 