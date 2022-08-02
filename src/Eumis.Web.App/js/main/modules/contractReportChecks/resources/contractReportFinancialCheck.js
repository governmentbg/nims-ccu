export const ContractReportFinancialCheckFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/financialChecks/:ind',
      {},
      {
        changeStatusToActive: {
          method: 'POST',
          url: 'api/contractReports/:id/financialChecks/:ind/changeStatusToActive'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/:id/financialChecks/canCreate'
        },
        canChangeStatusToActive: {
          method: 'POST',
          url: 'api/contractReports/:id/financialChecks/:ind/canChangeStatusToActive'
        }
      }
    );
  }
];
