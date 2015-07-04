var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") return Reflect.decorate(decorators, target, key, desc);
    switch (arguments.length) {
        case 2: return decorators.reduceRight(function(o, d) { return (d && d(o)) || o; }, target);
        case 3: return decorators.reduceRight(function(o, d) { return (d && d(target, key)), void 0; }, void 0);
        case 4: return decorators.reduceRight(function(o, d) { return (d && d(target, key, o)) || o; }, desc);
    }
};
import { inject } from "aurelia-framework";
import { Router } from "aurelia-router";
export let Welcome = class {
    constructor(router) {
        this.heading = "Welcome to the Aurelia Navigation App!";
        this.firstName = "John";
        this.lastName = "Doe";
        this._router = router;
    }
    get fullName() {
        return `${this.firstName} ${this.lastName}`;
    }
    welcome() {
        alert(`Welcome, ${this.fullName}!`);
    }
    activate(a, queryParams, c, d) {
        setTimeout(() => {
            this._router.navigate("login", "");
        });
    }
};
Welcome = __decorate([
    inject(Router)
], Welcome);
//# sourceMappingURL=welcome.js.map