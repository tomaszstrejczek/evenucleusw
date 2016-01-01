﻿import * as React from 'react';

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
    skills: Array<SkillData>;
    color: TsColor;
}

export class PilotInfo extends React.Component<PilotInfoProperties, any> {
    render(): JSX.Element {
        var currentTraining: JSX.Element;
        if (this.props.skills.length > 0)
            currentTraining =
                <div>
                    <div>{this.props.skills[0].name}</div>
                    <div><SkillBar levelCompleted={this.props.skills[0].levelCompleted} levelTraining={this.props.skills[0].levelTraining} color={this.props.color}/></div>
                <div>100d 10h 20m/100h</div>
                    </div>;
        else
            currentTraining =
                <div>-/-</div>;

        var queueTraining = this.props.skills.slice(1, 4);

        var that = this;

        return (
            <div className="col-md-4 col-xs-12 col-lg-3" style={{ margin: "5px" }}>
              <Link to={"/pilot/" + encodeURIComponent(this.props.name)}>
              <table width="100%" >
                <tbody style={{lineHeight: "normal"}}>
                <tr style={{ background: this.props.color.primary }} ><td>
                  <table width="100%" style={{ color: "white" }}>
                    <tbody>
                    <tr><td rowSpan={3} style={{ width: "10px"}}><img src="https://image.eveonline.com/Character/1_64.jpg" style={{margin: "4px"}}/></td></tr>
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
                            return <tr key={skill.name + skill.levelTraining}>
                                <td><div style={{ marginLeft: "4px"}}>{skill.name}</div></td>
                                <td style={{width:"1px"}}><SkillBar levelTraining={skill.levelTraining} levelCompleted={skill.levelCompleted} color={that.props.color}/></td>
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