import {autoinject} from 'aurelia-framework';
import {Router} from "aurelia-router";
import {AppState} from "app-state";
import {IAccountService, AccountService} from "AccountService";


@autoinject
export class Login {
    static inject = [Router];

    public heading: string;
    public username: string;
    public password: string;
    public destination: string;

    constructor(private theRouter: Router, private service: IAccountService) {
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