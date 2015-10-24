/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {assert} from 'chai';

export class Runner {
    static run(): void {
        describe('Array', function () {
            describe('#indexOf()', function () {
                it('should return -1 when the value is not present', function () {
                    assert.equal(-1, [1, 2, 3].indexOf(5));
                    assert.equal(-1, [1, 2, 3].indexOf(0));
                });
            });
        });
    }
}

