/**
 * Created by ts15187 on 2015-07-15.
 */
define(['mirrorkey-browser'], function(mirrorKey) {
    var constants = window.mirrorkey({
        GET_PAGE: null,
        RECEIVE_PAGE: null,
        CHANGE_LOCATION: null
    });

    return constants;
});