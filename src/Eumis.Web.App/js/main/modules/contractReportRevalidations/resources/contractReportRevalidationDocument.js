export const ContractReportRevalidationDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportRevalidations/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/contractReportRevalidations/:id/documents/new'
        }
      }
    );
  }
];
