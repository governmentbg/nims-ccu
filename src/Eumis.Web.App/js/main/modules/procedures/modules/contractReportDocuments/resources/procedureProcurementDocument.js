export const ProcedureProcurementDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/procurementDocs/:ind',
      {},
      {
        newProcurementDocument: {
          method: 'GET',
          url: 'api/procedures/:id/procurementDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/procurementDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/procurementDocs/:ind/activate'
        }
      }
    );
  }
];
