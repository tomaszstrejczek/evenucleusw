import * as When from 'when';


export interface IApiCaller {
    setKey(jwt: string): void;
    post<T>(url: string, data?: Object | string): When.Promise<T>;
    get<T>(url: string, data?: Object | string): When.Promise<T>;
}


