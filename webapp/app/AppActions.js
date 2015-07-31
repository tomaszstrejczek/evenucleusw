/**
 * Created by ts15187 on 2015-07-15.
 */
import Dispatcher from 'app/Dispatcher';
import ActionTypes from 'actions/ActionTypes';

function navigateTo(path, options) {
        if (options && options.replace) {
            window.history.replaceState({}, document.title, path);
        } else {
            window.history.pushState({}, document.title, path);
        }

        Dispatcher.dispatch({
            type: ActionTypes.CHANGE_LOCATION,
            path: path
        });
};

function loadPage(path, cb) {
    Dispatcher.dispatch({
        type: ActionTypes.GET_PAGE,
        path: path
    });

/*
    http.get('/api/query?path=' + encodeURI(path))
        .accept('application/json')
        .end(function (err, res) {
            Dispatcher.dispatch({
                type: ActionTypes.RECEIVE_PAGE,
                path: path,
                err: err,
                page: res ? res.body : null
            });
            if (cb) {
                cb();
            }
        });
*/
}

var r = {
    navigateTo: navigateTo,
    loadPage: loadPage
};

module.exports = r;

