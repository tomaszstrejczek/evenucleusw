/*! React Starter Kit | MIT License | http://www.reactstarterkit.com/ */

import React, { PropTypes } from 'react';
import styles from './LoginPage.less';
import withStyles from '../../decorators/withStyles';
import AuthActions from '../../actions/AuthActions';

@withStyles(styles)
class LoginPage {

  static contextTypes = {
    onSetTitle: PropTypes.func.isRequired
  };

  logon() {
    AuthActions.logon('tomek', 'token123');
  };

  render() {
    let title = 'Log In';
    this.context.onSetTitle(title);
    return (
      <div className="LoginPage">
        <div className="LoginPage-container">
          <h1>{title}</h1>
          <p>...</p>
          <button onClick={this.logon}>Login</button>
        </div>
      </div>
    );
  };
}

export default LoginPage;
