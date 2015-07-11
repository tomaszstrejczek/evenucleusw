myApp.Router.map(function () {
    this.resource("home", { path: "/" });
    this.resource("characters", { path: "/characters" });
});
myApp.HomeRoute = Ember.Route.extend({
    model: function () {
        return this.store.find('blueprint');
    }
});
//# sourceMappingURL=router.js.map