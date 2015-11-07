/// <reference path="references.d.ts" />
/// <amd-dependency path="bootstrap-less/bootstrap" />
/// <amd-dependency path="styles/less/flat-ui" />

/// <reference path="app/Routes.tsx"/>

import {BaseStore} from './app/BaseStore';

//import App from 'app/App.jsx';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import $ = require('jquery');
import * as fclick from 'fastclick';
//import ActionTypes from 'actions/ActionTypes';
//import Dispatcher from 'app/Dispatcher';
import {routes} from './app/Routes';
import Router = require('react-router');

import {KernelCreator} from './app/kernel';

console.log('app starting');


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
    var kernel = KernelCreator.create();

    Router.run(routes, function (Handler: new() => React.Component<any, any>) {
      ReactDOM.render(<Handler/>, document.getElementById("app"));
    });
};

$(document).ready(function() {
    fclick.attach(document.body);
    run();
});
