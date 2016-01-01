///<reference path='../react/react.d.ts' />


declare module ReactModal {
    import React = __React;

    interface IOverlay {
        position?: string;
        top?:number;
        left?:number;
        right?: number;
        bottom?: number;
        backgroundColor?: string;
    }

    interface IContent {
        position?:string;
        top?: string;
        left?: string;
        right?: string;
        bottom?: string;
        border?: string;
        background?: string;
        overflow?: string;
        WebkitOverflowScrolling?: string;
        borderRadius?: string;
        outline?: string;
        padding?: string;
    }

    interface IStyle {
        overlay?: IOverlay;
        content?: IContent;        
    }

    interface ReactModalProperties {
        isOpen: boolean;
        onRequestClose?: Function;
        closeTimeoutMS?: number;
        style?: IStyle;
        className?: string;
    }

    interface IReactModal extends React.Component<ReactModalProperties, any> {
    }
}

declare module "react-modal" {
    export = ReactModal;
}
