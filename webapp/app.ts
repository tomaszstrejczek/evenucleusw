/// <reference path="references.d.ts" />

//import App from 'app/App.jsx';
//import React from 'react';
import $ = require('jquery');
import {FastClick} from 'fastclick';
//import ActionTypes from 'actions/ActionTypes';
//import Dispatcher from 'app/Dispatcher';
//import routes from 'app/Routes.jsx';
//import Router from 'react-router';

//console.log('app starting');


//require('bootstrap-less/bootstrap');
//require('styles/less/flat-ui');


//function onSetMeta(name, content) {
//    // Remove and create a new <meta /> tag in order to make it work
//    // with bookmarks in Safari
//    var elements = document.getElementsByTagName('meta');
//    [].slice.call(elements).forEach(function (element) {
//        if (element.getAttribute('name') === name) {
//            element.parentNode.removeChild(element);
//        }
//    });

//    var meta = document.createElement('meta');
//    meta.setAttribute('name', name);
//    meta.setAttribute('content', content);
//    document.getElementsByTagName('head')[0].appendChild(meta);
//};

//function run() {
//    Router.run(routes, function (Handler) {
//      React.render(<Handler />, document.body);
//    });
//};

$(document).ready(function() {
    FastClick.attach(document.body);
//    run();
});
