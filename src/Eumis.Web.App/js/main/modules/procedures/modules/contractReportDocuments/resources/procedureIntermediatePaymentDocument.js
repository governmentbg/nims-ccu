export const ProcedureIntermediatePaymentDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/intermediatePaymentDocs/:ind',
      {},
      {
        newIntermediatePaymentDocument: {
          method: 'GET',
          url: 'api/procedures/:id/intermediatePaymentDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/intermediatePaymentDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/intermediatePaymentDocs/:ind/activate'
        }
      }
    );
  }
];
