import Application = Ember.Application;

interface MyWindow extends Window {
    MyApp: Application;
};


declare var myWindow: MyWindow;
myWindow = <MyWindow>window;

declare var myApp: Application;

myWindow.MyApp = <Application>Application.create();
myApp = myWindow.MyApp;

(<any>myApp).NavigationController = Ember.Controller.extend({
    items: Ember.A([
        Ember.Object.create({ title: "Characters", location: 'characters', active: null })
    ])
});

(<any>myApp).ListLinkComponent = Ember.Component.extend({
    tagName: 'li',
    classNameBindings: ['active'],
    active: function () {
        return this.get('childViews').anyBy('active');
    }.property('childViews.@each.active')
});
