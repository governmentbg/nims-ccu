export const ProcedureAppDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/appDocs/:ind',
      {},
      {
        newAppDocument: {
          method: 'GET',
          url: 'api/procedures/:id/appDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/appDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/appDocs/:ind/activate'
        }
      }
    );
  }
];
