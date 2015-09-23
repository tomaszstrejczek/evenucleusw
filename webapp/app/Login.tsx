import * as React from 'react';

//import LinkedStateMixin from 'react/lib/LinkedStateMixin';
//import AuthService from 'app/AuthService';
import {Input} from './forms/input';

require('app/Login.css');

var Formsy = require('formsy-react');

class MyInput extends React.Component<any, any> {
}

//var MyInput = React.createClass({

//    // Add the Formsy Mixin
//    mixins: [Formsy.Mixin],

//    // setValue() will set the value of the component, which in 
//    // turn will validate it and the rest of the form
//    changeValue: function (event) {
//        this.setValue(event.currentTarget.value);
//    },
//    render: function () {

//        // Set a specific className based on the validation
//        // state of this component. showRequired() is true 
//        // when the value is empty and the required prop is 
//        // passed to the input. showError() is true when the 
//        // value typed is invalid
//        var className = this.showRequired() ? 'form-group required' : this.showError() ? 'form-group has-error' : 'form-group has-success';

//        // An error message is returned ONLY if the component is invalid
//        // or the server has returned an error message
//        var errorMessage = this.getErrorMessage();

//        return (
//            <div className={className}>
//                <input type={this.props.type} className="form-control" placeholder={this.props.placeholder} onChange={this.changeValue} value={this.getValue()}/>
//                <span>{errorMessage}</span>
//            </div>
//        );
//    }
//});

class LoginState {
    formError: string;

    constructor() {
        this.formError= '';
    }
}


export class Login extends React.Component<any, LoginState> {
}

//var Login = React.createClass({
//    mixins: [LinkedStateMixin],
//    contextTypes: { 
//        router: React.PropTypes.func 
//    }, 

//    getInitialState: function() {
//        return {
//            formError: ''
//        };
//    },
//    enableButton: function () {
//        this.setState({
//            canSubmit: true
//        });
//    },
//    disableButton: function () {
//        this.setState({
//            canSubmit: false
//        });
//    },
//    submit: function(model) {
//        var that = this;
//        AuthService.login(model.email, model.password)
//            .then(function() {
//                that.context.router.transitionTo('/');
//            })
//            .catch(function(err) {
//                console.log("Error logging in", err);
//                that.setState({
//                    formError: err
//                });
//            });
//    },
//    render: function() {
//        return (
//            <div className="row">
//                <div className="col-sm-6 col-md-4 col-md-offset-4">
//                    <h1 className="text-center login-title">Sign in to continue to Bootsnipp</h1>
//                    <div className="account-wall">
//                        <Formsy.Form className="form-signin" onValidSubmit={this.submit} onValid={this.enableButton} onInvalid={this.disableButton}>
//                            <span className="help-block">{this.state.formError}</span>
//                            <Input name="email" type="text" validations="isEmail" placeholder="Email" required autofocus layout="elementOnly"></Input>
//                            <Input name="password" type="password" placeholder="Password" required layout="elementOnly"></Input>
//                            <button className="btn btn-lg btn-primary btn-block" type="submit" disabled={!this.state.canSubmit}>
//                                Sign in</button>
//                            <label className="checkbox pull-left">
//                                <input type="checkbox" value="remember-me"></input>
//                                Remember me
//                            </label>
//                        </Formsy.Form>
//                    </div>
//                    <a href="#" className="text-center new-account">Create an account </a>
//                </div>
//            </div>
//        );
//    }
//});

//module.exports = Login;

