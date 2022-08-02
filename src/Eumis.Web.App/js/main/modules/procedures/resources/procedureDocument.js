export const ProcedureDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/procedures/:id/documents/new'
        }
      }
    );
  }
];
