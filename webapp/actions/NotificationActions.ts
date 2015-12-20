import {ActionTypes} from './ActionTypes';
import * as ReactNotifications from 'react-notifications';
import {createAction, handleActions, Action} from 'redux-actions';
import {owl} from './../utils/deepCopy';

export enum NotificationType {
    info,
    success,
    warning,
    error
}


export const createNotificationShowAction = createAction<ReactNotifications.Notification>(
    ActionTypes[ActionTypes.NOTIFICATION_SHOW],
    (type: NotificationType, title: string, message: string, timeOut: number = 5000) => {       
        return {
            id: new Date().getTime(),
            title: title,
            message: message,
            timeOut: timeOut,
            onClick: undefined,
            type: NotificationType[type]
        };
    }
);

export const createNotificationHideAction = createAction<number>(
    ActionTypes[ActionTypes.NOTIFICATION_HIDE],
    (id: number) => id
);


export var NotificationReducer = handleActions<ReactNotifications.Notification[]>(
{
    [ActionTypes[ActionTypes.NOTIFICATION_SHOW]]: (state: ReactNotifications.Notification[], action: Action): ReactNotifications.Notification[] => {
        var payload = action.payload as ReactNotifications.Notification;
        var result = owl.deepCopy(state, 10);
        result.push(payload);
        return result;
    },
    [ActionTypes[ActionTypes.NOTIFICATION_HIDE]]: (state: ReactNotifications.Notification[], action: Action): ReactNotifications.Notification[] => {
        var result = owl.deepCopy(state, 10);
        var payload = action.payload as number;

        result = result.filter(n => n.id !== payload);

        return result;
    },
}, new Array<ReactNotifications.Notification>()
);