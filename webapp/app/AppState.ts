import * as ReactNotifications from 'react-notifications';

export class LoginInfo {
    public user: string;
    public jwt: string;

    isLoggedIn(): boolean {
        return !!this.user;
    }
}


export interface IAppState {
    loginInfo: LoginInfo;
    notifications: ReactNotifications.Notification[];
}