export const ProcedureAdvancePaymentDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/advancePaymentDocs/:ind',
      {},
      {
        newAdvancePaymentDocument: {
          method: 'GET',
          url: 'api/procedures/:id/advancePaymentDocs/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/procedures/:id/advancePaymentDocs/:ind/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/procedures/:id/advancePaymentDocs/:ind/activate'
        }
      }
    );
  }
];
