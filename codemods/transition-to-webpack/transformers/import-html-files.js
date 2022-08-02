const path = require('path');
const fs = require('fs');

const roots = {
  'js': 'C:\\Users\\angel\\Git\\Eumis\\src\\Eumis.Web.App\\js\\',
  'app': 'C:\\Users\\angel\\Git\\Eumis\\src\\Eumis.Web.App\\test\\app\\',
};

module.exports = function transformer(file, api) {
  const { jscodeshift: j, stats } = api;
  const { expression, statement, statements } = j.template;
  const {
    capitalize,
    createAddImport,
  } = require('./utils')(j);

  const fileIdentifiers = {};
  const indentifierCounts = {};

  const addFileIdentifier = (file, identifier) => {
    indentifierCounts[identifier] = indentifierCounts[identifier] || 0;
    indentifierCounts[identifier]++;
    if (indentifierCounts[identifier] > 1) {
      identifier = `${identifier}${indentifierCounts[identifier]}`;
    }

    fileIdentifiers[file] = `${identifier}TemplateUrl`;
  };

  const getImportPath = (f) => {
    let resolvedPath;
      for (const [root, rootPath] of Object.entries(roots)) {
        if ((new RegExp(`^${root}/`)).test(f)) { // starts with ${root}/
          resolvedPath = path.resolve(rootPath, path.relative(root, f));
          break;
        }
      }

      if (!resolvedPath || !fs.existsSync(resolvedPath)) {
        throw new Error('Cannot resolve template file');
      }

      const relativePath = path.relative(path.dirname(file.path), resolvedPath);
      let importPath =  relativePath.replace(/\\/g, '/');

      if (/\.\.\//.test(importPath)) { // starts with ../
        throw new Error('Importing templates from outside folders');
      }

      importPath = './' + importPath;

      return importPath;
  };

  const createImportIdentifiers = (files) => {
    const filenameGroups = files
    // parse
    .map(file => {
      const { dir, name } = path.parse(file);
      const dirname = path.basename(dir);
      return [file, dirname, name];
    })
    // group
    .reduce((acc, [file, dirname, name]) => {
      acc[name] = acc[name] || [];
      acc[name].push([file, dirname]);
      return acc;
    }, {});

    Object.entries(filenameGroups)
      .forEach(([name, items]) => {
        if (items.length === 1) {
          const [file] = items[0];
          addFileIdentifier(file, name);
        } else {
          for (const [file, dirname] of items) {
            // prefix the filename with the folder
            addFileIdentifier(file, `${dirname}${capitalize(name)}`);
          }
        }
      });
  };

  const root = j(file.source);

  const htmlFileLiterals =
    root.find(j.Literal)
    .filter(l => typeof l.node.value === 'string' && /.+\.html/.test(l.node.value));

  createImportIdentifiers(
    htmlFileLiterals
      .nodes()
      .map(l => getImportPath(l.value))
      .filter((value, index, self) => self.indexOf(value) === index) //unique
    );

  htmlFileLiterals.forEach(l => l.replace(j.identifier(fileIdentifiers[getImportPath(l.node.value)])));

  let addImport = createAddImport(root);
  
  Object.entries(fileIdentifiers)
    .sort(([file1], [file2]) => file1 < file2) // sort reverse alphabetically
    .forEach(([file, identifier]) => {
      addImport(
        statement([
          `import ${identifier} from '${file}';\n`
        ])
      );
    });

  let newSrc = root.toSource({ quote: 'single' });

  // remove extra newlines between imports
  // https://github.com/benjamn/recast/issues/371
  newSrc = newSrc.replace(/(import.*(?:\r?\n))(?:\r?\n)+(import)/g, '$1$2');

  return newSrc;
};
