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

export const createKeyGetAllAction = createAction<ts.dto.KeyInfoDto[]>(
    ActionTypes[ActionTypes.KEY_GETALL],
    (keys: ts.dto.KeyInfoDto[]) => {
        return keys;
    }
);

export const createKeyDeleteAction = createAction<number>(
    ActionTypes[ActionTypes.KEY_DELETE],
    (keyid: number) => {
        return keyid;
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
    [ActionTypes[ActionTypes.KEY_GETALL]]: (state: ts.dto.KeyInfoDto[], action: Action): ts.dto.KeyInfoDto[] => {
        var payload = action.payload as ts.dto.KeyInfoDto[];
        return payload;
    },
    [ActionTypes[ActionTypes.KEY_DELETE]]: (state: ts.dto.KeyInfoDto[], action: Action): ts.dto.KeyInfoDto[] => {
        var payload = action.payload as number;
        var result = owl.deepCopy(state.filter(elem => elem.keyId!== payload), 10) as ts.dto.KeyInfoDto[];
        return result;
    },
}, new Array<ts.dto.KeyInfoDto>()
);