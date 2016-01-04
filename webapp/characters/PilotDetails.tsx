import * as React from 'react';

import {purple, red} from './../utils/colors';

import { Link } from 'react-router';

import {owl} from './../utils/deepCopy';
import {PilotInfo} from './PilotInfo';
import {SkillCard} from './SkillCard';
import {frigates, destroyers} from './SkillData';

export interface PilotDetailsState {
    name: string;
}

export class PilotDetails extends React.Component<any, PilotDetailsState> {
    render(): JSX.Element {
        var data = this.getTestData();

        var skillInTraining: ts.dto.SkillInQueueDto = {
            skillInQueueId: 0,
            pilotId: data.pilotId,
            skillName: data.currentTrainingNameAndLevel.substr(0, data.currentTrainingNameAndLevel.length - 2),
            level: Number(data.currentTrainingNameAndLevel.charAt(data.currentTrainingNameAndLevel.length - 1)),
            length: data.currentTrainingLength,
            order: 0
        };

        return (
            <div className="row">
                <PilotInfo name={data.name} url={data.url} skillsInQueue={data.skillsInQueue.sort((a, b) => a.order - b.order)} skillInTraining={skillInTraining} color={purple}/>
                <div className="col-md-7 col-xs-11 col-lg-8">
                    <SkillCard skills={data.skills} grouping={frigates} color={red} tableCount={4} classStyle="col-md-3"/>
                    <SkillCard skills={data.skills} grouping={destroyers} color={purple} tableCount={2} classStyle="col-md-6"/>
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
