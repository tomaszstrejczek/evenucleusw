import React from 'react';
import Navigation from 'app/Navigation.jsx';
import Characters from 'characters/Characters.jsx';
import Login from 'app/Login.jsx';
import Industry from 'industry/Industry.jsx';
import AppActions from 'app/AppActions';
import Router from 'react-router'

var RouteHandler = Router.RouteHandler;

var App = React.createClass({
    render: function() {
        return (
            <div>
                <Navigation/>
                <div className="container">
                <RouteHandler/>
                </div>
            </div>
        );
    }
});

module.exports = App;