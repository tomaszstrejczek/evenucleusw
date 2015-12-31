import * as React from 'react';

import {yellow, blue} from './../utils/colors';
import {KeyCard} from './KeyCard';

export class Keys extends React.Component<any, any>{
    render(): JSX.Element {
        var data = this.getTestData();

        return (
            <div>
                <div className="row">
                {data.map(key => {
                    return <KeyCard key={key.keyId.toString()} keyDto={key} color={yellow}/>;
                }) }
                    </div>
                <div className="row">
                    <div className="col-md-4 col-xs-12 col-lg-3" style={{ margin: "5px" }}>
                        <div className="panel panel-default">
                          <div className="panel-heading" style={{ background: blue.darkest, color: "white" }} >
                            <span>Add new</span><span className="glyphicon glyphicon-plus pull-right"></span>
                          </div>
                        </div>
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

