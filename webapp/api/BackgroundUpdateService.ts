import * as When from 'when';
import * as $ from 'jquery';
import {IApiCaller} from './IApiCaller';


export interface IBackgroundUpdateService {
    Update(): When.Promise<void>;
}

export interface IBackgroundUpdateServiceContext{
    backgroundUpdateService: IBackgroundUpdateService;
}

export class BackgroundUpdateService implements IBackgroundUpdateService {
    _api: IApiCaller;

    constructor(api: IApiCaller) {
        this._api = api;
    }

    public Update(): When.Promise<void> {
        var r = this._api.post<void>("/api/backgroundupdate");
        return r;
    }
}

