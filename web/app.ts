import {Router, RouterConfiguration} from "aurelia-router";

import "bootstrap";
import "bootstrap/css/bootstrap.css!";

export class App {
    router: Router;

    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = "Aurelia";
        config.map([
            { route: ["", "welcome"], name: "welcome", moduleId: "./welcome", nav: true, title: "Welcome" }
        ]);        

        this.router = router;
    }
}
