import * as When from 'when';
import * as $ from 'jquery';
import {IApiCaller} from './IApiCaller';

export interface IAuthService {
    login(username: string, password: string): When.Promise<string>;
    logout(): When.Promise<void>;
}

export interface IAuthServiceContext {
    authService: IAuthService;
}

export class AuthService implements IAuthService {
    _api: IApiCaller;

    constructor(api: IApiCaller) {
        this._api = api;
    }

    public login(username: string, password: string): When.Promise<string> {
        return this._api.post<string>("/api/account/login", { email: username, password: password });
    }

    public logout(): When.Promise<void> {
        return this._api.post<void>("/api/account/logout");
    }
}

