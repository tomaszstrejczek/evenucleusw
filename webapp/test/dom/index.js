var context = require.context('test/dom', true, /-test\.js$/); //make sure you have your directory and regex test set correctly!
context.keys().forEach(context);
