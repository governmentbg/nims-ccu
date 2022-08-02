export const ProcedureFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/procedures/:id/files/:fileKey');
  }
];
