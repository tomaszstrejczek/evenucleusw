import {ActionTypes} from './../actions/ActionTypes';
import {BaseStore} from './BaseStore';


class LoginStore extends BaseStore {

    private _user: string;
    private _jwt: string;

    constructor() {
        super();
        this.subscribe(() => this._registerToActions.bind(this))
        this._user = null;
        this._jwt = null;
    }

    _registerToActions(action) {
        switch(action.actionType) {
            case ActionTypes.LOGIN_USER:
                this._jwt = action.jwt;
                this._user = 'user admin';
                this.emitChange();
                break;
            case ActionTypes.LOGOUT_USER:
                this._user = null;
                this.emitChange();
                break;
            default:
                break;
        };
    }

    user(): string {
        return this._user;
    }

    jwt(): string {
        return this._jwt;
    }

    isLoggedIn():boolean {
        return !!this._user;
    }
}

export var loginStore = new LoginStore();