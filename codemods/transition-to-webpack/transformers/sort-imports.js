const { default: compareImports } = require('codemod-imports-sort/dist/compareImports');

module.exports = function transformer(file, api, options) {
  const j = api.jscodeshift;
  const root = j(file.source);

  const declarations = root.find(j.ImportDeclaration);

  if (declarations.length === 0) {
    return;
  }

  const firstImport = declarations.nodes()[0];
  const leadingComments = firstImport.leadingComments || [];
  delete firstImport.leadingComments;
  delete firstImport.comments;

  const sortedDeclarations = declarations
    .nodes()
    .sort((a, b) => {
      const aSpecLength = a.specifiers.length;
      const bSpecLength = b.specifiers.length;

      // move side-effects-only imports on top
      if (!aSpecLength && bSpecLength) {
        return -1;
      } else if (aSpecLength && !bSpecLength) {
        return 1;
      }

      return compareImports(a.source.value, b.source.value);
    });

  declarations.remove();

  sortedDeclarations[0].leadingComments = [...leadingComments, ...(sortedDeclarations[0].leadingComments || [])];
  sortedDeclarations[0].comments = [...leadingComments, ...(sortedDeclarations[0].comments || [])];

  return root
    .find(j.Statement)
    .at(0)
    .insertBefore(sortedDeclarations)
    .toSource({ quote: 'single' });
};
