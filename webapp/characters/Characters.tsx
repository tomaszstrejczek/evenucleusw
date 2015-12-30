import * as React from 'react';

import {purple} from './../utils/colors';
import {PilotInfo, SkillData, PilotInfoProperties} from './PilotInfo';



export class Characters extends React.Component<any, any> {
    render(): JSX.Element {
        var data = this.getTestData();

        return (
            <div className="row">
                {data.map(pilot => {
                    return <PilotInfo key={pilot.name} name={pilot.name} url={pilot.url} skills={pilot.skills} color={pilot.color}/>;
                })}                
             </div>
        );
    }

    getTestData(): Array<PilotInfoProperties> {
        var result = new Array<PilotInfoProperties>();
        var p: PilotInfoProperties;
        p = {
            name: "empty",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skills: new Array<SkillData>(),
            color: purple
        };
        result.push(p);

        p = {
            name: "one skill",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skills: [{ name:"Minmatar Starship Engineering", levelCompleted:0, levelTraining: 1}],
            color: purple
        };
        result.push(p);

        p = {
            name: "one skill bis",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skills: [{ name: "Minmatar Starship Engineering", levelCompleted: 5, levelTraining: 0 }],
            color: purple
        };
        result.push(p);

        p = {
            name: "two skills",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skills: [{ name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 0 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 1 }],
            color: purple
        };
        result.push(p);

        p = {
            name: "more skills",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skills: [{ name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 1 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 2 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 3 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 4 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 5 }, { name: "Minmatar Starship Engineering2", levelCompleted: 2, levelTraining: 5 }],
            color: purple
        };
        result.push(p);

        return result;
    }
};

