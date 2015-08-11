import React from 'react';
import AuthService from 'app/AuthService';

var Logout = React.createClass({
    componentDidMount: function () {
        AuthService.logout();
    },
    render: function () {
        return (
            <p>You are now logged out</p>
        );
    }

});


module.exports = Logout;