myApp.Router.map(function () {
    this.resource("home", { path: "/" });
});
myApp.HomeRoute = Ember.Route.extend({
    model: function () {
        return this.store.find('blueprint');
    }
});
//# sourceMappingURL=router.js.map