export const ContractReportCertCorrectionDocumentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportCertCorrections/:id/documents/:ind',
      {},
      {
        newDocument: {
          method: 'GET',
          url: 'api/contractReportCertCorrections/:id/documents/new'
        }
      }
    );
  }
];
