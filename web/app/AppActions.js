/**
 * Created by ts15187 on 2015-07-15.
 */
define(['app/Dispatcher', 'actions/ActionTypes'],
    function(Dispatcher, ActionTypes) {

    function navigateTo(path, options) {
        //this.loadPage(path, function() {
            if (options && options.replace) {
                window.history.replaceState({}, document.title, path);
            } else {
                window.history.pushState({}, document.title, path);
            }

            Dispatcher.dispatch({
                type: ActionTypes.CHANGE_LOCATION,
                path
            });
        //});
    };

    function loadPage(path, cb) {
        Dispatcher.dispatch({
            type: ActionTypes.GET_PAGE,
            path
        });

/*
        http.get('/api/query?path=' + encodeURI(path))
            .accept('application/json')
            .end(function (err, res) {
                Dispatcher.dispatch({
                    type: ActionTypes.RECEIVE_PAGE,
                    path,
                    err,
                    page: res ? res.body : null
                });
                if (cb) {
                    cb();
                }
            });
*/
    }

    return {
        navigateTo: navigateTo,
        loadPage: loadPage
    } ;

})