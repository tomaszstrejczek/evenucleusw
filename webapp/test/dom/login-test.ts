/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {expect} from 'chai';
//import ReactAddons from 'react/addons';

//const TestUtils = ReactAddons.addons.TestUtils;

export class Runner {
    static run(): void {
        describe('Login', function () {
            let login;

            beforeEach(function () {
            })

            it('empty', function () {
                expect(() => {
                    throw 'ala';
                }).to.throw('ala');
            });
        });
    }
}


 