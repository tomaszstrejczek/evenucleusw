///<reference path='../react/react.d.ts' />

declare module FormsyReact {
    import React = __React;

    interface FormsyBaseState {
        _value: any;
        _isRequired: boolean;
        _isValid: boolean;
        _isPristine: boolean;
        _pristineValue: any;
        _validationError: string;
        _externalError: string;
        _formSubmitted: boolean;
    }

    interface FormsyBaseProps {
        validationError: string;
        validationErrors: any;

    }

    interface MixinInterface {
        getInitialState(): FormsyBaseState;
        getDefaultProps(): FormsyBaseProps;
        componentWillMount(): void;
        componentWillReceiveProps(nextProps: any): void;
        componentDidUpdate(): void;
        componentWillUnmount(): void;
        setValidations(validations: any, required: boolean): void;
        setValue(value: any):void;
        resetValue(): void;
        getValue(): any;
        hasValue(): boolean;
        getErrorMessage(): any;
        isFormDisabled(): boolean;
        isValid(): boolean;
        isPristine(): boolean;
        isFormSubmitted(): boolean;
        isRequired(): boolean;
        showRequired(): boolean;
        showError(): boolean;
        isValidValue(): boolean;
    }

    interface FormInterface extends React.ReactElement<any> {
    }


    var Mixin: MixinInterface;
    var Form: FormInterface;
}

declare module "formsy-react" {
    export = FormsyReact;
}
