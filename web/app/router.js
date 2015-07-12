myApp.Router.map(function () {
    this.resource("home", { path: "/" });
    this.resource("characters", { path: "/characters" });
    this.resource("industry", { path: "/industry" });
    this.resource('sessions', function () {
        this.route('logout');
        this.route('login');
    });
    this.resource('users', function () {
        this.route('signup');
        this.route('user', {
            path: '/user/:user_id'
        });
    });
});
myApp.IndustryRoute = Ember.Route.extend({
    model: function () {
        return this.store.findAll('blueprint');
    }
});
//# sourceMappingURL=router.js.map