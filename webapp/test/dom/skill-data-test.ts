/// <reference path="./../../typings/mocha/mocha.d.ts" />

import {assert} from 'chai';

import {ApiCaller} from './../../api/ApiCaller';
import {IApiCaller} from './../../api/IApiCaller';
import {ISkillGrouping} from './../../characters/SkillData';
import {frigates, destroyers, cruisers, battlecruisers, battleships, transport} from './../../characters/SkillData';
import {carriers, freighters, dreads, titans, jfreighters, industryCapitals} from './../../characters/SkillData';
import {barges, armor, shield, engineering, rigging} from './../../characters/SkillData';
import {drones, projectiles, missiles, amarrTech3, caldariTech3, gallenteTech3, minmatarTech3} from './../../characters/SkillData';

import {Helper} from './Helper';

export class Runner {
    static run(): void {
        var api: IApiCaller;
        var helper: Helper;
        var skey: string;

        describe('skill-data tests', function() {
            before(function() {
                api = new ApiCaller();
                helper = new Helper();
                return helper.createTestUser().then((s: string) => {
                    skey = s;
                    api.setKey(s);
                });
            });

            function checkSkill(api: IApiCaller, skill: string): any {
                return api.get<ts.dto.SingleLongDto>("/api/typeid/" + encodeURIComponent(skill))
                    .then((data: ts.dto.SingleLongDto) => {
                        assert.isTrue(data.value > 0, skill);
                    });
            }

            function checkNoSkill(api: IApiCaller, skill: string): any {
                return api.get<ts.dto.SingleLongDto>("/api/typeid/" + encodeURIComponent(skill))
                    .then((data: ts.dto.SingleLongDto) => {
                        assert.equal(data.value, -1, skill);
                    });
            }

            //function checkSkillGroup(api: IApiCaller, group: ISkillGrouping): any {
            //    group.section1.map((skill) => {
            //        it('Check ' + skill, function() {
            //            return checkSkill(api, skill);
            //        });
            //    });
            //}


            var groups = [frigates, destroyers, cruisers, battlecruisers, battleships, transport,
                carriers, freighters, dreads, titans, jfreighters, industryCapitals,
                barges, armor, shield, engineering, rigging,
                drones, projectiles, missiles, amarrTech3, caldariTech3, gallenteTech3, minmatarTech3
            ];

            it('Check wrongSkill', function () {
                return checkNoSkill(api, 'wrongSkill');
            });

            groups.map((group) => {
                var tocheck = [];
                tocheck = tocheck.concat(group.section1);
                group.section2.map((arr) => tocheck = tocheck.concat(arr));
                tocheck.map((skill) => {
                    it('Check ' + skill, function() {
                        return checkSkill(api, skill);
                    });
                });
            });
        });
    }
}