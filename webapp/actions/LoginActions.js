import Dispatcher from 'app/Dispatcher';
import {LOGIN_USER, LOGOUT_USER} from 'actions/ActionTypes';

export default {
    loginUser: (jwt) => {
        var savedJwt = localStorage.getItem('jwt');

        if (savedJwt !== jwt) {
            //var nextPath = RouterContainer.get().getCurrentQuery().nextPath || '/';

            //RouterContainer.get().transitionTo(nextPath);
            localStorage.setItem('jwt', jwt);
        }

        Dispatcher.dispatch({
            actionType: LOGIN_USER,
            jwt: jwt
        });
    },

    logout: () => {
        localStorage.clear('jwt');

        Dispatcher.dispatch({
            actionType: LOGOUT_USER
        });
    }
};