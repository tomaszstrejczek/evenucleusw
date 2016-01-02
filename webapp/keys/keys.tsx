import * as React from 'react';

import {IKeyInfoServiceContext} from './../api/KeyInfoService';
import {IStoreContext} from './../app/IStoreContext';
import {createNotificationShowAction, NotificationType} from './../actions/NotificationActions';
import {IAppState} from './../app/AppState';
import {createKeyGetAllAction} from './../actions/KeyActions';

import {yellow, blue} from './../utils/colors';
import {KeyCard} from './KeyCard';
import {Link} from 'react-router';

import update =require('react-addons-update');


interface IKeysState {
    keys: ts.dto.KeyInfoDto[];
}
export class Keys extends React.Component<IKeysState, any>{
    context: IStoreContext & IKeyInfoServiceContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired,
        keyInfoService: React.PropTypes.object.isRequired
    };

    private changeListener: () => void;
    private unsubscribe: Function;

    constructor() {
        this.state = { keys: [] };
        super();
    }

    componentDidMount() {
        this.changeListener = this._onChange.bind(this);
        this.unsubscribe = this.context.store.subscribe(this.changeListener);

        var that = this;
        this.context.store.dispatch( (dispatch, getState) => {
            that.context.keyInfoService.GetAll()
                .then( keys => {
                    dispatch(createKeyGetAllAction(keys));
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
        //var currentKeys = this.state.keys;
        //currentKeys.clear();
        //// to add
        //var currentKeyIds = currentKeys.map(elem => elem.keyId);
        //var toadd = s.keys.filter(elem => currentKeyIds.indexOf(elem.keyId)===-1);

        //// to remove
        //var newIds = s.keys.map(elem => elem.keyId);
        //var toremove = currentKeys.filter(elem => newIds.indexOf(elem.keyId) === -1);

        //currentKeys = update(currentKeys, { $push: toadd});
        //currentKeys = update(currentKeys, { $shift: toremove });

        this.setState({ keys: s.keys });
    }

    render(): JSX.Element {
        var data = this.state.keys;

        return (
            <div>
                <div className="row">
                {data.map(key => {
                    return <KeyCard key={key.keyId.toString()} keyDto={key} color={yellow}/>;
                }) }
                    </div>
                <div className="row">
                    <div className="col-md-4 col-xs-12 col-lg-3" style={{ margin: "5px" }}>
                        <Link to="/keyadd">
                            <div className="panel panel-default">
                              <div className="panel-heading" style={{ background: blue.darkest, color: "white" }} >
                                <span>Add new</span><span className="glyphicon glyphicon-plus pull-right"></span>
                              </div>
                            </div>
                        </Link>
                    </div>
                </div>
            </div>
        );
    }

    getTestData(): Array<ts.dto.KeyInfoDto> {
        var result = new Array<ts.dto.KeyInfoDto>();

        var p1 = {
            name: "stryju",
            url: "https://image.eveonline.com/Character/1_64.jpg"
        } as ts.dto.PilotDto;

        var p2 = {
            name: "pilot2",
            url: "https://image.eveonline.com/Character/1_64.jpg"
        } as ts.dto.PilotDto;

        var p3 = {
            name: "pilot3",
            url: "https://image.eveonline.com/Character/1_64.jpg"
        } as ts.dto.PilotDto;

        var c1 = {
            name: "corpo1",
            url: "https://image.eveonline.com/Character/1_64.jpg"
        } as ts.dto.CorporationDto;

        var k: ts.dto.KeyInfoDto = {
            keyInfoId: 1,
            keyId: 12345678,
            vCode: "alamakota",
            userId: 1,
            pilots: [p1],
            corporations: []
        };
        result.push(k);

        k = {
            keyInfoId: 2,
            keyId: 12345671,
            vCode: "alamakota",
            userId: 1,
            pilots: [p1, p2, p3],
            corporations: []
        };
        result.push(k);

        k = {
            keyInfoId: 3,
            keyId: 12345672,
            vCode: "alamakota",
            userId: 1,
            pilots: [],
            corporations: [c1]
        };
        result.push(k);

        return result;
    }
};

