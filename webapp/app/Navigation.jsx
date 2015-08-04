import React from 'react';
import { Link } from 'react-router';

var Navigation = React.createClass({
    render: function() {
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
                        <li><Link to="/login">Login</Link></li>
                    </ul>
                </div>
            </nav>
        );
    }
});

module.exports = Navigation;

