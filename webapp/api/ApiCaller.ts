import * as When from 'when';
import * as $ from 'jquery';
import {IApiCaller} from './IApiCaller';

export class ApiCaller implements IApiCaller {
    private _jwt: string;

    public constructor() {
        this._jwt = "";
    }

    public setKey(jwt: string): void {
        this._jwt = jwt;
    }

    private call<T>(url: string, verb: string, data?: Object | string): When.Promise<T> {
        var that = this;
        return When.promise<T>(function(resolve: (data: T) => void, reject: (reason: any) => void): void {
            $.ajax({
                    url: url,
                    type: verb,
                    data: data,
                    headers: {
                        'jwt': that._jwt
                    },
                    dataType: 'json'
                })
                .then((data: any, textStatus: string, jqXHR: JQueryXHR) => {
                        if (jqXHR.status === 202) {
                            reject(data as ts.dto.Error);
                        } else if (jqXHR.status === 200) {
                            resolve(data as T);
                        } else {
                            reject({
                                errorMessage: "Error calling api",
                                fullException: JSON.stringify(data),
                                errors: null
                            });
                        }
                })
                .fail((reason: any) => {
                    if (reason.status === 200) 
                        resolve(undefined);
                    else
                        reject({
                            errorMessage: "Error calling api",
                            fullException: JSON.stringify(reason),
                            errors: null
                        });
                    }
                );
        });        
    }

    public post<T>(url: string, data?: Object | string): When.Promise<T> {
        return this.call<T>(url, 'post', data);
    }

    public get<T>(url: string, data?: Object | string): When.Promise<T> {
        return this.call<T>(url, 'get', data);        
    }
}


