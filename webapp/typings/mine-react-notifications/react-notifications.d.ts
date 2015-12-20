///<reference path='../react/react.d.ts' />

import React = __React;

declare module ReactNotifications {
    class Notification {
        id: number;
        title: string;
        message: string;
        timeOut: number; //[milliseconds],
        onClick: () => void;
        type: any;
    }

    interface NotificationProperties {
        notifications?: Notification[];
        onRequestHide?: (Notification) => void;
    }

    interface INotifications extends React.Component<NotificationProperties, any> {
    }
}

declare module "react-notifications" {
    export = ReactNotifications;
}
