import {IAuthService, AuthService} from './../../api/AuthService';
import {IApiCaller} from './../../api/IApiCaller';
import {ApiCaller} from './../../api/ApiCaller';

import * as When from 'when';


export class Helper {
    public _email: string;
    public _password: string;
    public _skey: string;

    // return session key for a test user
    // the user does not have any eve api keys defined
    public createTestUser(): When.Promise<string> {
        var api = new ApiCaller();
        var authService = new AuthService(api);
        this._skey = "";

        this._email = "user" + Date.now() + "@ts.ts";
        this._password = "123";
        return authService.register(this._email, this._password);
    }
}