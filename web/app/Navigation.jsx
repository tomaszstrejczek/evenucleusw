define(['react'], function(React) {
    var Navigation = React.createClass({
        propTypes: function() {
            return {
                path: PropTypes.string
            };
        },
        render: function() {
            var charactersClass = this.props.path === '#/characters'  ? 'active':'';
            var industryClass = this.props.path === '#/industry'?'active':'';
            var loginClass = this.props.path === '#/login'?'active':'';

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
                            <li className={charactersClass}><a href="#/characters">Chracters</a></li>
                            <li className={industryClass}><a href="#/industry">Industry</a></li>
                        </ul>
                        <ul className="nav navbar-nav navbar-right">
                            <li className={loginClass}><a href="#/login">Login</a></li>
                        </ul>
                    </div>
                </nav>
            );
        }
    });

    return Navigation;
})
