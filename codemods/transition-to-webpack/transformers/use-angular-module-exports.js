module.exports = function transformer(file, api) {
  const j = api.jscodeshift;
  const { expression, statement, statements } = j.template;
  const {
    createModuleIdentifier,
    getAngularModuleExpression,
    createAddImport,
  } = require('./utils')(j);

  const root = j(file.source);
  
  const imports = root.find(j.ImportDeclaration);
  const body = root.find(j.Program).get('body').value;

  const moduleExpressions = root
    .find(j.ExpressionStatement)
    .paths()
    .filter(path =>
      path.scope &&
      path.scope.isGlobal
    ).map(path => ({
      statementPath: path,
      moduleExprPath: getAngularModuleExpression(path.get('expression')),
    }))
    .filter(({ moduleExprPath }) => moduleExprPath && moduleExprPath.node.arguments.length === 2);

  if (moduleExpressions.length === 1) {
    moduleExpressions.forEach(({ statementPath, moduleExprPath }) => {
      const moduleName = createModuleIdentifier(moduleExprPath.node.arguments[0].value);
      statementPath.replace(
        j.variableDeclaration(
          'const',
          [
            j.variableDeclarator(
              j.identifier(moduleName),
              statementPath.node.expression
            )
          ]
        )
      );

      body.push(
        statement([
          `\nexport default ${moduleName}.name;\n`
        ])
      );
    });
  } else if (moduleExpressions.length > 0) {
    throw new Error('More than one module declaration');
  }
  
  return root.toSource({ quote: 'single' });
}
