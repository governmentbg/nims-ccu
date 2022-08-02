export const ActuallyPaidAmountDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/actuallyPaidAmounts/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/actuallyPaidAmounts/:id/documents/new'
        }
      }
    );
  }
];
