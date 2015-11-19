var path = require('path');
var WebpackNotifierPlugin = require('webpack-notifier');

var SCSS_LOADER = 'style-loader!css-loader!sass-loader?includePaths[]=' +
        path.resolve(__dirname, './node_modules/bootstrap-sass/assets/stylesheets');

var LESS_LOADER = 'style-loader!css-loader!less-loader';

module.exports = {
  context: path.join(__dirname, '.'),
  entry: {
    main: './app.tsx',
    dom: 'test/dom/index.ts'
  },
  output: {
    path: path.join(__dirname, 'Built'),
    filename: '[name].bundle.js',
    publicPath: '/Built/'
  },
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

        { test: /\.ts(x)?$/, loader: "ts-loader" },
    ]
  },
  resolve: {
    extensions: ['', '.js', '.jsx', '.less', '.ts', '.tsx'],
    root: [__dirname, path.join(__dirname, './node_modules')]
  },
  resolveLoader: {
    root: [path.resolve(__dirname, './node_modules')],
    extensions: ['', '.js']
  },
  plugins: [
      new WebpackNotifierPlugin()
  ],
//  devtool: 'source-map',
  ts: {
    compiler: 'typescript'
  }
}