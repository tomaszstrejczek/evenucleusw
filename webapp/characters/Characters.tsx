import * as React from 'react';

import {SkillBar} from './SkillBar';
import {purple} from './../utils/colors';



export class Characters extends React.Component<any, any> {
    render(): JSX.Element {
        return (
            <div>
            <p>Characters</p>
                <SkillBar levelCompleted={3} levelTraining={4} color={purple}/>
             </div>
        );
    }
};

