import React from 'react';
import { Link } from 'react-router';
import LoginStore from 'app/LoginStore';

var Navigation = React.createClass({

    getInitialState: function() {
        return {
            userLoggedIn: LoginStore.isLoggedIn()
        };
    },

    componentDidMount: function() {
        this.changeListener = this._onChange.bind(this);
        LoginStore.addChangeListener(this.changeListener);
    },

    _onChange: function() {
        this.setState(this.getInitialState());
    },

    componentWillUnmount: function() {
        LoginStore.removeChangeListener(this.changeListener);
    },

    render: function() {
        var login;
        if (this.state.userLoggedIn)
            login = <li><Link to="/logout">Logout</Link></li>;
        else
            login = <li><Link to="/login">Login</Link></li>;

        return (
            <nav className="navbar navbar-default">
                <div className="navbar-header active">
                    <button type="button" className="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span className="sr-only">Toggle navigation</span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                    </button>
                    <a className="navbar-brand" href="#">EveNucleus</a>
                </div>
                <div className="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul className="nav navbar-nav">
                        <li><Link to="/characters">Chracters</Link></li>
                        <li><Link to="/industry">Industry</Link></li>
                    </ul>
                    <ul className="nav navbar-nav navbar-right">
                        {login}
                    </ul>
                </div>
            </nav>
        );
    }
});

module.exports = Navigation;

