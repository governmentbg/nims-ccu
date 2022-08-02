export const ProgrammeDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmes/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/programmes/:id/documents/new'
        }
      }
    );
  }
];
