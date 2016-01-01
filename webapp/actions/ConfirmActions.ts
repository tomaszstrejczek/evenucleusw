import {ActionTypes} from './ActionTypes';
import {createAction, handleActions, Action} from 'redux-actions';
import {owl} from './../utils/deepCopy';
import {ConfirmInfo} from './../app/AppState';

export const createConfirmShowAction = createAction<ConfirmInfo>(
    ActionTypes[ActionTypes.CONFIRM_SHOW],
    (visible: boolean, owner: string, title: string, description: string, okButton: string) => {       
        return {
            visible: visible,
            owner: owner,
            title: title,
            description: description,
            okButton: okButton,
            confirmed: false
        };
    }
);

export const createConfirmConfirmAction = createAction<void>(
    ActionTypes[ActionTypes.CONFIRM_CONFIRM]
);

export var ConfirmReducer = handleActions<ConfirmInfo>(
{
    [ActionTypes[ActionTypes.CONFIRM_SHOW]]: (state: ConfirmInfo, action: Action): ConfirmInfo => {
        var payload = action.payload as ConfirmInfo;
        return payload;
    },
    [ActionTypes[ActionTypes.CONFIRM_CONFIRM]]: (state: ConfirmInfo, action: Action): ConfirmInfo => {
        var result = owl.deepCopy(state, 10) as ConfirmInfo;
        result.confirmed = true;
        result.visible = false;
        return result;
    },
}, {visible: false, owner: "", title: "", description: "", okButton: "Ok", confirmed: false}
);