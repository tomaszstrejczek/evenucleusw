import React from 'react';
import Navigation from 'app/Navigation.jsx';
import Characters from 'characters/Characters.jsx';
import Login from 'app/Login.jsx';
import Industry from 'industry/Industry.jsx';
import AppActions from 'app/AppActions';

var App = React.createClass({
    componentDidMount: function() {
        window.addEventListener('popstate', this.handlePopState);
    },

    componentWillUnmount: function() {
        window.removeEventListener('popstate', this.handlePopState);
    },
    shouldComponentUpdate: function(nextProps) {
        return this.props.path !== nextProps.path;
    },

    handlePopState: function (event) {
        AppActions.navigateTo(window.location.hash, {replace: !!event.state});
    },

    render: function() {
        var component;
        switch (this.props.path) {

            case '':
            case '#/':
            case '/':
            case '#/characters':
                component = <Characters />;
                break;

            case '#/login':
                component = <Login />;
                break;

            case '#/industry':
                component = <Industry />;
                break;
        }

        return (
            <div>
                <Navigation path={this.props.path}/>
                <div className="container">
                {component}
                </div>
            </div>
        );
    }
});

module.exports = App;