export class LoginInfo {
    private _user: string;
    private _jwt: string;
    user(): string {
        return this._user;
    }
    jwt(): string {
        return this._jwt;
    }

    isLoggedIn(): boolean {
        return !!this._user;
    }
}

export class AppState {
    constructor() {
        this.loginInfo = new LoginInfo();
    }
    public loginInfo: LoginInfo;
}