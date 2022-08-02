export const ProcedureMassCommunicationDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedureMassCommunications/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/procedureMassCommunications/:id/documents/new'
        }
      }
    );
  }
];
