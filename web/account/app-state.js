export class AppState {
    /**
      * Simple application state
      *
      */
    constructor() {
        this.isAuthenticated = false;
    }
    login(username, password) {
        if (username == "Admin" && password == "xxx") {
            this.isAuthenticated = true;
            return true;
        }
        this.logout();
        return false;
    }
    logout() {
        this.isAuthenticated = false;
    }
}
//# sourceMappingURL=app-state.js.map