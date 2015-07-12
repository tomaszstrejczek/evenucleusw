myApp.Router.map(function () {
    this.resource("home", { path: "/" });
    this.resource("characters", { path: "/characters" });
    this.resource("industry", { path: "/industry" });
});

(<any>myApp).IndustryRoute = Ember.Route.extend({
    model: function () {
        return this.store.findAll('blueprint');
    }
});