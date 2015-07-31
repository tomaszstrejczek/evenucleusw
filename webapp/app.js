define(['app/App.jsx', 'react', 'jquery', 'fastclick', 'actions/ActionTypes', 'app/Dispatcher', 'react-dom'],
    function(App, React, $, FastClick, ActionTypes, Dispatcher, ReactDom) {
    console.log('app starting');

    require('bootstrap-less/bootstrap');
    require('styles/less/flat-ui');

    var path = decodeURI(window.location.hash);

    function onSetMeta(name, content) {
        // Remove and create a new <meta /> tag in order to make it work
        // with bookmarks in Safari
        var elements = document.getElementsByTagName('meta');
        [].slice.call(elements).forEach(function (element) {
            if (element.getAttribute('name') === name) {
                element.parentNode.removeChild(element);
            }
        });

        var meta = document.createElement('meta');
        meta.setAttribute('name', name);
        meta.setAttribute('content', content);
        document.getElementsByTagName('head')[0].appendChild(meta);
    };

    function run() {
        // Render the top-level React component
        var props = {
            path: path,
            context: {
                onSetTitle: function(value) { document.title = value },
                onSetMeta: onSetMeta
            }
        };

        var element = React.createElement(App, props);

        React.render(element, document.getElementById('app'));

        // Update `Application.path` prop when `window.location` is changed
        Dispatcher.register(function(action) {
            if (action.type === ActionTypes.CHANGE_LOCATION) {
                element = React.cloneElement(element, {path: action.path});
                React.render(element, document.getElementById('app'));
            }
        });
    };

    $(document).ready(function() {
        FastClick.attach(document.body);
        run();
    });

});
