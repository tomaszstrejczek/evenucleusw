/// <reference path="./../../references.d.ts" />
/// <amd-dependency path="./../../node_modules/sinon"/>

import * as loginTest from './login-test';
import * as smoke0Test from './smoke0-test';

loginTest.Runner.run();
smoke0Test.Runner.run();