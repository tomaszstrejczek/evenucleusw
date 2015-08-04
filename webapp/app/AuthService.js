import delay from 'when/delay';
import LoginActions from 'actions/LoginActions';

class AuthService {
    login(username, password) {
        function loginMock()
        {
            if (username !== 'admin')
                throw 'Invalid user/password';
            
            return 'token';
        }

        return delay(100)
            .then(loginMock)
            .then(function(token) {
                LoginActions.loginUser(token);
            });
    }
}

export default new AuthService();