import Application = Ember.Application;

interface MyWindow extends Window {
    MyApp: Application;
};

declare var myWindow: MyWindow;
myWindow = <MyWindow>window;

declare var myApp: Application;
myWindow.MyApp = <Application>Application.create({
    rootElement: "#app-body"
});
myApp = myWindow.MyApp;

