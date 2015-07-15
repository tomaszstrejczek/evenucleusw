define(['react', 'jsx!app/Navigation', 'jsx!characters/Characters', 'jsx!app/Login', 'jsx!industry/Industry', 'app/AppActions'],
    function(React, Navigation, Characters, Login, Industry, AppActions) {

    var App = React.createClass({
        propTypes: function() {
            return {
                path: PropTypes.string.isRequired
            };
        },
        componentDidMount: function() {
            window.addEventListener('popstate', this.handlePopState);
        },

        componentWillUnmount: function() {
            window.removeEventListener('popstate', this.handlePopState);
        },
        shouldComponentUpdate: function(nextProps) {
            return this.props.path !== nextProps.path;
        },

        handlePopState: function (event) {
            AppActions.navigateTo(window.location.hash, {replace: !!event.state});
        },

        render: function() {
            var component;
            switch (this.props.path) {

                case '':
                case '#/':
                case '/':
                case '#/characters':
                    component = <Characters />;
                    break;

                case '#/login':
                    component = <Login />;
                    break;

                case '#/industry':
                    component = <Industry />;
                    break;
            }

            return (
                <div>
                    <Navigation/>
                    {component}
                </div>
            );
        }
    });

    return App;
})
