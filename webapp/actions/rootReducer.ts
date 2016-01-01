import { combineReducers } from 'redux'; 
import {LoginReducer} from './LoginActions'; 
import {NotificationReducer} from './NotificationActions'; 
import {KeyReducer} from './KeyActions'; 
 
 
const rootReducer = combineReducers({
    loginInfo: LoginReducer,
    notifications: NotificationReducer,
    keys: KeyReducer
}); 
 
export { rootReducer }; 
