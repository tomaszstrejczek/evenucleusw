import * as When from 'when';
import {AppActions} from './../actions/AppActions';

export class AuthService {
    public static login(username: string, password:string): When.Promise<string> {
        function loginMock()
        {
            if (username !== 'a@a.a')
                throw 'Invalid user/password';
            
            return 'token';
        }

        return When.resolve('')
            .delay(100)
            .then(loginMock)
            .then((token: string):string => {
                AppActions.loginUser(token);
                return token;
            });
    }

    public static logout(): When.Promise<void> {
        function logoutMock()
        {
        }

        return When.resolve()
            .then(logoutMock)
            .then(() => {
                AppActions.logout();
            });
    }
}

