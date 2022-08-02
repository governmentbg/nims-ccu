export const ProcurementDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procurements/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/procurements/:id/documents/new'
        }
      }
    );
  }
];
