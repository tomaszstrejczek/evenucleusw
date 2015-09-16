import { EventEmitter } from 'events';
import {Dispatcher} from './Dispatcher';

export class BaseStore extends EventEmitter {

    constructor() {
        super();
    }

    subscribe(actionSubscribe) {
        this._dispatchToken = Dispatcher.register(actionSubscribe());
    }

    dispatchToken():string {
        return this._dispatchToken;
    }

    emitChange() {
        this.emit('CHANGE');
    }

    addChangeListener(cb) {
        this.on('CHANGE', cb)
    }

    removeChangeListener(cb) {
        this.removeListener('CHANGE', cb);
    }

    private _dispatchToken: string;
}