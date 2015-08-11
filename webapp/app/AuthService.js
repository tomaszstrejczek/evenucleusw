import delay from 'when/delay';
import LoginActions from 'actions/LoginActions';

class AuthService {
    login(username, password) {
        function loginMock()
        {
            if (username !== 'a@a.a')
                throw 'Invalid user/password';
            
            return 'token';
        }

        return delay(100)
            .then(loginMock)
            .then(function(token) {
                LoginActions.loginUser(token);
            });
    }

    logout() {
        function logoutMock()
        {
        }

        return delay(100)
            .then(logoutMock)
            .then(function() {
                LoginActions.logout();
            });
    }
}

export default new AuthService();