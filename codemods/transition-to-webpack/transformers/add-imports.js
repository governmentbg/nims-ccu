const path = require('path');

const imports = {
  '..\\..\\..\\src\\Eumis.Web.App\\js\\app.js': [
    'autofill-event/src/autofill-event',
  ],

  '..\\..\\..\\src\\Eumis.Web.App\\js\\app_bg.js': [
    'angular-i18n/angular-locale_bg-bg',
    'moment/locale/bg',
  ],
    
  '..\\..\\..\\src\\Eumis.Web.App\\js\\scaffolding\\scaffolding_bg.js': [
    'bootstrap-datepicker/js/bootstrap-datepicker',
    'bootstrap-datepicker/js/locales/bootstrap-datepicker.bg',
    'select2',
    'select2/select2_locale_bg',
  ],

  '..\\..\\..\\src\\Eumis.Web.App\\js\\scaffolding\\directives\\date\\dateDirective.js': [
    'bootstrap-datepicker/js/bootstrap-datepicker',
  ],

  '..\\..\\..\\src\\Eumis.Web.App\\js\\scaffolding\\directives\\file\\fileCtrl.js': [
    'jquery-ui/ui/jquery.ui.widget',
    'jquery-ui/ui/jquery.ui.core',
    'jquery-ui/ui/jquery.ui.mouse',
    'jquery-ui/ui/jquery.ui.sortable',
    'blueimp-file-upload/js/jquery.iframe-transport',
    'blueimp-file-upload/js/jquery.fileupload',
  ],

  '..\\..\\..\\src\\Eumis.Web.App\\js\\scaffolding\\directives\\suggestion\\suggestionDirectives.js': [
    'typeahead.js/dist/typeahead.jquery',
  ],

  '..\\..\\..\\src\\Eumis.Web.App\\js\\scaffolding\\directives\\treetable\\treetableDirective.js': [
    'jquery-treetable/javascripts/src/jquery.treetable',
  ],

  '..\\..\\..\\src\\Eumis.Web.App\\test\\app\\testapp_bg.js': [
    'angular-i18n/angular-locale_bg-bg',
    'moment/locale/bg',
  ],
}

module.exports = function transformer(file, api, options) {
  const absoluteImports = Object.entries(imports)
    .map(([filepath, imports]) => {
      return [path.resolve(__dirname, filepath), imports];
    })
    .reduce((obj, [k, v]) => {
      obj[k] = v;
      return obj;
    }, {});

  const absoluteFilepath = path.resolve(file.path);
  if (!absoluteImports[absoluteFilepath]) {
    return;
  }

  const { jscodeshift: j, stats } = api;
  const { expression, statement, statements } = j.template;
  const { createAddImport } = require('./utils')(j);

  const root = j(file.source);

  const addImport = createAddImport(root);
  
  absoluteImports[absoluteFilepath].reverse().forEach(importPath => {
    addImport(statement([
      `import '${importPath}';\n`
    ]));
  });

  let newSrc = root.toSource({ quote: 'single' });

  // remove extra newlines between imports
  // https://github.com/benjamn/recast/issues/371
  newSrc = newSrc.replace(/(import.*(?:\r?\n))(?:\r?\n)+(import)/g, '$1$2');

  return newSrc;
};
