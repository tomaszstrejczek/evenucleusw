import * as React from 'react';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';
import {ISkillGrouping} from './SkillData';
import {SkillBar} from './SkillBar';

import autoprefix = require("auto-prefixer");

var containerProps = (autoprefix as any)({
    display: "flex",
    flexDirection: "row",
    flexWrap: "wrap",
    //alignContent: "flex-end"
    marginTop: "-20px"
});

var row = (autoprefix as any)({
    display: "flex",
    flexDirection: "row",
    flexWrap: "wrap",
    justifyContent: "space-between",
    transformStyle: "preserve-3d"
    //alignContent: "flex-end"
});

var rowElement = (autoprefix as any)({
    display: "flex",
    flexDirection: "row",
    flexWrap: "wrap",
    marginRight: "10px",
    justifyContent: "flex-start",
    alignItems: "center"
    //alignContent: "flex-end"
});


export interface SkillCardProperties {
    skills: ts.dto.SkillDto[];
    grouping: ISkillGrouping;
    color: TsColor;
}

export class SkillCard extends React.Component<SkillCardProperties, any> {
    render(): JSX.Element {
        var that = this;
        function getLevel(skill: string) {
            var elem = that.props.skills.filter(elem => elem.skillName === skill);
            return elem.length > 0 ? elem[0].level : 0;
        }

        return (
            <div className="panel panel-default">
                <div className="panel-body" style={{background:this.props.color.lighter, color: "black"}}>
                    <div style={row}>
                        {this.props.grouping.section1.map((skill, key) => {
                             return <div key={key} style={rowElement}>
                                        <div>{skill}</div>
                                        <SkillBar levelCompleted={getLevel(skill) } levelTraining={0} color={this.props.color}/>
                                    </div>;
                    })}
                    </div>
                    <div><hr/></div>
                    <div style={row}>
                        {this.props.grouping.section2.map((skill, key) => {
                            return <div key={key} style={rowElement}>
                                        <div>{skill}</div>
                                        <SkillBar levelCompleted={getLevel(skill) } levelTraining={0} color={this.props.color}/>
                                </div>;
                        }) }
                        </div>
                </div>
            </div>
        );
    }

}
