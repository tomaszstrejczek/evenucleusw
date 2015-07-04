import { Router } from "aurelia-router";
export class Login {
    constructor(theRouter, service) {
        this.theRouter = theRouter;
        this.service = service;
        this.heading = "aurelia login page";
        this.username = "Admin";
        this.password = "xxx";
        this.destination = "#/";
    }
    activate(a, queryParams, c, d) {
        if (queryParams && queryParams.origin)
            this.destination = queryParams.origin;
    }
    trylogin() {
        //if (app.state.login(this.username, this.password))
        //    this.theRouter.navigate(this.destination, true);
        //else
        //    alert("Access denied");
    }
}
Login.inject = [Router];
//# sourceMappingURL=login.js.map