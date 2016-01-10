import * as React from 'react';

import {purple, red} from './../utils/colors';

import { Link } from 'react-router';

import {owl} from './../utils/deepCopy';
import {PilotInfo} from './PilotInfo';
import {SkillCard} from './SkillCard';
import {frigates, destroyers, cruisers, battlecruisers, battleships, transport} from './SkillData';
import {NotFound} from './../special/NotFound';

import {IAppState} from './../app/AppState';
import {IStoreContext} from './../app/IStoreContext';

import autoprefix = require("auto-prefixer");

var containerProps = (autoprefix as any)({
    display: "flex",
    flexDirection: "row",
    flexWrap: "wrap",
    //alignContent: "flex-end"
    marginTop: "-20px"
});

var containerProps2 = (autoprefix as any)({
    display: "flex",
    flexDirection: "column",
    flexWrap: "wrap",
    marginTop: "25px"
    //alignContent: "flex-end"
});

export class PilotDetails extends React.Component<any, any> {
    context: IStoreContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired
    };

    render(): JSX.Element {

        let { name } = this.props.params;
        let { query } = this.props.location;

        var tab = "combat";

        if (query !== undefined && query.tab !== undefined)
            tab = query.tab;

        if (name === undefined)
            return <NotFound/>;

        var tabContent = [frigates, destroyers, cruisers, battlecruisers, battleships];

        if (tab === "industry") {
            tabContent = [transport];
        }

        var s: IAppState = this.context.store.getState() as IAppState;

        var p = s.pilots.filter(elem => elem.name === decodeURIComponent(name));
        if (p === undefined || p===null || p.length!==1)
            return <NotFound/>;

        var data = p[0];

        //var data = this.getTestData();

        var skillInTraining: ts.dto.SkillInQueueDto = {
            skillInQueueId: 0,
            pilotId: data.pilotId,
            skillName: data.currentTrainingNameAndLevel.length > 2 ? data.currentTrainingNameAndLevel.substr(0, data.currentTrainingNameAndLevel.length - 2) : "<no training>",
            level: Number(data.currentTrainingNameAndLevel.length > 2 ? data.currentTrainingNameAndLevel.charAt(data.currentTrainingNameAndLevel.length - 1) : "0"),
            length: data.currentTrainingNameAndLevel.length > 2 ? data.currentTrainingLength : "",
            order: 0
        };

        return (
            <div style={containerProps}>
                <PilotInfo name={data.name} url={data.url} skillsInQueue={data.skillsInQueue.sort((a, b) => a.order - b.order) } skillInTraining={skillInTraining} color={purple} skillCountLimit={100}/>
                <div style={containerProps2}>
                    <ul className="nav nav-pills">
                        <li><Link to={"/pilot/"+encodeURIComponent(name)} query={{tab: "combat"}}>Combat ships</Link></li>
                        <li><Link to={"/pilot/" + encodeURIComponent(name) } query={{ tab: "industry" }}>Industry ships</Link></li>
                    </ul>
                    {tabContent.map((tabInfo, key) => {
                            return <SkillCard key={tab + key.toString()} skills={data.skills} grouping={tabInfo} color={key%2==0?red:purple}/>;
                    })}
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
            name: "two skill",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skillsInQueue: [
                { skillName: "Skill 1", level: 2, skillInQueueId: 1, pilotId: 1, order: 0 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 2", level: 2, skillInQueueId: 1, pilotId: 1, order: 1 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 3", level: 3, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
            ],
            pilotId: 1,
            skills: skills,
            currentTrainingNameAndLevel: "Interceprots 2",
            currentTrainingLength: "10d 20g 5m 3s"
        } as ts.dto.PilotDto;


        return p;
    }

}
