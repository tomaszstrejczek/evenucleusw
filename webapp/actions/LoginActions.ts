import {ActionTypes} from './ActionTypes';
import {LoginInfo} from './../app/AppState';
import {createAction, handleActions, Action} from 'redux-actions';
import {owl} from './../utils/deepCopy';

export const createLoginAction = createAction<string>(
    ActionTypes[ActionTypes.LOGIN_USER],
    (jwt: string) => jwt
);

export const createLogoutAction = createAction<void>(
    ActionTypes[ActionTypes.LOGOUT_USER],
    () => {}
);



export var LoginReducer = handleActions<LoginInfo>(
{
    [ActionTypes[ActionTypes.LOGIN_USER]]: (state: LoginInfo, action: Action): LoginInfo => {
        var result = owl.clone(state);
        var jwt = action.payload as string;

        var savedJwt = localStorage.getItem('jwt');

        if (savedJwt !== jwt) {
            //var nextPath = RouterContainer.get().getCurrentQuery().nextPath || '/';

            //RouterContainer.get().transitionTo(nextPath);
            localStorage.setItem('jwt', jwt);
        }

        result.jwt = jwt;
        result.user = "test user";
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