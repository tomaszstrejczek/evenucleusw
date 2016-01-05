import * as React from 'react';

import {purple, red} from './../utils/colors';

import { Link } from 'react-router';

import {owl} from './../utils/deepCopy';
import {PilotInfo} from './PilotInfo';
import {SkillCard} from './SkillCard';
import {frigates, destroyers, cruisers, battlecruisers, battleships} from './SkillData';
import {NotFound} from './../special/NotFound';

import {IAppState} from './../app/AppState';
import {IStoreContext} from './../app/IStoreContext';


export class PilotDetails extends React.Component<any, any> {
    context: IStoreContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired
    };

    render(): JSX.Element {

        let { name } = this.props.params;

        if (name === undefined)
            return <NotFound/>;

        var s: IAppState = this.context.store.getState() as IAppState;

        var p = s.pilots.filter(elem => elem.name === decodeURIComponent(name));
        if (p === undefined || p===null || p.length!==1)
            return <NotFound/>;

        var data = p[0];

        var skillInTraining: ts.dto.SkillInQueueDto = {
            skillInQueueId: 0,
            pilotId: data.pilotId,
            skillName: data.currentTrainingNameAndLevel.length > 2 ? data.currentTrainingNameAndLevel.substr(0, data.currentTrainingNameAndLevel.length - 2) : "<no training>",
            level: Number(data.currentTrainingNameAndLevel.length > 2 ? data.currentTrainingNameAndLevel.charAt(data.currentTrainingNameAndLevel.length - 1) : "0"),
            length: data.currentTrainingNameAndLevel.length > 2 ? data.currentTrainingLength : "",
            order: 0
        };

        return (
            <div className="row">
                <PilotInfo name={data.name} url={data.url} skillsInQueue={data.skillsInQueue.sort((a, b) => a.order - b.order)} skillInTraining={skillInTraining} color={purple}/>
                <div className="col-md-7 col-xs-11 col-lg-8">
                    <SkillCard skills={data.skills} grouping={frigates} color={red} tableCount={4} classStyle="col-md-3"/>
                    <SkillCard skills={data.skills} grouping={destroyers} color={purple} tableCount={2} classStyle="col-md-6"/>
                    <SkillCard skills={data.skills} grouping={cruisers} color={red} tableCount={4} classStyle="col-md-3"/>
                    <SkillCard skills={data.skills} grouping={battlecruisers} color={purple} tableCount={1} classStyle="col-md-12"/>
                    <SkillCard skills={data.skills} grouping={battleships} color={red} tableCount={2} classStyle="col-md-6"/>
                </div>
            </div>
        );
    }

    getTestData(): ts.dto.PilotDto {
        var skills = new Array<ts.dto.SkillDto>();
        skills.push({ skillName: "Interceptors", level: 3 } as ts.dto.SkillDto);
        skills.push({ skillName: "Assault Frigates", level: 4 } as ts.dto.SkillDto);
        skills.push({ skillName: "Covert Ops", level: 5 } as ts.dto.SkillDto);
        skills.push({ skillName: "Amarr Frigates", level: 5 } as ts.dto.SkillDto);
        
        var p = {
            name: this.props.params.name,
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skills: skills
        } as ts.dto.PilotDto;


        return p;
    }

}
