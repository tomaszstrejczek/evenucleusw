var Application = Ember.Application;
;
myWindow = window;
myWindow.MyApp = Application.create();
myApp = myWindow.MyApp;
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
//# sourceMappingURL=myapp.js.map