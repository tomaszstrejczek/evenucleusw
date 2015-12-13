/// <reference path="./../../references.d.ts" />
/// <amd-dependency path="./../../node_modules/sinon"/>

import * as loginTest from './login-test';
import * as smoke0Test from './smoke0-test';
import * as apiTest from './api-test';
import * as apiKeysTest from './api-keys-test';


loginTest.Runner.run();
smoke0Test.Runner.run();
apiTest.Runner.run();
apiKeysTest.Runner.run();