// Karma configuration
// Generated on Fri Jul 03 2015 23:19:25 GMT+0200 (Central European Daylight Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: 'web',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jspm', 'jasmine'],

    jspm: {
      // Edit this to your needs
      loadFiles: ['*.js', 'tests/**/*.js', 'Scripts/**/*.js'],
      paths: {
        '*': '*.js'
      }
    },


    // list of files / patterns to load in the browser
    files: [],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
      '*.js': ['babel', 'coverage'],
      'tests/**/*.js': ['babel', 'coverage'],
      'Scripts/**/*.js': ['babel', 'coverage']
    },

    'babelPreprocessor': {
      options: {
        sourceMap: 'inline',
        modules: 'system',
        moduleIds: false,
        optional: [
          "es7.decorators",
          "es7.classProperties"
        ]
      }
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress', 'coverage'],

    coverageReporter: {
        dir: "test/coverage/",
        reporters: [
            { type: 'lcov', subdir: 'report-lcov' },
            { type: 'text-summary', subdir: '.', file: 'coverage-summary.txt' },
            { type: 'text' },
        ]
    },

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
    browsers: ['Chrome'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false
  })
}
