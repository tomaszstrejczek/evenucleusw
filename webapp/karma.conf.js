// Karma configuration
// Generated on Mon Aug 17 2015 15:30:29 GMT+0200 (Central European Daylight Time)

var path = require('path');

var SCSS_LOADER = 'style-loader!css-loader!sass-loader?includePaths[]=' +
        path.resolve(__dirname, './node_modules/bootstrap-sass/assets/stylesheets');

var LESS_LOADER = 'style-loader!css-loader!less-loader';


module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['mocha'],


    // list of files / patterns to load in the browser
    files: [
      'test/dom/**/*-test.js*',
    ],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
      'test/dom/**/*-test.js*': ['webpack', 'sourcemap'],
    },


    webpack: {
        devtool: 'inline-source-map', //just do inline source maps instead of the default
        module: {
        loaders: [
            {
              //tell webpack to use jsx-loader for all *.jsx files
              test: /\.jsx$/,
              loader: 'jsx-loader?insertPragma=React.DOM&harmony'
            },
            {
              test: /\.js(x)?$/,
              exclude: /node_modules/,
              loader: 'babel-loader'
            },
            { test: /\.css$/, loader: 'style-loader!css-loader' },
            { test: /\.less$/, loader: LESS_LOADER },
            { test: /\.scss$/, loader: SCSS_LOADER },


            { test: /\.woff(2)?(\?v=\d+\.\d+\.\d+)?$/,   loader: "url?limit=10000&mimetype=application/font-woff" },
            { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,    loader: "url?limit=10000&mimetype=application/octet-stream" },
            { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/,    loader: "file" },
            { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,    loader: "url?limit=10000&mimetype=image/svg+xml" },
            { test: /\.png$/,    loader: "file" },
        ]
      },
      resolve: {
        extensions: ['', '.js', '.jsx', '.less'],
        root: [__dirname, path.join(__dirname, './node_modules')]
      },
      resolveLoader: {
        root: [path.resolve(__dirname, './node_modules')],
        extensions: ['', '.js']
      },
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['PhantomJS'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: true,

    plugins: [
       require("karma-webpack"),
       require("karma-sourcemap-loader"),
       require("karma-mocha"),
       require("karma-phantomjs-launcher"),
    ],

  })
}
