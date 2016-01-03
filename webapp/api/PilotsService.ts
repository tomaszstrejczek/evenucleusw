import * as When from 'when';
import * as $ from 'jquery';
import {IApiCaller} from './IApiCaller';


export interface IPilotsService {
    GetAll(): When.Promise<ts.dto.PilotDto[]>;
}

export interface IPilotsServiceContext {
    pilotsService: IPilotsService;
}

export class PilotsService implements IPilotsService {
    _api: IApiCaller;

    constructor(api: IApiCaller) {
        this._api = api;
    }

    public GetAll(): When.Promise<ts.dto.PilotDto[]> {
        var r = this._api.get<ts.dto.PilotDto[]>("/api/pilots");
        return r;
    }
}

