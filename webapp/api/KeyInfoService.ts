import * as When from 'when';
import * as $ from 'jquery';
import {IApiCaller} from './IApiCaller';


export interface IKeyInfoService {
    AddKey(keyid: number, vcode: string): When.Promise<number>;
    GetAll(): When.Promise<ts.dto.KeyInfoDto[]>;
}

export interface IKeyInfoServiceContext {
    authService: IKeyInfoService;
}

export class KeyInfoService implements IKeyInfoService {
    _api: IApiCaller;

    constructor(api: IApiCaller) {
        this._api = api;
    }

    public AddKey(keyid: number, vcode: string): When.Promise<number> {
        return this._api.post<number>("/api/keyinfo/add", { KeyId: keyid, VCode: vcode });
    }

    public GetAll(): When.Promise<ts.dto.KeyInfoDto[]> {
        return this._api.get<ts.dto.KeyInfoDto[]>("/api/keyinfo");
    }
}

