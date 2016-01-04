import * as ReactNotifications from 'react-notifications';

export class LoginInfo {
    public user: string;
    public jwt: string;

    isLoggedIn(): boolean {
        return !!this.user;
    }
}

export class ConfirmInfo {
    public visible: boolean;
    public key: string;
    public title: string;
    public description: string;
    public okButton: string;
    public confirmed: boolean;
}

export interface IAppState {
    loginInfo: LoginInfo;
    notifications: ReactNotifications.Notification[];
    keys: ts.dto.KeyInfoDto[];
    confirmInfo: ConfirmInfo;
    pilots: ts.dto.PilotDto[];
}