myApp.Router.map(function () {
    this.resource("home", { path: "/" });
});

(<any>myApp).HomeRoute = Ember.Route.extend({
    model: function () {
        return this.store.find('blueprint');
    }
});