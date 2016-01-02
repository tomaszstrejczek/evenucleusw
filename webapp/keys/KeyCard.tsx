import * as React from 'react';

import {IStoreContext} from './../app/IStoreContext';
import {createConfirmShowAction} from './../actions/ConfirmActions';
import {createKeyDeleteAction} from './../actions/KeyActions';
import {createNotificationShowAction, NotificationType} from './../actions/NotificationActions';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';

import {IDeferredActionExecutorContext} from './../utils/DeferredActionExecutor';
import {IKeyInfoServiceContext} from './../api/KeyInfoService';

export interface KeyCardProperties {
    key?: string;
    keyDto: ts.dto.KeyInfoDto;
    color: TsColor;
}

export class KeyCard extends React.Component<KeyCardProperties, any> {

    context: IStoreContext & IDeferredActionExecutorContext & IKeyInfoServiceContext;

    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired,
        deferredActionExecutor: React.PropTypes.object.isRequired,
        keyInfoService: React.PropTypes.object.isRequired
    };


    handleDelete() {
        var that = this;
        function confirmed() {
            that.context.store.dispatch((dispatch, getState) => {
                that.context.keyInfoService.Delete(that.props.keyDto.keyInfoId)
                    .then(() => {
                        dispatch(createKeyDeleteAction(that.props.keyDto.keyId));
                    })
                    .catch(function (err) {
                        console.log("Error logging in", err);
                        dispatch(createNotificationShowAction(NotificationType.error, "error", err.errorMessage));
                    });
            });
        }

        var key = this.context.deferredActionExecutor.AddAction(confirmed);

        this.context.store.dispatch(createConfirmShowAction(true, key, "Confirm delete", "Do you want delete key " + this.props.keyDto.keyId+"?", "Delete" ));
    }

    render(): JSX.Element {
        return (
            <div className="col-md-4 col-xs-12 col-lg-3" style={{ margin: "5px" }}>
                <div className="panel panel-default">
                  <div className="panel-heading" style={{ background: this.props.color.darkest, color: "white" }} >
                    <span>{this.props.keyDto.keyId}</span><span className="glyphicon glyphicon-trash pull-right" onClick={this.handleDelete.bind(this)}></span>
                  </div>
                  <div className="panel-body" style={{ padding: "0px"}}>
                    <table className="table table-striped" style={{ marginBottom: "0px" }} >
                      <tbody>
                        {this.props.keyDto.pilots.map(pilot => {
                                    return <tr key={"p_"+pilot.name}><td style={{width:"1px"}} ><img src={pilot.url} style={{ width: "24px", height: "24px" }}/></td><td style={{ verticalAlign: "middle" }} >{pilot.name}</td></tr>
                        }) }
                        {this.props.keyDto.corporations.map(corpo => {
                            return <tr key={"c_" + corpo.name}><td style={{ width: "1px" }} ><img src={corpo.url} style={{ width: "24px", height: "24px" }}/></td><td style={{ verticalAlign: "middle" }} >{corpo.name}</td></tr>
                        }) }
                      </tbody>
                    </table>
                  </div>
                </div>
            </div>
        );
    }
}
