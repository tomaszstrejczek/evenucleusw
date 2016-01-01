import * as React from 'react';
import { Link } from 'react-router';
import {IStoreContext} from './IStoreContext';
import {IAppState} from './AppState';

class NavigationState {
    constructor(public userLoggedIn: boolean, public userName: string) {
    }
}

export class Navigation extends React.Component<any, NavigationState> {

    context: IStoreContext;
    static contextTypes: React.ValidationMap<any> = {
        store: React.PropTypes.object.isRequired
    };


    public state: NavigationState;
    private changeListener: () => void;
    private unsubscribe: Function;

    constructor(props, context) {
        super(props, context);
        var s: IAppState = this.context.store.getState() as IAppState;
        this.state = new NavigationState(s.loginInfo.isLoggedIn(), s.loginInfo.user);
    }

    componentDidMount() {
        this.changeListener = this._onChange.bind(this);
        this.unsubscribe = this.context.store.subscribe(this.changeListener);
    }

    _onChange() {
        var s: IAppState = this.context.store.getState() as IAppState;
        this.setState(new NavigationState(s.loginInfo.isLoggedIn(), s.loginInfo.user));
    }

    componentWillUnmount() {
        this.unsubscribe();
    }

    render(): JSX.Element {
        var login, hello;
        if (this.state.userLoggedIn) {
            hello = <p className="navbar-text">Signed in as {this.state.userName}</p>
            login = <li><Link to="/logout">Logout</Link></li>;
        } else {
            hello = null;
            login = <li><Link to="/login">Login</Link></li>;
        }

        return (
            <nav className="navbar navbar-default">
                <div className="navbar-header active">
                    <button type="button" className="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span className="sr-only">Toggle navigation</span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                        <span className="icon-bar"></span>
                    </button>
                    <a className="navbar-brand" href="#">EveNucleus</a>
                </div>
                <div className="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul className="nav navbar-nav">
                        <li><Link to="/characters">Chracters</Link></li>
                        <li><Link to="/industry">Industry</Link></li>
                        <li><Link to="/keys">Eve Api Keys</Link></li>
                    </ul>
                    <ul className="nav navbar-nav navbar-right">
                        {hello}
                        {login}
                    </ul>
                </div>
            </nav>
        );
    }
};


