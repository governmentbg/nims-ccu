module.exports = (j) => {
  const capitalize = (str) => str[0].toUpperCase() + str.substring(1);
  
  const createModuleIdentifier = (moduleName) => {
    const parts = moduleName.split('.');
    if (parts.length > 1 && parts[0] === 'main') {
      parts.shift();
    }
    const name = parts
      .map(p => capitalize(p))
      .join('');
  
    return `${name}Module`
  };
  
  const getAngularModuleExpression = (path) => {
    if (!j.match(path, { type: 'CallExpression' })) {
      return false;
    }
  
    const callee = path.get('callee');
    const args = path.get('arguments');
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
      return path;
    }
  
    if (!j.match(callee, {
      type: 'MemberExpression',
      property: {
        type: 'Identifier',
      },
    })) {
      return false;
    }
  
    return getAngularModuleExpression(callee.get('object'));
  };
  
  const createAddImport = (root) => {
    const imports = root.find(j.ImportDeclaration);
    if (imports.length) {
      const lastImport = j(imports.at(imports.length - 1).get());
      return (node) => lastImport.insertAfter(node);
    } else {
      const body = root.find(j.Program).get('body').value;
      return (node) => {
        body.unshift(node);
  
        //move first stament comments (normally those are the file comments)
        body[0].comments = (body[1].comments || []).map((c) => {
          let ctor;
          if (j.match(c, { type: 'CommentLine' })) {
            ctor = j.commentLine;
          } else if (j.match(c, { type: 'CommentBlock' })) {
            ctor = j.commentBlock;
          } else {
            throw new Error('Unknown comment type');
          }
  
          return ctor(c.value, c.leading, c.trailing);
        });
        body[1].comments = null;
      }
    }
  };

  return {
    capitalize,
    createModuleIdentifier,
    getAngularModuleExpression,
    createAddImport,
  };
};
