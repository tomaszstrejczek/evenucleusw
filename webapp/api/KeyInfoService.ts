import * as When from 'when';
import * as Restful from 'restful.js';
import * as $ from 'jquery';

//import {AppActions} from './../actions/AppActions';

export interface IKeyInfoService {
    AddKey(keyid: number, vcode: string): When.Promise<number>;
    GetAll(): When.Promise<ts.dto.KeyInfoDto[]>;
}

export interface IKeyInfoServiceContext {
    authService: IKeyInfoService;
}

export class KeyInfoService implements IKeyInfoService {
    _api: Restful.Api;

    constructor(api: Restful.Api) {
        this._api = api;
    }
    public AddKey(keyid: number, vcode: string): When.Promise<number> {
        var that = this;
        return When.promise<number>(function(resolve: (data: number) => void, reject: (reason: any) => void): void {
            that._api.all("keyinfo").post<number>({ KeyId: keyid, VCode: vcode })
                .then(c => resolve(c().data), reject);
        });
    }

    public GetAll(): When.Promise<ts.dto.KeyInfoDto[]> {
        var that = this;
        return When.promise<ts.dto.KeyInfoDto[]>(function (resolve: (data: ts.dto.KeyInfoDto[]) => void, reject: (reason: any) => void): void {
            that._api.all("keyinfo").getAll()
                .then(c => resolve(c().data as ts.dto.KeyInfoDto[]), reject);
        });
        
    }

}

