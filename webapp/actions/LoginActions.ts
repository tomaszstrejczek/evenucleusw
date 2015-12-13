import {ActionTypes} from './ActionTypes';
import {LoginInfo} from './../app/AppState';
import {createAction, handleActions, Action} from 'redux-actions';
import {owl} from './../utils/deepCopy';
import {IApiCaller} from './../api/IApiCaller';

interface LoginActionData {
    user: string;
    jwt: string;
}

export const createLoginAction = createAction<LoginActionData>(
    ActionTypes[ActionTypes.LOGIN_USER],
    (api: IApiCaller, jwt: string, user: string) => {
        api.setKey(jwt);
        return { user: user, jwt: jwt };
    }
);

export const createLogoutAction = createAction<void>(
    ActionTypes[ActionTypes.LOGOUT_USER],
    () => {}
);

export const createRegisterAction = createAction<LoginActionData>(
    ActionTypes[ActionTypes.REGISTER_USER],
    (api: IApiCaller, jwt: string, user:string) => {
        api.setKey(jwt);
        return { user: user, jwt: jwt };
    }
);



export var LoginReducer = handleActions<LoginInfo>(
{
    [ActionTypes[ActionTypes.LOGIN_USER]]: (state: LoginInfo, action: Action): LoginInfo => {
        var result = owl.clone(state);
        var payload = action.payload as LoginActionData;
        var jwt = payload.jwt;

        var savedJwt = localStorage.getItem('jwt');

        if (savedJwt !== jwt) {
            //var nextPath = RouterContainer.get().getCurrentQuery().nextPath || '/';

            //RouterContainer.get().transitionTo(nextPath);
            localStorage.setItem('jwt', jwt);
        }

        result.jwt = jwt;
        result.user = payload.user;
        return result;
    },
    [ActionTypes[ActionTypes.REGISTER_USER]]: (state: LoginInfo, action: Action): LoginInfo => {
        var result = owl.clone(state);
        var payload = action.payload as LoginActionData;
        var jwt = payload.jwt;

        var savedJwt = localStorage.getItem('jwt');

        if (savedJwt !== jwt) {
            //var nextPath = RouterContainer.get().getCurrentQuery().nextPath || '/';

            //RouterContainer.get().transitionTo(nextPath);
            localStorage.setItem('jwt', jwt);
        }

        result.jwt = jwt;
        result.user = payload.user;
        return result;
    },

    [ActionTypes[ActionTypes.LOGOUT_USER]]: (state: LoginInfo, action: Action): LoginInfo => {
        var result = owl.clone(state);

        localStorage.removeItem('jwt');

        result.jwt = undefined;
        result.user = undefined;
        return result;
    }
}, new LoginInfo()
);