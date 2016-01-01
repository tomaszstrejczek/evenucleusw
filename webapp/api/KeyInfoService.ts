import * as When from 'when';
import * as $ from 'jquery';
import {IApiCaller} from './IApiCaller';


export interface IKeyInfoService {
    AddKey(keyid: number, vcode: string): When.Promise<number>;
    GetAll(): When.Promise<ts.dto.KeyInfoDto[]>;
}

export interface IKeyInfoServiceContext {
    keyInfoService: IKeyInfoService;
}

export class KeyInfoService implements IKeyInfoService {
    _api: IApiCaller;

    constructor(api: IApiCaller) {
        this._api = api;
    }

    public AddKey(keyid: number, vcode: string): When.Promise<number> {
        var r = this._api.post<ts.dto.SingleLongDto>("/api/keyinfo/add", { KeyId: keyid, VCode: vcode });
        return r.then((data: ts.dto.SingleLongDto) => data.value);
    }

    public GetAll(): When.Promise<ts.dto.KeyInfoDto[]> {
        return this._api.get<ts.dto.KeyInfoDto[]>("/api/keyinfo");
    }
}

