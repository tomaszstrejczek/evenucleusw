require.config({
  shim: {
    "JSXTransformer": "JSXTransformer"
  },

  deps: ["app"],

  paths: {
    bootstrap: "bower_components/bootstrap/dist/js/bootstrap",
    fastclick: "bower_components/fastclick/lib/fastclick",
    "jquery-cookie": "bower_components/jquery-cookie/jquery.cookie",
    jquery: "bower_components/jquery/dist/jquery",
    react: "bower_components/react/react-with-addons",
    requirejs: "bower_components/requirejs/require",
    JSXTransformer: "bower_components/react/JSXTransformer",
    jsx: "bower_components/requirejs-react-jsx/jsx",
    text: "bower_components/requirejs-text/text",
    "mirrorkey-browser": "bower_components/mirrorkey/mirrorkey_browser",
    flux: "bower_components/flux/dist/flux",
    superagent: "bower_components/superagent/lib/client",
  },

  config: {
    jsx: {
      fileExtension: ".jsx",
      transformOptions: {
        harmony: true,
        stripTypes: false,
        inlineSourceMap: true
      },
      usePragma: false
    }
  },

  packages: [
    { name: 'when', location: 'bower_components/when', main: 'when' }
  ]
});
