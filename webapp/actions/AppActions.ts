import {Dispatcher} from './../app/Dispatcher';
import {ActionTypes} from './ActionTypes';

export class AppActions {
    public static loginUser(jwt: string): void {
        var savedJwt = localStorage.getItem('jwt');

        if(savedJwt !== jwt) {
            //var nextPath = RouterContainer.get().getCurrentQuery().nextPath || '/';

            //RouterContainer.get().transitionTo(nextPath);
            localStorage.setItem('jwt', jwt);
        }

        Dispatcher.dispatch({
            actionType: ActionTypes.LOGIN_USER,
            jwt: jwt
        });
    };

    public static logout(): void {
        localStorage.removeItem('jwt');

        Dispatcher.dispatch({
            actionType: ActionTypes.LOGOUT_USER
        });
    }
};

