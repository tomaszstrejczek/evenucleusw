import * as React from 'react';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';
import {ISkillGrouping} from './SkillData';
import {SkillBar} from './SkillBar';


export interface SkillCardProperties {
    skills: ts.dto.SkillDto[];
    grouping: ISkillGrouping;
    color: TsColor;
    tableCount: number;
    classStyle: string;
}

export class SkillCard extends React.Component<SkillCardProperties, any> {
    render(): JSX.Element {
        var tableCount = this.props.tableCount;
        var classStyle = this.props.classStyle;
        var gr = this.props.grouping.section1;

        var data = Array.apply(0, Array(tableCount)).map((elem) => []);
        var tab = 0;
        for (var i = 0; i < gr.length; ++i) {
            data[tab].push(gr[i]);
            ++tab;
            if (tab >= tableCount)
                tab = 0;
        }

        var that = this;
        function getLevel(skill: string) {
            var elem = that.props.skills.filter(elem => elem.skillName === skill);
            return elem.length > 0 ? elem[0].level : 0;
        }

        return (
            <div className="panel panel-default">
                <div className="panel-body" style={{background:this.props.color.lighter, color: "black"}}>
                    <div className="row">
                        {data.map( (elem, key) => {
                            return <div key={key} className={classStyle}><table><tbody>
                                {elem.map(skill => {
                                    return <tr key={skill}>
                                                        <td style={{ fontSize: "75%", verticalAlign:"middle" }}>{skill}</td>
                                                        <td style={{ verticalAlign: "middle", paddingLeft: "15px", paddingTop:"3px" }}><SkillBar levelCompleted={getLevel(skill)} levelTraining={0} color={this.props.color}/></td>
                                                   </tr>;
                                }) }
                                </tbody></table></div>;
                    })}
                        </div>
                    <div className="row"><div className="col-md-12"><hr/></div></div>
                    <div className="row">
                        {this.props.grouping.section2.map(skill => {
                            return <div key={skill} className="col-md-3"><table><tbody><tr>
                                    <td style={{ fontSize: "75%", verticalAlign: "middle" }}>{skill}</td>
                                    <td style={{ verticalAlign: "middle", paddingLeft: "15px", paddingTop: "3px" }}><SkillBar levelCompleted={getLevel(skill)} levelTraining={0} color={this.props.color}/></td>
                                </tr></tbody></table></div>;
                            })}
                    </div>
                </div>
            </div>
        );
    }

}
