import * as React from 'react';

import { Link } from 'react-router';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';
import {SkillBar} from './SkillBar';

export class SkillData {
    name: string;
    levelCompleted: number;
    levelTraining: number;
}

export interface PilotInfoProperties {
    key?: string;
    url: string;
    name: string;
    skillInTraining: ts.dto.SkillInQueueDto,
    skillsInQueue: ts.dto.SkillInQueueDto[];
    color: TsColor;
    skillCountLimit?: number;
}

export class PilotInfo extends React.Component<PilotInfoProperties, any> {
    render(): JSX.Element {
        var currentTraining: JSX.Element;
        if (this.props.skillInTraining !== undefined)
            currentTraining =
                <div>
                    <div>{this.props.skillInTraining.skillName}</div>
                    <div><SkillBar levelCompleted={this.props.skillInTraining.level-1} levelTraining={this.props.skillInTraining.level} color={this.props.color}/></div>
                <div>{this.props.skillInTraining.length}</div>
                    </div>;
        else
            currentTraining =
                <div>-/-</div>;

        var limit = this.props.skillCountLimit === undefined ? 3 : this.props.skillCountLimit;
        var queueTraining = this.props.skillInTraining.length.length > 0 ? this.props.skillsInQueue.slice(1, limit+1) : this.props.skillsInQueue.slice(0, limit);

        var that = this;

        return (
            <div style={{ margin: "25px" }}>
              <Link to={"/pilot/" + encodeURIComponent(this.props.name)}>
              <table width="100%" >
                <tbody style={{lineHeight: "normal"}}>
                <tr style={{ background: this.props.color.primary }} ><td>
                  <table width="100%" style={{ color: "white" }}>
                    <tbody>
                    <tr><td rowSpan={3} style={{ width: "10px"}}><img src={this.props.url} style={{margin: "4px"}}/></td></tr>
                    <tr><td style={{ height: "1px", lineHeight: "normal" }}>{this.props.name}</td></tr>
                    <tr><td style={{ verticalAlign: "bottom", fontSize: "75%" }}>
                        {currentTraining}
                        </td></tr>
                    </tbody>
                    </table>
                </td></tr>
                <tr><td>
                    <table width="100%">
                    <tbody style={{ background: "white", fontSize: "75%", color: "black"}}>
                    {queueTraining.map(skill => {
                            return <tr key={skill.skillName + skill.level}>
                                <td><div style={{ marginLeft: "4px", marginRight:"20px" }}>{skill.skillName}</div></td>
                                <td style={{width:"1px"}}><SkillBar levelTraining={skill.level} levelCompleted={skill.level-1} color={that.props.color}/></td>
                                </tr>;
                    }) }
                    </tbody>
                    </table>
                </td></tr>
            </tbody>
            </table>
            </Link>
        </div>
        );
    }
}
