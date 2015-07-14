/*! React Starter Kit | MIT License | http://www.reactstarterkit.com/ */

import EventEmitter from 'eventemitter3';
import Dispatcher from '../core/Dispatcher';
import ActionTypes from '../constants/ActionTypes';

const CHANGE_EVENT = 'change';

var misLogged = false;
var muserName = '';
var mauthToken = '';

var AuthStore = Object.assign({}, EventEmitter.prototype, {

  isLogged() {
    return misLogged;
  },
  userName() {
    return muserName;
  },
  authToken() {
    return mauthToken;
  },

  /**
   * Emits change event to all registered event listeners.
   *
   * @returns {Boolean} Indication if we've emitted an event.
   */
  emitChange() {
    return this.emit(CHANGE_EVENT);
  },

  /**
   * Register a new change event listener.
   *
   * @param {function} callback Callback function.
   */
  onChange(callback) {
    this.on(CHANGE_EVENT, callback);
  },

  /**
   * Remove change event listener.
   *
   * @param {function} callback Callback function.
   */
  off(callback) {
    this.removeListener(CHANGE_EVENT, callback);
  }

});

AuthStore.dispatchToken = Dispatcher.register((action) => {

  switch (action.type) {

    case ActionTypes.USER_LOGON:
      misLogged = true;
      muserName = action.userName;
      mauthToken = action.authToken;
      AuthStore.emitChange();
      break;

    default:
      // Do nothing
  }

});

export default AuthStore;
