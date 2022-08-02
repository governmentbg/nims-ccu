const fs = require('fs');
const path = require('path');

module.exports = function transformer(file, api) {

  const { jscodeshift: j, stats } = api;
  const { expression, statement, statements } = j.template;
  const {
    createModuleIdentifier,
    getAngularModuleExpression,
    createAddImport,
  } = require('./utils')(j);

  const walk = (dir) => {
    const results = [];
    fs.readdirSync(dir)
      .forEach((filename) => {
        const filepath = path.join(dir, filename);
        const stat = fs.statSync(filepath);
        if (stat && stat.isDirectory()) { 
          Array.prototype.push.apply(results, walk(filepath));
        } else {
          results.push(filepath);
        }
      });
    return results;
  };

  const makeRelativeImportPath = (base, file) => {
    const { dir, name } = path.parse(path.relative(base, file));
    return `./${dir.replace(/\\/g, '/')}/${name}`;
  };

  const root = j(file.source);

  let moduleIdentifier;
  root
    .find(j.CallExpression)
    .paths()
    .filter(path =>
      path.scope &&
      path.scope.isGlobal &&
      getAngularModuleExpression(path) === path &&
      path.node.arguments.length === 2
    )
    .forEach(path => {
      if (moduleIdentifier) {
        throw new Error('More than one module found');
      }

      moduleIdentifier = createModuleIdentifier(path.node.arguments[0].value);
    });

  if (!moduleIdentifier) {
    return file.source;
  }

  const hasModuleVar = root
    .find(j.VariableDeclaration)
    .some(path =>
      path.scope &&
      path.scope.isGlobal &&
      path.get('declarations').value.length === 1 &&
      path.get('declarations', 0, 'id', 'name').value === moduleIdentifier
    );

  if (!hasModuleVar) {
    throw new Error('No module var found. Run \'use-angular-module-exports\' codemod first');
  }

  const moduleFolder = path.dirname(file.path);
  const normalizedFilePath = path.normalize(file.path);
  const moduleFiles =
    walk(moduleFolder)
    .filter(f =>
      path.extname(f) === '.js' &&
      !/_bg$/.test(path.basename(f, '.js')) && // does not end in '_bg'
      f !== normalizedFilePath
    );

  const methodEndings = {
    Provider: 'provider',
    Factory: 'factory',
    Service: 'service',
    Value: 'value',
    Constant: 'constant',
    Decorator: 'decorator',
    Animation: 'animation',
    Filter: 'filter',
    Directive: 'directive',
    Component: 'component',
    Ctrl: 'controller',
  }

  const endings = Object.keys(methodEndings);
  
  const methodGroups = {};
  
  const importStatements = [];
  for (const moduleFile of moduleFiles) {
    const importPath = makeRelativeImportPath(moduleFolder, moduleFile);

    if (/\.\/modules\//.test(importPath)) { // starts with ./modules
      continue; // skip submodules
    }

    const source = fs.readFileSync(moduleFile, 'utf8');
    const fileRoot = j(source);

    const imports = [];
    for(const exp of fileRoot.find(j.ExportNamedDeclaration).nodes()) {
      for (const spec of exp.specifiers) {
        imports.push(spec.exported.name);
      }
      if (exp.declaration) {
        for (const declarator of exp.declaration.declarations) {
          imports.push(declarator.id.name);
        }
      }
    }
  
    if (!imports.length) {
      throw new Error(`No exports found in ${moduleFile}`);
    }
  
    importStatements.push(
      statement([
        `import { ${imports.join(', ')} } from '${importPath}';\n`
      ])
    );
  
    for (const imp of imports) {
      let ending = endings.filter(e => imp.endsWith(e));
      if (ending.length != 1) {
        throw new Error('Unknown ending');
      }
  
      ending = ending[0];
      const group = methodEndings[ending];
      methodGroups[group] = methodGroups[group] || [];
  
      if (group === 'controller') {
        const controllerIsUsed = root.find(j.Literal)
          .filter(l => typeof l.node.value === 'string' && l.node.value === imp)
          .forEach(l => l.replace(j.identifier(imp)))
          .size() > 0;

        // if the controller is not used in the module file
        // add it to the angular.module().controllers() registrations
        // as it should be used elsewhere
        if (!controllerIsUsed) {
          const name = imp;
          const identifier = imp;
          methodGroups[group].push([name, identifier]);
        }
      } else {
        const name = imp.replace(new RegExp(`^(.+)${ending}$`), '$1'); //remove the ending
        const identifier = imp;
        methodGroups[group].push([name, identifier]);
      }
    }
  }

  importStatements
    .reverse()
    .forEach(createAddImport(root));

  const body = root.find(j.Program).get('body').value;

  Object.entries(methodGroups)
    .forEach(([m, mg]) =>
      mg.forEach(([name, identifier]) =>
        body.push(
          statement([
            `${moduleIdentifier}.${m}('${name}', ${identifier});\n`
          ])
        )
      )
    );

    let newSrc = root.toSource({ quote: 'single' });

    // remove extra newlines between imports
    // https://github.com/benjamn/recast/issues/371
    newSrc = newSrc.replace(/(import.*(?:\r?\n))(?:\r?\n)+(import)/g, '$1$2');

    return newSrc;
};
