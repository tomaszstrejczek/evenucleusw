if (typeof __decorate !== "function") __decorate = function (decorators, target, key, desc) {
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") return Reflect.decorate(decorators, target, key, desc);
    switch (arguments.length) {
        case 2: return decorators.reduceRight(function(o, d) { return (d && d(o)) || o; }, target);
        case 3: return decorators.reduceRight(function(o, d) { return (d && d(target, key)), void 0; }, void 0);
        case 4: return decorators.reduceRight(function(o, d) { return (d && d(target, key, o)) || o; }, desc);
    }
};
import { autoinject } from 'aurelia-framework';
import { Router } from "aurelia-router";
export let Login = class {
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
};
Object.defineProperty(Login, "name", { value: "Login", configurable: true });
Login.inject = [Router];
Login = __decorate([
    autoinject
], Login);
//# sourceMappingURL=login.js.map