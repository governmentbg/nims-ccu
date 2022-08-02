export const ProcedureMassCommunicationFileFactory = [
  'urlTemplate',
  function(urlTemplate) {
    return urlTemplate('api/procedureMassCommunications/:id/files/:fileKey');
  }
];
