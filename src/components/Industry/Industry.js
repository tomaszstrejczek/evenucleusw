import React, { PropTypes } from 'react';
import withStyles from '../../decorators/withStyles';
import styles from './industry.less';

@withStyles(styles)
class RegisterPage {

  static contextTypes = {
    onSetTitle: PropTypes.func.isRequired
  };

  render() {
    let title = 'Industry';
    this.context.onSetTitle(title);
    return (
      <div className="Industry">
        <div className="Industry-container">
          <h1>{title}</h1>
          <p>...</p>
        </div>
      </div>
    );
  }

}

export default RegisterPage;
