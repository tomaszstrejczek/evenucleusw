define(['react'], function(React) {
    var Navigation = React.createClass({
        render: function() {
            return (
                <nav className="navbar navbar-default">
                    <div className="navbar-header">
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
                            <li><a href="#/characters">Chracters</a></li>
                            <li><a href="#/industry">Industry</a></li>
                        </ul>
                        <ul className="nav navbar-nav navbar-right">
                            <li><a href="#/login">Login</a></li>
                        </ul>
                    </div>
                </nav>
            );
        }
    });

    return Navigation;
})
