export interface ActionClass<T extends Action> {
    prototype: T;
}

// Base class for actions
export abstract class Action {
    type: string;
    constructor() {
        // Copy from the prototype onto the instance
        this.type = this.type;
    }
}

// Decorator to set type names for action classes
export function typeName(name: string) {
    return function <T extends Action>(actionClass: ActionClass<T>) {
        actionClass.prototype.type = name;
    }
}

// Type guard for type checking
export function isType<T extends Action>(action: Action, actionClass: ActionClass<T>): action is T {
    return action.type == actionClass.prototype.type;
}

