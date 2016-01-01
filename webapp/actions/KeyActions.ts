import {ActionTypes} from './ActionTypes';
import {createAction, handleActions, Action} from 'redux-actions';
import {owl} from './../utils/deepCopy';

export const createKeyAddAction = createAction<ts.dto.KeyInfoDto>(
    ActionTypes[ActionTypes.KEY_ADD],
    (keyinfoid: number, keyid: number, vcode:string) => {       
        return {
            keyInfoId: keyinfoid,
            keyId: keyid,
            vCode: vcode,
            userId: undefined,
            pilots: undefined,
            corporations: undefined
        };
    }
);


export var KeyReducer = handleActions<ts.dto.KeyInfoDto[]>(
{
    [ActionTypes[ActionTypes.KEY_ADD]]: (state: ts.dto.KeyInfoDto[], action: Action): ts.dto.KeyInfoDto[] => {
        var payload = action.payload as ts.dto.KeyInfoDto;
        var result = owl.deepCopy(state, 10);
        result.push(payload);
        return result;
    },
}, new Array<ts.dto.KeyInfoDto>()
);