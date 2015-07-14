/*! React Starter Kit | MIT License | http://www.reactstarterkit.com/ */

import React, { PropTypes } from 'react';
import classNames from 'classnames';
import styles from './Navigation.less';
import withStyles from '../../decorators/withStyles';
import Link from '../../utils/Link';
import AuthStore from '../../stores/AuthStore';

function getNavState() {
  return {
    userName: AuthStore.userName()
  };
}


@withStyles(styles)
class Navigation extends React.Component{

  static propTypes = {
    className: PropTypes.string
  };

  constructor(props) {
    super(props);
    this.state = getNavState();
    this.onChange = this.onChange.bind(this);
  }
  componentDidMount() {
    AuthStore.onChange(this.onChange);
  }

  componentWillUnmount() {
    AuthStore.onChange(this.onChange);
  }

  onChange() {
    this.setState(getNavState());
  }

  render() {
    return (
      <div className={classNames(this.props.className, 'Navigation')} role="navigation">
        <a className="Navigation-link" href="/industry" onClick={Link.handleClick}>Industry</a>
        <a className="Navigation-link" href="/about" onClick={Link.handleClick}>About</a>
        <a className="Navigation-link" href="/contact" onClick={Link.handleClick}>Contact</a>
        <span className="Navigation-spacer"> | </span>
        <a className="Navigation-link" href="/login" onClick={Link.handleClick}>Log in</a>
        <span className="Navigation-spacer">logged as </span>
        <span className="Navigation-spacer">{this.state.userName ? this.state.userName : ''} </span>
        <span className="Navigation-spacer">or</span>
        <a className="Navigation-link Navigation-link--highlight" href="/register" onClick={Link.handleClick}>Sign up</a>
      </div>
    );
  }

}

export default Navigation;
