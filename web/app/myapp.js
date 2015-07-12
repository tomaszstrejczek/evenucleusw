var Application = Ember.Application;
window.MyApp = myApp = Application.create({
    LOG_TRANSITIONS: true,
    LOG_TRANSITIONS_INTERNAL: true
});

myApp.NavigationController = Ember.Controller.extend({
    items: Ember.A([
        Ember.Object.create({ title: "Characters", location: 'characters', active: null }),
        Ember.Object.create({ title: "Industry", location: 'industry', active: null })
    ])
});


myApp.ListLinkComponent = Ember.Component.extend({
    tagName: 'li',
    classNameBindings: ['active'],
    active: function () {
        return this.get('childViews').anyBy('active');
    }.property('childViews.@each.active')
});

myApp.ApplicationAdapter = DS.FixtureAdapter.extend();

myApp.ApplicationController = Ember.Controller.extend({
    // requires the sessions controller
    needs: ['sessions'],
    // creates a computed property called currentUser that will be
    // binded on the curretUser of the sessions controller and will return its value
    currentUser: (function () {
        return this.get('controllers.sessions.currentUser');
    }).property('controllers.sessions.currentUser'),
    // creates a computed property called isAuthenticated that will be
    // binded on the curretUser of the sessions controller and will verify if the object is empty
    isAuthenticated: (function () {
        return !Ember.isEmpty(this.get('controllers.sessions.currentUser'));
    }).property('controllers.sessions.currentUser')
});

// For more information see: http://emberjs.com/guides/routing/
myApp.ApplicationRoute = Ember.Route.extend({
    actions: {
        // create a global logout action
        logout: function () {
            // get the sessions controller instance and reset it to then transition to the sessions route
            this.controllerFor('sessions').reset();
            this.transitionTo('sessions');
        }
    }
});
