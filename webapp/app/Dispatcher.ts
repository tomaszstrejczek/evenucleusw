/**
 * Created by ts15187 on 2015-07-15.
 */

import flux = require('flux');
import {AppActions} from './actions/AppActions';

export var Dispatcher: flux.Dispatcher<AppActions> = new flux.Dispatcher();
