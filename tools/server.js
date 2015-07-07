var connect = require('connect');
var serveStatic = require('serve-static');
var path = __dirname + "\\..\\web";
console.log(path);
connect().use(serveStatic(path)).listen(60567);