export const ContractReportAttachedFinancialCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/financialCorrections/:ind',
      {},
      {
        getCorrections: {
          method: 'GET',
          url: 'api/contractReports/:id/financialCorrections/corrections',
          isArray: true
        },
        canAttach: {
          method: 'POST',
          url: 'api/contractReports/:id/financialCorrections/canAttach'
        }
      }
    );
  }
];
