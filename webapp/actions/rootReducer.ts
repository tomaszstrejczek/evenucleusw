import { combineReducers } from 'redux'; 
import {LoginReducer} from './LoginActions'; 
 
 
const rootReducer = combineReducers({
    loginInfo: LoginReducer
}); 
 
export { rootReducer }; 
