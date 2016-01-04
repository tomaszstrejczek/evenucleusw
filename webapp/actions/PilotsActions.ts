import {ActionTypes} from './ActionTypes';
import {createAction, handleActions, Action} from 'redux-actions';
import {owl} from './../utils/deepCopy';

export const createPilotsGetAllAction = createAction<ts.dto.PilotDto[]>(
    ActionTypes[ActionTypes.PILOTS_GETALL],
    (pilots: ts.dto.PilotDto[]) => {
        return pilots;
    }
);

export var PilotsReducer = handleActions<ts.dto.PilotDto[]>(
{
    [ActionTypes[ActionTypes.PILOTS_GETALL]]: (state: ts.dto.PilotDto[], action: Action): ts.dto.PilotDto[] => {
        var payload = action.payload as ts.dto.PilotDto[];
        return payload;
    },
}, new Array<ts.dto.PilotDto>()
);