﻿import * as React from 'react';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';

import autoprefix = require("auto-prefixer");

var skillBarOuterStyle = (autoprefix as any)({
    width: "52px",
    height: "12px",
    background: "#00689D",
    borderRadius: "4px",
    //marginLeft: "10px",
    //position: "relative",
    //top: "50%",
    //transform: "translateY(-50%)"
});

var skillBarInnerStyleBase = {
    width: "8px",
    height: "8px",
    background: "#E6E6E6",
    borderRadius: "2px",
    marginTop: "2px",
    marginLeft: "2px",
    display: "inline-block"
};


export interface SkillBarProperties {
    levelCompleted: number;
    levelTraining: number;
    color: TsColor;
    marginLeft?: string;
}

export class SkillBar extends React.Component<SkillBarProperties, any> {
    render(): JSX.Element {
        var outerStyle = owl.copy(skillBarOuterStyle);
        outerStyle.background = this.props.color.darkest;
        if (this.props.marginLeft !== undefined)
            outerStyle.marginLeft = this.props.marginLeft;

        return (
            <div style={outerStyle}>
                {Array.apply(0, Array(Math.max(this.props.levelCompleted, this.props.levelTraining))).map( (obj, index) => {
                    var props = owl.copy(skillBarInnerStyleBase);
                    if (index === 0)
                        props.marginLeft = "2px";
                    if (index < this.props.levelCompleted)
                        props.background = this.props.color.lighter;
                    if (index >= this.props.levelCompleted && index < this.props.levelTraining - 1)
                        props.background = this.props.color.light;
                    if (index === this.props.levelTraining-1)
                        props.background = this.props.color.dark;
                    return <div key={index} style={props}>&nbsp; </div>;
                })
                }
            </div>
        );
    }

}
