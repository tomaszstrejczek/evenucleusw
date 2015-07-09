import {inject} from "aurelia-framework";
import {Router} from "aurelia-router";

@inject(Router)
export class Welcome{
    heading: String;
    firstName: String;
    lastName: String;
    _router: Router;

    constructor(router: Router)
    {
        this.heading = "Welcome to the Aurelia Navigation App!";
        this.firstName = "John";
        this.lastName = "Doe";
        this._router = router;
    }

  get fullName(){
    return `${this.firstName} ${this.lastName}`;
  }

  welcome(){
    alert(`Welcome, ${this.fullName}!`);
  }

  activate(a, queryParams, c, d) {
      setTimeout(() => {
          //this._router.navigate("login", "");
      });
  }
}
