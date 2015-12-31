import * as React from 'react';

import { Link } from 'react-router';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';
import {SkillBar} from './SkillBar';


export interface PilotDetailsState {
    name: string;
}

export class PilotDetails extends React.Component<any, PilotDetailsState> {
    render(): JSX.Element {
        return (
            <div>PilotDetails {this.props.params.name}</div>
        );
    }
}
