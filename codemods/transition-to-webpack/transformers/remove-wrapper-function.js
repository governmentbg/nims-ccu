const imports = {
  _: 'lodash',
  angular: 'angular',
  moment: 'moment',
  s: 'underscore.string',
  Select2: 'select2'
};

const skippedImports = [
  'window',
  '$'
];

module.exports = function transformer(file, api) {
  const { jscodeshift: j } = api;
  
  const createImport = (module, as) => {
    return j.importDeclaration(
      [
        j.importDefaultSpecifier(
          j.identifier(as)
        )
      ],
      j.literal(module)
    )
  };

  var root = j(file.source);
  
  const body = root.find(j.Program).get('body').value;

  if (!j(body).every(path => j.match(path, {
   		type: 'ExpressionStatement',
    	expression: {
          type: 'CallExpression',
          callee: {
            type: 'FunctionExpression'
          }
        }
  }))
  ) {
    throw new Error('no function wrapper');
    return;
  }
  
  if (body.length != 1) {
    throw new Error('more than one function wrapper per file');
    return;
  }
  
  const fn = body[0].expression.callee;
  
  const skipped = [];
  const importStatements =
    fn.params
      .map(i => {
        let identifier = i.name;

        if (skippedImports.indexOf(identifier) !== -1) {
          skipped.push(identifier);
          return null;
        }

        if (!imports[identifier]) {
          throw new Error('unknown import');
        }

        return createImport(imports[identifier], identifier);
      })
      .filter(imp => imp);

  const fnBodyWithoutUseStrict = j(fn.body.body)
    .filter(path => !j.match(path, {
      type: 'ExpressionStatement',
      expression: {
        type: 'Literal',
        value: 'use strict'
      }
    }))
    .nodes();

  const newBody = [
    ...importStatements,
    ...fnBodyWithoutUseStrict,
  ];

  // preserve file comments, skipping the /*global */ comment
  newBody[0].comments = (body[0].comments || []).filter((c) => !/globals?((?:,?\s(?:[\w$]+))+)/.test(c.value));

  // add a /*global ...*/ comment with globals that are not to be transformed to import statements
  if (skipped.length) {
    newBody[0].comments.push(j.commentBlock(`global ${skipped.join(', ')}`, true, false));
  }
  
  body.length = 0;
  body.unshift(...newBody);

  let newSrc = root.toSource({ quote: 'single' });

  // remove extra newlines between imports
  // https://github.com/benjamn/recast/issues/371
  newSrc = newSrc.replace(/(import.*(?:\r?\n))(?:\r?\n)+(import)/g, '$1$2');

  return newSrc;
}
