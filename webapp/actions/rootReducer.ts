import { combineReducers } from 'redux'; 
import {LoginReducer} from './LoginActions'; 
import {NotificationReducer} from './NotificationActions'; 
 
 
const rootReducer = combineReducers({
    loginInfo: LoginReducer,
    notifications: NotificationReducer
}); 
 
export { rootReducer }; 
