export class LoginInfo {
    public user: string;
    public jwt: string;

    isLoggedIn(): boolean {
        return !!this.user;
    }
}

export class AppState {
    public loginInfo: LoginInfo;
}