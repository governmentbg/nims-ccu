export const ContractReportPaymentFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id/payments/:ind',
      {},
      {
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/payments/:ind/changeStatusToDraft'
        },
        changeStatusToReturned: {
          method: 'POST',
          url: 'api/contractReports/:id/payments/:ind/changeStatusToReturned'
        },
        changeStatusToActual: {
          method: 'POST',
          url: 'api/contractReports/:id/payments/:ind/changeStatusToActual'
        },
        canChangeStatusToReturned: {
          method: 'POST',
          url: 'api/contractReports/:id/payments/:ind/canChangeStatusToReturned'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/:id/payments/canCreate'
        }
      }
    );
  }
];
