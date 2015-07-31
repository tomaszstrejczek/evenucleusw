import React from 'react';
import LinkedStateMixin from 'react/lib/LinkedStateMixin';

require('app/Login.css');

var Login = React.createClass({
    mixins: [LinkedStateMixin],
    getInitialState: function() {
        return {
        user: '',
        password: ''
        };
    },
    login: function(e) {
        e.preventDefault();
    },
    render: function() {
        return (
            <div className="row">
                <div className="col-sm-6 col-md-4 col-md-offset-4">
                    <h1 className="text-center login-title">Sign in to continue to Bootsnipp</h1>
                    <div className="account-wall">
                        <form className="form-signin">
                            <input type="text" className="form-control" placeholder="Email" required autofocus valueLink={this.linkState('user')}></input>
                            <input type="password" className="form-control" placeholder="Password" required valueLink={this.linkState('password')}></input>
                            <button className="btn btn-lg btn-primary btn-block" type="submit" onClick={this.login}>
                                Sign in</button>
                            <label className="checkbox pull-left">
                                <input type="checkbox" value="remember-me"></input>
                                Remember me
                            </label>
                        </form>
                    </div>
                    <a href="#" className="text-center new-account">Create an account </a>
                </div>
            </div>
        );
    }
});

module.exports = Login;

