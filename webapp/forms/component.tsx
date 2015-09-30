import * as React from 'react';

class ComponentProps {
    disabled: boolean = false;
    validatePristine: boolean = false;
    onChange():void { };
    onFocus():void { };
    onBlur():void { };
};

export class ComponentMixin extends React.Mixin<ComponentProps, any> {

/*
    propTypes: {
        layout: React.PropTypes.string
    };

    contextTypes: {
        layout: React.PropTypes.string
    };

    getDefaultProps() {
        return {
            disabled: false,
            validatePristine: false,
            onChange: function() {},
            onFocus: function() {},
            onBlur: function() {}
        };
    };

    hashString(string) {
        var hash = 0;
        for (var i = 0; i < string.length; i++) {
            hash = (((hash << 5) - hash) + string.charCodeAt(i)) & 0xFFFFFFFF;
        }
        return hash;
    };

    getId() {
        return this.props.id || this.props.name.split('[').join('_').replace(']', '') + this.hashString(JSON.stringify(this.props));
    };

    getLayout() {
        var defaultLayout = this.context.layout || 'horizontal';
        return this.props.layout ? this.props.layout : defaultLayout;
    };

    renderHelp() {
        if (!this.props.help) {
            return '';
        }
        return (
            <span className="help-block">{this.props.help}</span>
        );
    };

    renderErrorMessage() {
        if (!this.showErrors()) {
            return '';
        }
        var errorMessage = this.getErrorMessage();
        if (!errorMessage) {
            return '';
        }
        return (
            <span className="help-block validation-message">{errorMessage}</span>
        );
    };

    showErrors() {
        if (this.isPristine() === true) {
            if (this.props.validatePristine === false) {
                return false;
            }
        }
        return (this.isValid() === false);
    }
    */
};