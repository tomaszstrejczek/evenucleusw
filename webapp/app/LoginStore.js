import {LOGIN_USER} from 'actions/ActionTypes';
import BaseStore from './BaseStore';


class LoginStore extends BaseStore {

    constructor() {
        super();
        this.subscribe(() => this._registerToActions.bind(this))
        this._user = null;
        this._jwt = null;
    }

    _registerToActions(action) {
        switch(action.actionType) {
            case LOGIN_USER:
                this._jwt = action.jwt;
                this._user = 'user admin';
                this.emitChange();
                break;
            default:
                break;
        };
    }

    get user() {
        return this._user;
    }

    get jwt() {
        return this._jwt;
    }

    isLoggedIn() {
        return !!this._user;
    }
}

export default new LoginStore();