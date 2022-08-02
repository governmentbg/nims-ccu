export const DeclarationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/declarations/:id/files/:fileKey');
  }
];
