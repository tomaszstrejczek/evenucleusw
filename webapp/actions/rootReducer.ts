import { combineReducers } from 'redux'; 
import {LoginReducer} from './LoginActions'; 
 
 
const rootReducer = combineReducers({ 
   loginReducer: LoginReducer
 }); 
 
export { rootReducer }; 
