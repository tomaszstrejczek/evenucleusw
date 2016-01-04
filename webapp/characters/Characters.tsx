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


interface ICharactersState {
    pilots: ts.dto.PilotDto[];
}


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

        return (
            <div className="row">
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

    //getTestData(): Array<PilotInfoProperties> {
    //    var result = new Array<PilotInfoProperties>();
    //    var p: PilotInfoProperties;
    //    p = {
    //        name: "empty",
    //        url: "https://image.eveonline.com/Character/1_64.jpg",
    //        skillsInQueue: new Array<tspan.>(),
    //        color: purple
    //    };
    //    result.push(p);

    //    p = {
    //        name: "one skill",
    //        url: "https://image.eveonline.com/Character/1_64.jpg",
    //        skills: [{ name:"Minmatar Starship Engineering", levelCompleted:0, levelTraining: 1}],
    //        color: purple
    //    };
    //    result.push(p);

    //    p = {
    //        name: "one skill bis",
    //        url: "https://image.eveonline.com/Character/1_64.jpg",
    //        skills: [{ name: "Minmatar Starship Engineering", levelCompleted: 5, levelTraining: 0 }],
    //        color: purple
    //    };
    //    result.push(p);

    //    p = {
    //        name: "two skills",
    //        url: "https://image.eveonline.com/Character/1_64.jpg",
    //        skills: [{ name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 0 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 1 }],
    //        color: purple
    //    };
    //    result.push(p);

    //    p = {
    //        name: "more skills",
    //        url: "https://image.eveonline.com/Character/1_64.jpg",
    //        skills: [{ name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 1 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 2 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 3 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 4 }, { name: "Minmatar Starship Engineering", levelCompleted: 0, levelTraining: 5 }, { name: "Minmatar Starship Engineering2", levelCompleted: 2, levelTraining: 5 }],
    //        color: purple
    //    };
    //    result.push(p);

    //    return result;
    //}
};

