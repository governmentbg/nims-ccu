module.exports = function transformer(file, api) {
  const { jscodeshift: j, stats } = api;

  const getAngularModuleRegistrations = (path, res = []) => {
    if (!j.match(path, { type: 'CallExpression' })) {
      return false;
    }

    const callee = path.get('callee');
    if (j.match(callee, {
        type: 'MemberExpression',
        object: {
          type: 'Identifier',
          name: 'angular',
        },
        property: {
          type: 'Identifier',
          name: 'module',
        },
      }) &&
      callee.get('object').scope &&
      callee.get('object').scope.isGlobal
    ) {
      return {
        registrations: res,
        path,
      };
    }

    if (!j.match(callee, {
      type: 'MemberExpression',
      property: {
        type: 'Identifier',
      },
    })) {
      return false;
    }

    const args = path.get('arguments');
    const propertyName = callee.get('property').value.name;
    return getAngularModuleRegistrations(
      callee.get('object'),
      [
        [propertyName, args.value],
        ...res
      ]
    );
  };

  const createExportDeclaration = (name, value) =>
    j.exportNamedDeclaration(
      j.variableDeclaration(
        'const',
        [j.variableDeclarator(
          j.identifier(name),
          value
        )]
      )
    );

  const constExports = [];
  const exportSpecifiers = [];
  const addExport = (name, value) => {
    if (j.match(value, { type: 'Identifier' })) {
      exportSpecifiers.push(
        j.exportSpecifier(value, j.identifier(name))
      );
    } else {
      constExports.push(
        createExportDeclaration(name, value)
      );
    }
  };

  const capitalize = (str) => str[0].toUpperCase() + str.substring(1);

  var root = j(file.source);

  root
    .find(j.ExpressionStatement)
    .paths()
    .filter(path =>
      path.scope &&
      path.scope.isGlobal
    )
    .map(path => ({
      path,
      res: getAngularModuleRegistrations(path.get('expression')),
    }))
    .filter(({ res }) => res)
    .forEach(({ path, res: { registrations, path: angularModulePath } }) => {
      const moduleCreate = angularModulePath.get('arguments').value.length === 2;
      if (moduleCreate) {
        if(registrations.some(([method, args]) => method !== 'config' && method !== 'run' && method !== 'constant' && method !== 'factory')) {
          throw new Error('Unsupported: module creations should not use anything other than config, run, constant and factory registrations');
        }

        //skip module creations
        return;
      }

      const servicesRegistrations = registrations.filter(([method, args]) => method !== 'config' && method !== 'run');
      
      if (servicesRegistrations.length) {
        if (servicesRegistrations.length !== registrations.length) {
          throw new Error('Unsupported: config and run blocks cannot be exported');
        }

        for (const [method, args] of servicesRegistrations) {
          if (args.length != 2) {
            throw new Error('Unsupported');
          }

          const [nameLiteral, value] = args;
          if (!j.match(nameLiteral, { type: 'Literal' })) {
            throw new Error('Unsupported');
          }
          
          const name = nameLiteral.value;

          switch (method) {
            case 'controller':
              if (!name.endsWith('Ctrl')) {
                throw new Error('Unsupported - controller not ending in Ctrl')
              }
              addExport(name, value);
              break;

            case 'provider':
            case 'factory':
            case 'service':
            case 'value':
            case 'constant':
            case 'decorator':
            case 'animation':
            case 'filter':
            case 'directive':
            case 'component':
              addExport(`${name}${capitalize(method)}`, value);
              break;

            default:
              throw new Error('Unsupported');
          }
        }

        j(path).remove();
      }
    });
  
  const body = root.find(j.Program).get('body');

  if (exportSpecifiers.length) {
    body.push(
      j.exportNamedDeclaration(
        null,
        exportSpecifiers
      )
    );
  }

  constExports.forEach(e => body.push(e));

  return root.toSource({ quote: 'single' });
}
