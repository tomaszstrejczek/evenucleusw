import * as When from 'when';
import * as Restful from 'restful.js';
import * as $ from 'jquery';

//import {AppActions} from './../actions/AppActions';

export interface IAuthService {
    login(username: string, password: string): When.Promise<string>;
    logout(): When.Promise<void>;
}

export interface IAuthServiceContext {
    authService: IAuthService;
}

export class AuthService implements IAuthService {
    _api: Restful.Api;

    constructor(api: Restful.Api) {
        this._api = api;
    }
    public login(username: string, password: string): When.Promise<string> {

        return When.promise<string>(function (resolve: (data: string) => void, reject: (reason: any) => void): void {
            $.post("/api/account/login", { email: username, password: password })
                .then((data: any, textStatus: string, jqXHR: JQueryXHR) => {
                    if (jqXHR.status === 202) {
                        reject(data as ts.dto.Error);
                    }
                    else if (jqXHR.status === 200) {
                        resolve(data as string);
                    } else {
                        reject({
                            errorMessage: "Error calling account/login api",
                            fullException: data.toString(),
                            errors: null                        
                        });
                    }
                    resolve(data);
                }, (reason: any) => {
                    reject({
                        errorMessage: "Error calling account/login api",
                        fullException: reason.toString(),
                        errors: null
                    });
                });
        });
        //$.post("/login", { email: username, password: password }

        //return When.resolve('')
        //    .delay(100)
        //    .then(loginMock)
            //.then((token: string):string => {
            //    AppActions.loginUser(token);
            //    return token;
        //})
            ;
    }

    public logout(): When.Promise<void> {
        function logoutMock()
        {
        }

        return When.resolve()
            .then(logoutMock)
            //.then(() => {
            //    AppActions.logout();
        //})
            ;
    }
}

