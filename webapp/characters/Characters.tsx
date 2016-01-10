import * as React from 'react';

import {IPilotsService, IPilotsServiceContext} from './../api/PilotsService';
import {IBackgroundUpdateService, IBackgroundUpdateServiceContext} from './../api/BackgroundUpdateService';
import {IKeyInfoServiceContext} from './../api/KeyInfoService';
import {IStoreContext} from './../app/IStoreContext';
import {createNotificationShowAction, NotificationType} from './../actions/NotificationActions';
import {IAppState} from './../app/AppState';
import {createPilotsGetAllAction} from './../actions/PilotsActions';

import {purple} from './../utils/colors';
import {PilotInfo, SkillData, PilotInfoProperties} from './PilotInfo';

import autoprefix = require("auto-prefixer");

interface ICharactersState {
    pilots: ts.dto.PilotDto[];
}

var containerProps = (autoprefix as any)({
    display: "flex",
    flexDirection: "row",
    flexWrap: "wrap",
    //alignContent: "flex-end"
});

export class Characters extends React.Component<any, ICharactersState> {
    context: IStoreContext & IKeyInfoServiceContext & IPilotsServiceContext & IBackgroundUpdateServiceContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired,
        keyInfoService: React.PropTypes.object.isRequired,
        pilotsService: React.PropTypes.object.isRequired,
        backgroundUpdateService: React.PropTypes.object.isRequired
    };

    private changeListener: () => void;
    private unsubscribe: Function;

    constructor() {
        this.state = { pilots: [] };
        super();
    }

    componentDidMount() {
        this.changeListener = this._onChange.bind(this);
        this.unsubscribe = this.context.store.subscribe(this.changeListener);

        var that = this;
        this.context.store.dispatch((dispatch, getState) => {
            dispatch(createNotificationShowAction(NotificationType.info, "info", "Background update started"));

            that.context.backgroundUpdateService.Update()
            .then(() => {
                dispatch(createNotificationShowAction(NotificationType.info, "success", "Background update completed"));
                    return that.context.pilotsService.GetAll();
                })
            .then(pilots => {
                    dispatch(createPilotsGetAllAction(pilots));
                })
            .catch(function (err) {
                console.log("Error logging in", err);
                dispatch(createNotificationShowAction(NotificationType.error, "error", err.errorMessage));
            });
        });
    }

    componentWillUnmount() {
        this.unsubscribe();
    }

    _onChange() {
        var s: IAppState = this.context.store.getState() as IAppState;
        this.setState({ pilots: s.pilots});
    }

    render(): JSX.Element {
        var data = this.state.pilots;
        //var data = this.getTestData();

        return (
            <div style={containerProps}>
                {data.map(pilot => {
                    var skillInTraining: ts.dto.SkillInQueueDto = {
                        skillInQueueId: 0,
                        pilotId: pilot.pilotId,
                        skillName: pilot.currentTrainingNameAndLevel.length>2?pilot.currentTrainingNameAndLevel.substr(0, pilot.currentTrainingNameAndLevel.length - 2):"<no training>",
                        level: Number(pilot.currentTrainingNameAndLevel.length > 2?pilot.currentTrainingNameAndLevel.charAt(pilot.currentTrainingNameAndLevel.length - 1):"0"),
                        length: pilot.currentTrainingNameAndLevel.length > 2?pilot.currentTrainingLength:"",
                        order: 0
                    };
                    return <PilotInfo key={pilot.name} name={pilot.name} url={pilot.url} skillsInQueue={pilot.skillsInQueue.sort((a, b) => a.order - b.order)} skillInTraining={skillInTraining} color={purple}/>;
                })}                
             </div>
        );
    }

    getTestData(): ts.dto.PilotDto[] {
        var result = new Array<ts.dto.PilotDto>();
        var p: ts.dto.PilotDto;
        p = {
            name: "empty",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skillsInQueue: new Array<ts.dto.SkillInQueueDto>(),
            pilotId: 1,
            currentTrainingNameAndLevel: "Interceprots 2",
            currentTrainingLength: "10d 20g 5m 3s",
        } as ts.dto.PilotDto;
        result.push(p);

        p = {
            name: "one skill",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skillsInQueue: [
                { skillName: "Skill 1", level: 2, skillInQueueId: 1, pilotId: 1, order: 0 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 2", level: 2, skillInQueueId: 1, pilotId: 1, order: 1 } as ts.dto.SkillInQueueDto,
            ],
            pilotId: 1,
            currentTrainingNameAndLevel: "Interceprots 2",
            currentTrainingLength: "10d 20g 5m 3s"
        } as ts.dto.PilotDto;
        result.push(p);

        p = {
            name: "two skill",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skillsInQueue: [
                { skillName: "Skill 1", level: 2, skillInQueueId: 1, pilotId: 1, order: 0 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 2", level: 2, skillInQueueId: 1, pilotId: 1, order: 1 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 3", level: 3, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
            ],
            pilotId: 1,
            currentTrainingNameAndLevel: "Interceprots 2",
            currentTrainingLength: "10d 20g 5m 3s"
        } as ts.dto.PilotDto;
        result.push(p);

        p = {
            name: "three skill",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skillsInQueue: [
                { skillName: "Skill 1", level: 2, skillInQueueId: 1, pilotId: 1, order: 0 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 2", level: 2, skillInQueueId: 1, pilotId: 1, order: 1 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 3 really long skill name ", level: 3, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 4", level: 4, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
            ],
            pilotId: 1,
            currentTrainingNameAndLevel: "Interceprots 2",
            currentTrainingLength: "10d 20g 5m 3s"
        } as ts.dto.PilotDto;
        result.push(p);

        p = {
            name: "four skill",
            url: "https://image.eveonline.com/Character/1_64.jpg",
            skillsInQueue: [
                { skillName: "Skill 1", level: 2, skillInQueueId: 1, pilotId: 1, order: 0 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 2", level: 2, skillInQueueId: 1, pilotId: 1, order: 1 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 3", level: 3, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 4", level: 4, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
                { skillName: "Skill 5", level: 5, skillInQueueId: 1, pilotId: 1, order: 2 } as ts.dto.SkillInQueueDto,
            ],
            pilotId: 1,
            currentTrainingNameAndLevel: "Interceprots 2",
            currentTrainingLength: "10d 20g 5m 3s"
        } as ts.dto.PilotDto;
        result.push(p);

        return result;
    }
};

