export const ProcedureFinalPaymentDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/finalPaymentDocs/:ind',
      {},
      {
        newFinalPaymentDocument: {
          method: 'GET',
          url: 'api/procedures/:id/finalPaymentDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/finalPaymentDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/finalPaymentDocs/:ind/activate'
        }
      }
    );
  }
];
