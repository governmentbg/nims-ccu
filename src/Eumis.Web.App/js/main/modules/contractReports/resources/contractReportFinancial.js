export const ContractReportFinancialFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/financials/:ind',
      {},
      {
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/financials/:ind/changeStatusToDraft'
        },
        changeStatusToReturned: {
          method: 'POST',
          url: 'api/contractReports/:id/financials/:ind/changeStatusToReturned'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/contractReports/:id/financials/:ind/changeStatusToActual'
        },
        canChangeStatusToReturned: {
          method: 'POST',
          url: 'api/contractReports/:id/financials/:ind/canChangeStatusToReturned'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/:id/financials/canCreate'
        }
      }
    );
  }
];
