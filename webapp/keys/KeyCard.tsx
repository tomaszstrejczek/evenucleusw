import * as React from 'react';

import {owl} from './../utils/deepCopy';
import {TsColor} from './../utils/colors';

export interface KeyCardProperties {
    key?: string;
    keyDto: ts.dto.KeyInfoDto;
    color: TsColor;
}

export class KeyCard extends React.Component<KeyCardProperties, any> {
    render(): JSX.Element {
        return (
            <div className="col-md-4 col-xs-12 col-lg-3" style={{ margin: "5px" }}>
                <div className="panel panel-default">
                  <div className="panel-heading" style={{ background: this.props.color.darkest, color: "white" }} >
                    <span>{this.props.keyDto.keyId}</span><span className="glyphicon glyphicon-trash pull-right"></span>
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
