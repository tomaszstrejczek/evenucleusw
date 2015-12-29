import * as React from 'react';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';


var skillBarOuterStyle = {
    width: "86px",
    height: "20px",
    background: "#00689D",
    borderRadius: "5px"
};

var skillBarInnerStyleBase = {
    width: "14px",
    height: "16px",
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
}

export class SkillBar extends React.Component<SkillBarProperties, any> {
    render(): JSX.Element {
        var arr = new Array<JSX.Element>();
        if (this.props.levelCompleted === 0 && this.props.levelTraining === 0) {
            arr.push(<div>&nbsp; </div>);
        }

        if (this.props.levelCompleted > 0) {
            var propsFirst = owl.copy(skillBarInnerStyleBase);
            propsFirst.marginLeft = "4px";
            propsFirst.background = this.props.color.lighter;
            arr.push(<div style={propsFirst}>&nbsp; </div>);

            var normalBox = owl.copy(skillBarInnerStyleBase);
            normalBox.background = propsFirst.background;
            for (var i = 1; i < this.props.levelCompleted; ++i)
                arr.push(<div style={normalBox}>&nbsp; </div>);
        }

        if (this.props.levelTraining === 1) {
            var propsFirst = owl.copy(skillBarInnerStyleBase);
            propsFirst.marginLeft = "4px";
            propsFirst.background = this.props.color.primary;
            arr.push(<div style={propsFirst}>&nbsp; </div>);
        } else if (this.props.levelTraining > 1) {
            var propsFirst = owl.copy(skillBarInnerStyleBase);
            propsFirst.background = this.props.color.primary;
            arr.push(<div style={propsFirst}>&nbsp; </div>);
        }

        var outerStyle = owl.copy(skillBarOuterStyle);
        outerStyle.background = this.props.color.darkest;

        return (
            <div style={outerStyle}>
                {arr}
                </div>
        );
    }

}
