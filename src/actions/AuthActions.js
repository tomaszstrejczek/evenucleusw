/*! React Starter Kit | MIT License | http://www.reactstarterkit.com/ */

import Dispatcher from '../core/Dispatcher';
import ActionTypes from '../constants/ActionTypes';

export default {

  logon(user, authToken) {
      Dispatcher.dispatch({
        type: ActionTypes.USER_LOGON,
        userName: user,
        authToken: authToken
      });
    }
};
