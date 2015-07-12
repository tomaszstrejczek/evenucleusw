var path = require('path');
var compiler = require('./bower_components/ember/ember-template-compiler');

module.exports = function (grunt) {
    grunt.config.init({
        converthbs: {
            options: {
                src: ['./**/*.hbs'],
                dest: './templates.js'
            }
        }
    });
    grunt.registerTask('converthbs', 'Precompiling hbs', function () {
        grunt.log.write('Starting task...').ok();        

        var options = this.options();

        var files = grunt.file.expand(options.src);
        grunt.log.writeln("files " + files);

        var outFile = "";

        files.forEach(function(file) {
            grunt.log.writeln("Processing " + file);

            var input = grunt.file.read(file, { encoding: 'utf8' });
            var module = path.basename(file, '.hbs');
            grunt.log.writeln("module " + module);
            var template = compiler.precompile(input, {moduleName:module});
            var output = "Ember.TEMPLATES['" + module + "'] = Ember.HTMLBars.template(" + template + ");";
            outFile = outFile + "\n" + output;
        });

        grunt.file.write(options.dest, outFile, { encoding: 'utf8' });
    });

    grunt.registerTask('default', ['converthbs']);
}