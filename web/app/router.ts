myApp.Router.map(function () {
    this.resource("home", { path: "/" });
    this.resource("characters", { path: "/characters" });
});

(<any>myApp).HomeRoute = Ember.Route.extend({
    model: function () {
        return this.store.find('blueprint');
    }
});