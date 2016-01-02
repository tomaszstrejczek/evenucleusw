
export interface IDeferredActionExecutor {
    AddAction(f: Function): string;
    RunAction(key: string): void;
    RemoveAction(key: string) :void;
}

export interface IDeferredActionExecutorContext {
    deferredActionExecutor: IDeferredActionExecutor;
}

export class DeferredActionExecutor implements IDeferredActionExecutor {
    private _actionStore = {};
    private _key = 1;

    public AddAction(f: Function): string {
        ++this._key;
        this._actionStore[this._key.toString()] = f;

        return this._key.toString();
    }
    public RunAction(key: string):void {
        this._actionStore[key]();
        this.RemoveAction(key);
    }
    public RemoveAction(key: string):void  {
        delete this._actionStore[key];
    }
    
}

