export const ContractReportCorrectionDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportCorrections/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/contractReportCorrections/:id/documents/new'
        }
      }
    );
  }
];
